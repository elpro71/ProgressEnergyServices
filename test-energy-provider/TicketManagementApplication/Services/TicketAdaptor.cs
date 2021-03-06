using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementApplication.Services
{
    class TicketAdaptor : ITicketService
    {
        public IEnumerable<TicketSummary> GetTicketHistory(int pageNumber)
        {
            return new[]
            {
                new TicketSummary { TicketNumber = 1 },
                new TicketSummary { TicketNumber = 2 },
                new TicketSummary { TicketNumber = 3 },
                new TicketSummary { TicketNumber = 4 },  
                new TicketSummary { TicketNumber = 5 },
            };
        }

        public string LoadTicket(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public string ReplyToTicket(int ticketNumber, string replyText)
        {
            throw new NotImplementedException();
        }
    }
}
