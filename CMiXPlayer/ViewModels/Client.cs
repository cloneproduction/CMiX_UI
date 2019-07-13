using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memento;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using Ceras;

namespace CMiXPlayer.ViewModels
{
    public class Client : ViewModel
    {
        public Client(CerasSerializer cerasSerializer)
        {
            CompoSelector = new FileSelector(string.Empty,"Single", new List<string>() { ".COMPMIX" }, new ObservableCollection<OSCValidation> (), new Mementor());
            CompoSelector.SelectedFileNameItem = new FileNameItem(string.Empty, new ObservableCollection<OSCValidation>(), new Mementor());
            OSCMessenger = new OSCMessenger("127.0.0.1", 1111);
            Serializer = cerasSerializer;

            SendCompositionCommand = new RelayCommand(p => SendComposition());
            ResetClientCommand = new RelayCommand(p => ResetClient());
        }

        public void SendComposition()
        {
            if(CompoSelector.SelectedFileNameItem.FileName != null)
            {
                var filename = CompoSelector.SelectedFileNameItem.FileName;
                byte[] data = File.ReadAllBytes(filename);
                CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                OSCMessenger.SendMessage("/CompositionReloaded", true);
                OSCMessenger.QueueObject(compositionmodel);
                OSCMessenger.SendQueue();
            }
        }

        public void ResetClient()
        {
            OSCMessenger.SendMessage("/CompositionReloaded", true);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CerasSerializer Serializer { get; set; }

        public ICommand SendCompositionCommand { get; }
        public ICommand ResetClientCommand { get; }

        public FileSelector CompoSelector { get; set; }

        public OSCMessenger OSCMessenger { get; set; }
    }
}
