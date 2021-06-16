using CMiX.MVVM.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class ControlCommunicator : ICommunicator
    {
        public ControlCommunicator(Control control)
        {
            ID = control.ID;
        }


        private Guid ID { get; set; }
        public IMessageReceiver MessageReceiver { get; set; }
        public IMessageSender MessageSender { get; set; }


        //public IMessageProcessor MessageProcessor { get; set; }

        //public void ProcessMessage(Message message)
        //{
        //    MessageProcessor.ProcessMessage(message);
        //    MessageReceiver.ReceiveMessage(message);
        //    
        //    MessageSender.SendMessage(message);
        //}

        public void SetMessageProcessor(IMessageProcessor messageProcessor)
        {

        }


        public void SendMessageUpdateViewModel(Control control)
        {
            var message = new MessageUpdateViewModel(control);
            MessageSender.SendMessage(message);
        }

        public void SetCommunicator(ICommunicator communicator)
        {
            MessageReceiver = new MessageReceiver(ID);
            MessageReceiver.SetReceiver(communicator.MessageReceiver);

            MessageSender = new MessageSender(ID);
            MessageSender.SetSender(communicator.MessageSender);
        }

        public void UnsetCommunicator(ICommunicator communicator)
        {
            MessageReceiver.UnsetReceiver(communicator.MessageReceiver);
        }
    }
}