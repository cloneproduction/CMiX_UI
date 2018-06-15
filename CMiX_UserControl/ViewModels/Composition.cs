using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel
    {
        public Composition()
        {
            Name = string.Empty;

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));

            MasterBeat = new MasterBeat(messenger);
            Camera = new Camera(messenger, MasterBeat);

            Layers = new ObservableCollection<Layer>
            {
                new Layer(MasterBeat, "LayerPouet", messenger),
                new Layer(MasterBeat, "LayerProut", messenger)
            };

            // TODO temporary
            CurrentLayer = new Layer(MasterBeat, "Layer0", messenger);
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            var messenger = new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555));
            CurrentLayer = new Layer(masterBeat, "Layer0", messenger); // TODO temporary

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

        public MasterBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }

        // TODO temporary
        public Layer CurrentLayer { get; }
    }
}