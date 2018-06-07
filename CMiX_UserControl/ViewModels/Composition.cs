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
            Layers = new ObservableCollection<Layer>(Enumerable.Empty<Layer>());

            // TODO temporary
            CurrentLayer = new Layer(MasterBeat);
        }

        public Composition(string name, Camera camera, MasterBeat masterBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            CurrentLayer = new Layer(masterBeat); // TODO temporary

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