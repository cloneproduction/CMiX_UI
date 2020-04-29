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
                SetAndNotify(ref _selectedComponent, value);
                System.Console.WriteLine(SelectedComponent.Name);
            }
        }

        public void SendMessage(string messageAddress, MessageCommand command, object parameter, object payload)
        {
            
        }
    }
}
