using DataAccess;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMA_MailAgent
{
    internal class MimeVisitor : MimeKit.MimeVisitor
    {
        private MailboxAddress _sender;
        private uint _id;
        private System.IO.MemoryStream _stream;

        internal MimeVisitor(int messageId)
        {
            _id = (uint)messageId;
            _stream = new System.IO.MemoryStream();
        }

        protected override void VisitTextRfc822Headers(TextRfc822Headers entity)
        {
            entity.Headers.WriteTo(FormatOptions.Default, _stream);
            base.VisitTextRfc822Headers(entity);
        }

        protected override void VisitBody(MimeMessage message)
        {
            _sender = message.Sender;
            message.Body.WriteTo(FormatOptions.Default, _stream);
            base.VisitBody(message);
        }

        internal TicketUpdateRequest ReadEmail()
        {
            _stream.Position = 0;
            var reader = new System.IO.StreamReader(_stream);
            return new TicketUpdateRequest
            {
                eMail = _sender.Address,
                MessageId = _id,
                Message = reader.ReadToEnd()
            };
        }
    }
}
