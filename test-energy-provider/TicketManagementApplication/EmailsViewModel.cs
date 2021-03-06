using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TicketManagementApplication.Services;

namespace TicketManagementApplication
{
    public class EmailsViewModel : ReactiveObject
    {

        private readonly Subject<Unit> initializer = new Subject<Unit>();
        private int pageNumber = 0;
        private readonly ObservableAsPropertyHelper<IEnumerable<TicketSummary>> _tickets;
        public EmailsViewModel(ITicketService ticketService)
        {
            _tickets =
                initializer
                    .Select(_=> ticketService.GetTicketHistory(pageNumber))
                    .ToProperty(this, x => x.Tickets);
               
            initializer.OnNext(Unit.Default);        
        }

        public IEnumerable<TicketSummary> Tickets => _tickets.Value;
    }
}
