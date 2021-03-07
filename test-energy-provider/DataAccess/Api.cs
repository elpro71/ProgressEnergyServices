using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataAccess
{
    public class Api : IDataAccess
    {       
        Types.MatchingTokenResponse IDataAccess.LookUp(string mailContent)
        {
            var dbEntities = new DataAccess.SupportDBEntities();
            var search =
                dbEntities
                .Tickets
                .Where(t => mailContent.Contains(t.TicketNumber.ToString()));
            var count = search.Count();

            switch (count)
            {
                case 0: return Types.NoTicketRecprded;
                case 1: return CreateResponse(search);
                default: return Types.AmbiguousQuery;
            }
        }
        
        bool IDataAccess.UpdateTicket(int ticket, TicketUpdateRequest updateData)
        {
            var dbEntities = new DataAccess.SupportDBEntities();

            var mail = dbEntities.Mails.Add(new Mail { MailContent = updateData.Message });
            var address = dbEntities.MailAddresses.Add(new MailAddress { Email = updateData.eMail });

            var ticketRec =
                dbEntities.Tickets.FirstOrDefault(rec => rec.TicketNumber == ticket) ??
                throw new InvalidOperationException("failing to update an none-existing record");
            ticketRec.Mails.Add(mail);
            ticketRec.MailAddresses.Add(address);

            dbEntities.SaveChanges();
            return true;
        }

        bool IDataAccess.CreateNewTicket(TicketUpdateRequest updateData)
        {
            var dbEntities = new SupportDBEntities();
            var ticketRec = new Ticket {};

            ticketRec.Mails.Add(new Mail { MailContent = updateData.Message });
            ticketRec.MailAddresses.Add(new MailAddress { Email = updateData.eMail });

            dbEntities.Tickets.Add(ticketRec);
            dbEntities.SaveChanges();
            return true;
        }

        public IEnumerable<Ticket> ReadPage(int pageNumber)
        {
            const int pageSize = 25;
            var dbEntities = new DataAccess.SupportDBEntities();

            dbEntities.Tickets.Include(x=> x.MailAddresses).Include(x=>x.Mails).Load();            
            return
                dbEntities.Tickets
                .OrderBy(ticket => ticket.TicketNumber)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();
        }

        #region Implementation details
        private Types.MatchingTokenResponse CreateResponse(IQueryable<Ticket> dbSearch) =>
        new Types.MatchingTokenResponse
        {
            State = MatchingTokenResponseStatus.Ok,
            TicketNumber = dbSearch.First().TicketNumber
        };


        #endregion
    }

    public static class Bootstrap
    {
        public static IDataAccess Init() => new Api();        
    }

}
