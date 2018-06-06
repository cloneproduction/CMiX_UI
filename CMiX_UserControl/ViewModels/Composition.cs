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
            : this(
                  name: string.Empty,
                  camera: new Camera(),
                  mainBeat: new MasterBeat(new OSCMessenger(new SharpOSC.UDPSender("127.0.0.1", 55555))),
                  layers: Enumerable.Empty<Layer>())
        { }

        public Composition(string name, Camera camera, MasterBeat mainBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

            CurrentLayer = new Layer(); // TODO temporary

            Name = name;
            Camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            MasterBeat = mainBeat ?? throw new System.ArgumentNullException(nameof(mainBeat));
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