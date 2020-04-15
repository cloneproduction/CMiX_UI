using Memento;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Root : Component
    {
        public Root(int id, string messageAddress, Beat beat, MessageService messageService, Mementor mementor)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            Servers = new ObservableCollection<Server>();
            ServerManager = new ServerManager(Servers);
        }

        public ObservableCollection<Server> Servers { get; set; }
        public ServerManager ServerManager { get; set; }
    }
}