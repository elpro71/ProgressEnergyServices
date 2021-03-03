using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public struct TicketUpdateRequest
    {
        public string eMail { get; set; }
        public uint MessageId { get; set; }
        public string Message { get;  set; }
    }

    public struct TicketUpdateReply
    {
        public bool Completed { get; set; }
        public int? TicketNumber { get; set; }
        public uint MessageId { get; set; }

    }
}
