using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace TicketManagementApplication.Services
{
    class TicketAdaptor : ITicketService
    {


        public IEnumerable<TicketSummary> GetTicketHistory(int pageNumber)
        {
            var api = new DataAccess.Api();
            return
                    api
                        .ReadPage(pageNumber)
                        .Select(ToSummary) ;
        }

        public string LoadTicket(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public string ReplyToTicket(int ticketNumber, string replyText)
        {
            throw new NotImplementedException();
        }


        private TicketSummary ToSummary(Ticket ticket) =>
            new TicketSummary
            {
                TicketNumber = ticket.TicketNumber,
                LastMail = ticket.Mails.ToList().Select(x=>x.MailContent).Last(),
                LastSender = ticket.MailAddresses.ToList().Select(x=> x.Email).Last()
            };
    }
}
