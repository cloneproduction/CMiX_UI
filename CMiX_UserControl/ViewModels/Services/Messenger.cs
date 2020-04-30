using CMiX.MVVM.Commands;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Messenger : ViewModel
    {
        public Messenger()
        {
            Server = new Server("Server Name", "localhost", 1111, "Device0");
            Server.Start();
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
                if(_selectedComponent != null)
                    _selectedComponent.SendChangeEvent -= Value_SendChangeEvent;

                SetAndNotify(ref _selectedComponent, value);

                if (SelectedComponent != null)
                    SelectedComponent.SendChangeEvent += Value_SendChangeEvent;
            }
        }

        private void Value_SendChangeEvent(object sender, MVVM.Services.ModelEventArgs e)
        {
            Server.Send(e.MessageAddress, e.Model);
        }
    }
}
