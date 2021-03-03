using MailKit.Net.Imap;
using MailKit.Security;
using System.Linq;
using DataAccess;
using System;
using MailKit.Net.Smtp;


namespace HMA_MailAgent
{
    class Program
    {
        private static void RunMailAgent()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                client.Authenticate("lemontzis.e@gmail.com", "xbmqygndvsuxlvrq");
                client.Inbox.Open(MailKit.FolderAccess.ReadOnly);

                var mesgIds = client
                        .Inbox
                        .Search(MailKit.Search.SearchQuery.NotSeen)
                        .Select(id => client.Inbox.GetMessage(id))
                        .Where(msg => msg.Sender!=null)
                        .Select((msg, msgId) => ReadMessage (msg, msgId))
                        .Select(HandleResponse)
                        .Select(Commit)
                        .Take(10)
                        .ToList();

                client.Disconnect(true);
            }
        }

        private static bool Commit(TicketUpdateReply reply)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                client.Authenticate("lemontzis.e@gmail.com", "xbmqygndvsuxlvrq");
                client.Inbox.Open(MailKit.FolderAccess.ReadOnly);
        
                client.Disconnect(true);
            }
            return true;
        }

        private static TicketUpdateRequest ReadMessage(MimeKit.MimeMessage msg, int msgId)
        {        
            var visitor = new MimeVisitor(msgId);
            msg.Accept(visitor);
            return visitor.ReadEmail();        
        }

        private static TicketUpdateReply HandleResponse(TicketUpdateRequest updateRequest)
        {
            TicketUpdateReply reply;
            var dataAccess = Bootstrap.Init();
            var lookUp = dataAccess.LookUp(updateRequest.Message);
            switch  (lookUp.State)
            {
                case MatchingTokenResponseStatus.None: reply = CreateResponsePositive(dataAccess, updateRequest); break;
                case MatchingTokenResponseStatus.Ok:   reply = Update(lookUp.TicketNumber.Value, dataAccess, updateRequest); break;
                case MatchingTokenResponseStatus.Ambiguous: reply = ResponseAmbiguousContext(updateRequest); break;
                default: throw new Exception("bug unexepcted case");
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // use the OAuth2.0 access token obtained above
                //var oauth2 = new SaslMechanismOAuth2("mymail@gmail.com", credential.Token.AccessToken);
                //client.Authenticate(oauth2);
                //client.Send(message);
                //client.Disconnect(true);
            }
            return reply;
        }

        private static TicketUpdateReply Update(int ticketNumber, IDataAccess dataAccess, TicketUpdateRequest updateRequest) =>
            dataAccess.UpdateTicket(ticketNumber, updateRequest)
            ? new TicketUpdateReply { MessageId = updateRequest.MessageId, Completed = true,  TicketNumber = ticketNumber }
            : new TicketUpdateReply { MessageId = updateRequest.MessageId,Completed = false };
        private static TicketUpdateReply ResponseAmbiguousContext(TicketUpdateRequest ticket) =>
            new TicketUpdateReply { MessageId = ticket.MessageId, Completed = false };
        
        private static TicketUpdateReply CreateResponsePositive(IDataAccess access, TicketUpdateRequest ticket) =>
            access.CreateNewTicket(ticket)
            ? new TicketUpdateReply { MessageId = ticket.MessageId,  Completed = false }
            : new TicketUpdateReply { MessageId = ticket.MessageId,  Completed = false };

        static void Main(string[] args)
        {            
            RunMailAgent();
        }
    }
}
