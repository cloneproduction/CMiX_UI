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

        public PublisherSocket Publisher { get; set; }

        public void StartPublisher()
        {
            Task.Factory.StartNew(() =>
            {
                using (Publisher = new PublisherSocket())
                {
                    Publisher.Options.SendHighWatermark = 1000;
                    Publisher.Bind("tcp://*:6666");

                    while (true)
                    {

                    }
                }
            }, TaskCreationOptions.LongRunning);
        }


        //Task.Factory.StartNew(lambda, TaskCreationOptions.LongRunning)
        //public void StartPublisher(CancellationToken token = CancellationToken.None)
        //while (!token?.IsCancellationRequested ?? true)


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
