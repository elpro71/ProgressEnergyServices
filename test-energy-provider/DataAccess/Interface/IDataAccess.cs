using System;
using System.Collections.Generic;

namespace DataAccess
{
    public static class Types
    {
        public class MatchingTokenResponse
        {
            public MatchingTokenResponseStatus State { get; internal set; }
            public Nullable<int> TicketNumber;
        }

        public static MatchingTokenResponse AmbiguousQuery = new MatchingTokenResponse { State = MatchingTokenResponseStatus.Ambiguous };
        public static MatchingTokenResponse NoTicketRecprded = new MatchingTokenResponse { State = MatchingTokenResponseStatus.None };
    }

    public interface IDataAccess
    {        
        Types.MatchingTokenResponse LookUp(string mailContent);
        bool UpdateTicket(int ticket, TicketUpdateRequest updateData);
        bool CreateNewTicket(TicketUpdateRequest updateData);

        IEnumerable<Ticket> ReadPage(int pageNumber);
             
        //event Action OnTicketSetChanges;
    }
}