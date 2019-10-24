using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Ceras;
using CMiX.MVVM.Models;
using System.Windows;

namespace CMiX.MVVM.Message
{
    public class NetMQMessenger
    {
        public NetMQMessenger()
        {
            Serializer = new CerasSerializer();
        }
        PublisherSocket Publisher = new PublisherSocket();
        public CerasSerializer Serializer { get; set; }

        public void StartPublisher()
        {
            Random rand = new Random(50);
            Task.Run(() =>
            {
                using (Publisher = new PublisherSocket())
                {
                    Console.WriteLine("Publisher socket binding...");
                    Publisher.Options.SendHighWatermark = 1000;
                    Publisher.Bind("tcp://*:6666");
                    for (var i = 0; i < 100; i++)
                    {
                        //var randomizedTopic = rand.NextDouble();
                        //if (randomizedTopic > 0.5)
                        //{
                        //    var msg = "testTopic msg-" + i;
                        //    Console.WriteLine("Sending message : {0}", msg);
                        //    //Publisher.SendMoreFrame("testTopic").SendFrame(msg);
                        //}
                        //else
                        //{
                        //    var msg = "TopicB msg-" + i;
                        //    Console.WriteLine("Sending message : {0}", msg);
                        //    //Publisher.SendMoreFrame("TopicB").SendFrame(msg);
                        //}
                        Thread.Sleep(500);
                    }
                }
            });
        }

        public void Send(string topic, byte[] data)
        {
            Publisher.SendMoreFrame(topic).SendFrame(data, data.Length);
        }

        //public void Send(string topic, string data)
        //{
        //    Publisher.SendMoreFrame(topic).SendFrame(data);
        //}

        public static IList<string> allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };
        public string ReceivedString { get; set; }
        private byte[] _receivedData;
        public byte[] ReceivedData
        {
            get { return _receivedData; }
            set {
                _receivedData = value;
                Console.WriteLine(ReceivedData.Length.ToString());
            }
        }

        public async void StartSubscriber ()
        {
            await Task.Run(() =>
            {
                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 1000;
                    subSocket.Connect("tcp://localhost:6666");
                    subSocket.Subscribe("testTopic");

                    while (true)
                    {
                        byte[] data = subSocket.ReceiveFrameBytes();
                        ReceivedString = data.Length.ToString();
                        ReceivedData = data;
                        //SliderModel slider = Serializer.Deserialize<SliderModel>(data);
                    }

                }
            });
        }
    }
}
