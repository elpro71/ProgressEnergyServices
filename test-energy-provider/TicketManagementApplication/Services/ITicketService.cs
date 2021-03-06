using System.Collections.Generic;

namespace TicketManagementApplication.Services
{
    public interface ITicketService
    {

        IEnumerable<TicketSummary> GetTicketHistory(int pageNumber);

        string LoadTicket(int ticketNumber);

        string ReplyToTicket(int ticketNumber, string replyText);
            
    }
}