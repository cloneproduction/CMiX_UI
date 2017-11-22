using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOSC;
using System.Windows;

namespace CMiX
{
    public sealed class Singleton : IDisposable
    {
        private UDPListener _listener;

        public delegate void MessageReceivedEventHandler(string message);
        public event MessageReceivedEventHandler MessageReceived;

        private Singleton()
        {
            // The cabllback function
            HandleOscPacket callback = delegate (OscPacket packet)
            {
                var messageReceived = (OscMessage)packet;
                MessageReceived?.Invoke("Received a message!");
            };

            _listener = new UDPListener(1234, callback);
        }

        public static Singleton Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Singleton instance = new Singleton();
        }

        public void Dispose()
        {
            _listener.Close();
        }
    }
}
