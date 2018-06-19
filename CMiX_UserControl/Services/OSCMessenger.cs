using SharpOSC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.Services
{
    public class OSCMessenger : IMessenger
    {
        public UDPSender Sender { get; }

        private readonly List<OscMessage> messages;

        public OSCMessenger(UDPSender sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            messages = new List<OscMessage>();
        }

        private OscMessage CreateOscMessage(string address, params object[] args) => new OscMessage(address, args.Select(a => a?.ToString()).ToArray());

        public void QueueMessage(string address, params object[] args)
        {
            var message = CreateOscMessage(address, args);
            messages.Add(message);
        }

        public void SendMessage(string address, params object[] args)
        {
            var message = CreateOscMessage(address, args);
            Sender.Send(message);
        }

        public void SendQueue()
        {
            var bundle = new OscBundle(0, messages.ToArray());
            Sender.Send(bundle);
            messages.Clear();
        }
    }
}
