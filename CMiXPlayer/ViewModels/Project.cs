using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.ViewModels;

namespace CMiXPlayer.ViewModels
{
    public class Project : ViewModel
    {
        #region CONSTRUCTORS
        public Project()
        {
            Clients = new ObservableCollection<Client>();
            Serializer = new CerasSerializer();

            AddClientCommand = new RelayCommand(p => AddClient());
            DeleteClientCommand = new RelayCommand(p => DeleteClient(p));
        }
        #endregion

        #region METHODS
        private void AddClient()
        {
            var client = new Client(Serializer) { Name = "pouet" };
            Clients.Add(client);
        }

        private void DeleteClient(object client)
        {
            Clients.Remove(client as Client);
        }
        #endregion

        #region PROPERTIES
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }

        public CerasSerializer Serializer { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        #endregion
    }
}
