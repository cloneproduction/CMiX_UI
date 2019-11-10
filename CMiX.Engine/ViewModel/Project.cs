using Ceras;
using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Message;
using System.Collections.Generic;
using NetMQ;

namespace CMiX.Engine.ViewModel
{
    public class Project
    {
        public Composition Composition { get; set; }
        public CerasSerializer Serializer { get; set; }
        public NetMQClient NetMQClient { get; set; }

        public Project()
        {
            Serializer = new CerasSerializer();
            NetMQClient = new NetMQClient("127.0.0.1", 7777, "/Composition");
            NetMQClient.Start();
            Composition = new Composition(NetMQClient, "/Composition", Serializer);
        }
    }
}