using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel
    {
        public Composition()
        {
            Name = string.Empty;

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;


            MasterBeat = new MasterBeat(messenger);
            Camera = new Camera(messenger, MasterBeat);
            AddLayerCommand = new RelayCommand(p => AddLayer());
            RemoveLayerCommand = new RelayCommand(p => RemoveLayer());

            Layers = new ObservableCollection<Layer>
            {
                new Layer(MasterBeat, "LayerPouet", messenger),
                new Layer(MasterBeat, "LayerProut", messenger)
            };
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            Messenger = messenger;

            Name = name;
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            MasterBeat = masterBeat ?? throw new ArgumentNullException(nameof(masterBeat));
            Layers = new ObservableCollection<Layer>(layers);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private IMessenger Messenger { get; }

        public MasterBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }

        public ICommand AddLayerCommand { get; }
        public ICommand RemoveLayerCommand { get; }

        private int layerID = 0;

        private void AddLayer()
        {
            layerID += 1;
            Layers.Add(new Layer(MasterBeat, "Layer" + layerID.ToString(), Messenger));

            List<string> LayerNames = new List<string>();
            foreach(Layer lyr in this.Layers)
            {
                LayerNames.Add(lyr.LayerName);
            }
            Messenger.SendMessage("/LayerNames", LayerNames.ToArray());
        }

        private void RemoveLayer()
        {
            Layers.RemoveAt(Layers.Count - 1);
            /*for(int i = Layers.Count - 1; i > 0; i--)
            {
                if(Layers[i].Enabled == true)
                {
                    Layers.RemoveAt(i);
                }
            }*/

            List<string> LayerNames = new List<string>();
            foreach (Layer lyr in this.Layers)
            {
                LayerNames.Add(lyr.LayerName);
            }
            Messenger.SendMessage("/LayerNames", LayerNames.ToArray());
        }
    }
}