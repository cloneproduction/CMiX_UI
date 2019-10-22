using Ceras;
using Ceras.Helpers; // This is where the ceras.WriteToStream extensions are in
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.Message
{
    public class Client
    {
        TcpClient _client;
        NetworkStream _netStream;
        CerasSerializer _sendCeras;
        CerasSerializer _receiveCeras;

        public void Start()
        {
            _client = new TcpClient();
            _client.Connect("localhost", 5555);
            _netStream = _client.GetStream();
            var config = new SerializerConfig();
            config.Advanced.PersistTypeCache = true;

            _sendCeras = new CerasSerializer(config);
            _receiveCeras = new CerasSerializer(config);

            StartReceiving();

            //SendExampleObjects();
        }

        public void SendExampleObjects(object obj)
        {
            Send(obj);
        }

        void StartReceiving()
        {
            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        // Read until we received the next message from the server
                        var obj = await _receiveCeras.ReadFromStream(_netStream);
                        HandleMessage(obj);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client error while receiving: " + e);
                }
            });
        }

        void HandleMessage(object obj)
        {
            Console.WriteLine($"[Client] Received a '{obj.GetType().Name}': {obj}");
        }
        void Send(object obj) => _sendCeras.WriteToStream(_netStream, obj);
    }
}