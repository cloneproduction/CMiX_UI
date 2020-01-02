using Ceras;
using CMiX.MVVM.Message;

namespace CMiX.Engine.ViewModel
{
    public class Project
    {
        public Composition Composition { get; set; }
        public CerasSerializer Serializer { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public string MessageAddress { get; set; }

        public Project()
        {
            MessageAddress = "Project/";
            Serializer = new CerasSerializer();
            NetMQClient = new NetMQClient("127.0.0.1", 1111, "/Device0");
            NetMQClient.Start();
            Composition = new Composition(NetMQClient, MessageAddress, Serializer);
        }
    }
}