using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Ceras;
using Ceras.Helpers;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.Message
{
    public class Messenger
    {
        public Messenger(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _netStream = tcpClient.GetStream();

            // We want to keep "learned" types
            // That means when the other side sends us a type (using the full name) we never want to transmit that again,
            // the type should (from then on) be known as a some ID.
            var config = new SerializerConfig();
            config.Advanced.PersistTypeCache = true;

            _sendCeras = new CerasSerializer(config);
            _receiveCeras = new CerasSerializer(config);

            StartReceivingMessages();
        }

        readonly TcpClient _tcpClient;
        readonly NetworkStream _netStream;
        readonly CerasSerializer _sendCeras;
        readonly CerasSerializer _receiveCeras;

        string _clientName;

        void StartReceivingMessages()
        {
            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        var obj = await _receiveCeras.ReadFromStream(_netStream);
                        HandleMessage(obj);
                    }
                }
                catch (Exception e)
                {
                    Log($"Error while handling client '{_tcpClient.Client.RemoteEndPoint}': {e}");
                }
            });
        }

        void HandleMessage(object obj)
        {
            if (obj is CompositionModel compmodel)
            {
                Send($"Composition Model received with Name '{compmodel.Name}'");
                return;
            }

            Log($"Just Received a '{obj.GetType().Name}': {obj}");
        }

        void Log(string text) => Console.WriteLine("[Server] " + text);

        void Send(object obj) => _sendCeras.WriteToStream(_netStream, obj);
    }
}

//if (obj is ClientLogin clientHello)
//{
//    _clientName = clientHello.Name;
//    Send($"Login ok, your name is now '{_clientName}'");
//    return;
//}

//if (obj is Person p)
//{
//    Log($"Got a person: {p.Name} ({p.Friends.Count} friends)");
//    return;
//}

//if (obj is List<ISpell> spells)
//{
//    Log($"We have received {spells.Count} spells:");
//    foreach (var spell in spells)
//        Log($"  {spell.GetType().Name}: {spell.Cast()}");

//    return;
//}

// If we have no clue how to handle something, we
// just print it out to the console