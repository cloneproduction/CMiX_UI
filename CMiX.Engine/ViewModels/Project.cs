using CMiX.MVVM.Message;
using CMiX.MVVM.Services;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Engine.ViewModels
{
    public class Project : IMessageReceiver
    {
        public Composition Composition { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }
        //public MessageService MessageService {get; set; }

        public Project()
        {
            MessageAddress = "Project/";
            //MessageService = new MessageService();
            
            //Composition = new Composition(MessageService, MessageAddress);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}