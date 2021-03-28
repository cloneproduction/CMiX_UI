//using Ceras;
//using CMiX.MVVM.Services;
//using CMiX.MVVM.ViewModels.Components;
//using CMiX.MVVM.ViewModels.Components.Messages;
//using CMiX.Studio.ViewModels.MessageService;
//using System;
//using System.Collections.Generic;

//namespace CMiX.MVVM.ViewModels.MessageService
//{
//    public class MessageTerminal : ViewModel
//    {
//        public MessageTerminal()
//        {
//            MessageSender = new MessageSender();
//            MessageReceiver = new MessageReceiver();
//            //MessageReceiver.MessageReceived += Receiver_MessageReceived;

//            var config = new SerializerConfig();// TargetMember.AllPublic | TargetMember.AllPrivate };
//            Serializer = new CerasSerializer(config);
//            Components = new List<Component>();
//        }

//        public List<Component> Components { get; set; }


//        public void RegisterComponent(Component component)
//        {
//            this.Components.Add(component);
//        }

//        public void UnregisterComponent(Component component)
//        {
//            this.Components.Remove(component);
//        }


//        //public event EventHandler<MessageEventArgs> MessageReceived;
//        //private void OnMessageReceived(object sender, MessageEventArgs e) => MessageReceived?.Invoke(sender, e);


//        private void Receiver_MessageReceived(object sender, MessageEventArgs e)
//        {
//            Console.WriteLine("MessageReceived " + e.Message.ID);
//            var message = e.Message;
//            foreach (var component in Components)
//            {
//                if (message is IComponentMessage && message.ID == component.ID)
//                {
//                    //message.Process(component);
//                    Console.WriteLine("Message Processed");
//                }
//                else
//                {
//                    //component.DispatchMessage(message);
//                    Console.WriteLine("Message Dispatched");
//                }
//            }
//        }


//        private CerasSerializer Serializer { get; set; }
//        public MessageSender MessageSender { get; set; }
//        public MessageReceiver MessageReceiver { get; set; }


//        public void StartReceiver(Settings settings)
//        {
//            MessageReceiver.Start(settings);
//        }

//        public void SendMessage(string address, IMessage message)
//        {
//            if (MessageSender.HasMessengerRunning)
//            {
//                byte[] data = Serializer.Serialize(message);
//                //MessageSender.SendMessage(address, data);
//            }
//        }
//    }
//}
