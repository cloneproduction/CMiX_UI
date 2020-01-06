using CMiX.MVVM.Message;
using CMiX.MVVM.Services;
using System.Collections.ObjectModel;

namespace CMiX.Engine.ViewModels
{
    public class Project
    {
        public Composition Composition { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }
        public MessageService MessageService {get; set; }

        public Project()
        {
            MessageAddress = "Project/";
            MessageService = new MessageService();
            MessageService.AddClient();


            Composition = new Composition(MessageService.CreateReceiver(), MessageAddress);
        }
    }
}