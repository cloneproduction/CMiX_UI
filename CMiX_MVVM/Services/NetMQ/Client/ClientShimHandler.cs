﻿using NetMQ;
using NetMQ.Sockets;
using System;

namespace CMiX.MVVM.Services
{
    public class ClientShimHandler : IShimHandler
    {
        private PairSocket shim;
        private NetMQPoller poller;
        private SubscriberSocket subscriber;
        //private Message Message;
        private string Address;
        private string Topic;


        public ClientShimHandler(Message.Message byteMessage, string address, string topic)
        {
            //this.Message = byteMessage;
            this.Address = address;
            this.Topic = topic;
        }

        public void Initialise(object state)
        {
        }

        const int MegaByte = 1024;
        public void Run(PairSocket shim)
        {
            using (subscriber = new SubscriberSocket())
            using (poller = new NetMQPoller())
            {
                subscriber.Connect(Address);
                subscriber.Subscribe(Topic);
                subscriber.Options.ReceiveHighWatermark = 1000;
                subscriber.Options.MulticastRate = 1000 * MegaByte;
                subscriber.ReceiveReady += OnSubscriberReady;

                this.shim = shim;
                shim.SignalOK();
                poller.Add(subscriber);
                poller.Add(this.shim);
                poller.RunAsync();
            }
        }

        private void OnSubscriberReady(object sender, NetMQSocketEventArgs e)
        {
            Console.WriteLine("OnSubscriberReady");
            var message = e.Socket.ReceiveMultipartMessage();
            Console.WriteLine(message.FrameCount);
            shim.SendMultipartMessage(message);
            OnReceiveChange(message);
            //Message.NetMQMessage = e.Socket.ReceiveMultipartMessage();
        }

        public event EventHandler<NetMQMessageEventArgs> ReceiveChangeEvent;
        public void OnReceiveChange(NetMQMessage netMQMessage)
        {
            ReceiveChangeEvent?.Invoke(this, new NetMQMessageEventArgs(netMQMessage));
        }

        //private void OnShimReady(object sender, NetMQSocketEventArgs e)
        //{
        //    Console.WriteLine("OnShimReady");
        //    string command = e.Socket.ReceiveFrameString();
        //    if (command == NetMQActor.EndShimMessage)
        //        poller.Stop();
        //}
    }
}
