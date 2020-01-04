using CMiX.MVVM.Message;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class Project
    {
        public Composition Composition { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public Project()
        {
            MessageAddress = "Project/";
            Receiver = new Receiver(new Client("name", "127.0.0.1", 1111, "/Device0"));
            Receiver.Client.Start();
            Composition = new Composition(Receiver, MessageAddress);
        }
    }
}