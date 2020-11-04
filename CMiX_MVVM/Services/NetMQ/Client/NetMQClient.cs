using System;
using System.Threading.Tasks;
using CMiX.MVVM.Services;
using NetMQ;
using NetMQ.Sockets;

namespace CMiX.MVVM.Message
{
    public class NetMQClient
    {
        //public NetMQClient(string address, string topic)
        //{
        //    this.Address = address;
        //    Message = new Message();
        //    Topic = topic;
        //}


        //public Message Message { get; set; }
        //private NetMQActor actor;

        //private string _topic;
        //public string Topic
        //{
        //    get { return _topic; }
        //    set
        //    {
        //        _topic = value;
        //        ReStart();
        //    }
        //}


        //public string Address { get; set; }
        //public ClientShimHandler ClientShimHandler { get; set; }
        //SubscriberSocket SubscriberSocket { get; set; }
        //NetMQPoller Poller { get; set; }
        //public void Start()
        //{
        //    SubscriberSocket = new SubscriberSocket(Address);
        //    SubscriberSocket.Subscribe(Topic);
        //    SubscriberSocket.ReceiveReady += ClientSub_ReceiveReady;

        //    Poller = new NetMQPoller();
        //    Poller.Add(SubscriberSocket);
        //    Poller.RunAsync();
        //    Console.WriteLine($"NetMQClient Started with Address " + Address);
        //}


        //private void ClientSub_ReceiveReady(object sender, NetMQSocketEventArgs e)
        //{
        //    NetMQMessage msg = e.Socket.ReceiveMultipartMessage();

        //    if(msg.FrameCount == 3)
        //    {
        //        Message.NetMQMessage = msg;
        //        Console.WriteLine(Message.MessageAddress);
        //    }
        //}

        //public void Stop()
        //{
        //    if (actor != null)
        //    {
        //        actor.Dispose();
        //        actor = null;
        //    }
        //}

        //public void ReStart()
        //{
        //    if (actor != null)
        //    {
        //        Stop();
        //        Start();
        //    }
        //}
    }
}