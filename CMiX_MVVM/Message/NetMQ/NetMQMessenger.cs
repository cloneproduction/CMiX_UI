using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Ceras;
using CMiX.MVVM.Models;


namespace CMiX.MVVM.Message
{
    public class NetMQMessenger
    {
        public NetMQMessenger()
        {
            Serializer = new CerasSerializer();


        }
        
        
        public CerasSerializer Serializer { get; set; }

        //async Task ServerAsync()
        //{
        //    using (var server = new RouterSocket("inproc://async"))
        //    {
        //        for (int i = 0; i < 1000; i++)
        //        {
        //            var (routingKey, more) = await server.ReceiveRoutingKeyAsync();
        //            var (message, _) = await server.ReceiveFrameStringAsync();

        //            // TODO: process message

        //            await Task.Delay(100);
        //            server.SendMoreFrame(routingKey);
        //            server.SendFrame("Welcome");
        //        }
        //    }
        //}

        //async Task ClientAsync()
        //{
        //    using (var client = new DealerSocket("inproc://async"))
        //    {
        //        for (int i = 0; i < 1000; i++)
        //        {
        //            client.SendFrame("Hello");
        //            var (message, more) = await client.ReceiveFrameStringAsync();

        //            // TODO: process reply

        //            await Task.Delay(100);
        //        }
        //    }
        //}

        //public void StartMessenger()
        //{
        //    using (var runtime = new NetMQRuntime())
        //    {
        //        runtime.Run(ServerAsync(), ClientAsync());
        //    }
        //}



        PublisherSocket Publisher = new PublisherSocket();

        public void StartPublisher()
        {
            Task.Run(() =>
            {
                using (Publisher = new PublisherSocket())
                {
                    Publisher.Options.SendHighWatermark = 1000;
                    Publisher.Bind("tcp://*:6666");
                    //Task.Delay(100);
                    for (var i = 0; i < 100; i++)
                    {
                        Thread.Sleep(500);
                    }
                }
            });
        }

        public void Send(string topic, byte[] data)
        {
            Publisher.SendMoreFrame(topic).SendFrame(data);
        }

        public void SendDouble(string topic, double data)
        {
            Publisher.SendMoreFrame(topic).SendFrame(data.ToString());
            Console.WriteLine(data.ToString());
        }

        public static IList<string> allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };
        //public string ReceivedString { get; set; }

        private string _receivedString;
        public string ReceivedString
        {
            get { return _receivedString; }
            set
            {
                _receivedString = value;
                Console.WriteLine("ReceivedString " + ReceivedString);
            }
        }



        private byte[] _receivedData;
        public byte[] ReceivedData
        {
            get { return _receivedData; }
            set
            {
                _receivedData = value;
            }
        }

        public  void StartSubscriber()
        {
             Task.Run(() =>
            {
                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    subSocket.Connect("tcp://localhost:6666");
                    subSocket.Subscribe("testTopic");
                    Console.WriteLine("Subscriber started");
                    while (true)
                    {
                        string topic = subSocket.ReceiveFrameString();
                        string data = subSocket.ReceiveFrameString();

                        //ReceivedData = data;
                        //ProjectModel project = Serializer.Deserialize<ProjectModel>(data);
                        //Console.WriteLine("Project " + project.MessageAddress);
                        ReceivedString = data;
                    }
                }
            });
        }
    }
}
