using CMiX.MVVM.Commands;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Messenger : ViewModel
    {
        public Messenger()
        {

        }

        private Server _server;
        public Server Server
        {
            get => _server;
            set => SetAndNotify(ref _server, value);
        }

        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                if(SelectedComponent != null)
                    SelectedComponent.Messengers.Remove(this);
                SetAndNotify(ref _selectedComponent, value);
                if (SelectedComponent != null)
                {
                    SelectedComponent.Messengers.Add(this);
                    System.Console.WriteLine("Selected Component Is " + SelectedComponent.Name);
                }
            }
        }

        public void SendMessage(string messageAddress)//, MessageCommand command, object parameter, object payload)
        {
            System.Console.WriteLine("Send message with address : " + messageAddress);
        }
    }
}
