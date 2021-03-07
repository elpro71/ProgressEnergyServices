using DataAccess;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TicketManagementApplication.Services;

namespace TicketManagementApplication
{
    public class EmailsViewModel : ReactiveObject
    {
        private readonly Subject<Unit> _initializer = new Subject<Unit>();
        private int _pageNumber = 0;
        private TicketSummary _ticket;
        
        private readonly ObservableAsPropertyHelper<IEnumerable<TicketSummary>> _tickets;
        public EmailsViewModel(ITicketService ticketService)
        {
            ResponseCommand = ReactiveCommand.Create<int, Unit>(ticketNumber => { /* open response window */ return Unit.Default; });
            ExitCommand = ReactiveCommand.Create(() => { });

            NextPageCommand = ReactiveCommand.Create(() => { }, this.WhenAnyValue(vm => vm.PageNumber).Select( pg => pg < 1000));
            PreviousPageCommand = ReactiveCommand.Create(() => { }, this.WhenAnyValue(vm=> vm.PageNumber).Select( pg => pg >0));

            NextPageCommand.Select(_ => ++PageNumber);
            PreviousPageCommand.Select(_ => --PageNumber);

            _tickets =
                   _initializer
                    .Select(_=> ticketService.GetTicketHistory(_pageNumber))
                    .ToProperty(this, x => x.Tickets);
               
            _initializer.OnNext(Unit.Default);
        }

        public IEnumerable<TicketSummary> Tickets => _tickets.Value;
        public TicketSummary Ticket { get => _ticket; set => this.RaiseAndSetIfChanged(ref _ticket, value); }       
        public int PageNumber { get => _pageNumber; set => this.RaiseAndSetIfChanged(ref _pageNumber, value); }
        public ReactiveCommand<int, Unit> ResponseCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
        public ReactiveCommand<Unit, Unit> PreviousPageCommand { get; }
    }
}
