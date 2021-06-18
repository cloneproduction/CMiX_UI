using CMiX.MVVM.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class ControlCommunicator : Communicator
    {
        public ControlCommunicator(Control iDObject) : base()
        {
            IIDObject = iDObject;
            System.Console.WriteLine("Control COmmunicator Created for " + iDObject.GetType() + " " + iDObject.ID);
        }

        public void SendMessageUpdateViewModel(Control control)
        {
            var message = new MessageUpdateViewModel(control);
            this.SendMessage(message);
        }
    }
}