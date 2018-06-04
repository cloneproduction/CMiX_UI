using CMiX.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMiX
{
    public class Composition : ViewModel
    {
        public Composition()
            : this(new Camera(), new MainBeat(), Enumerable.Empty<Layer>())
        { }

        public Composition(Camera camera, MainBeat mainBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new System.ArgumentNullException(nameof(layers));
            }

            Camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            MainBeat = mainBeat ?? throw new System.ArgumentNullException(nameof(mainBeat));
            Layers = new ObservableCollection<Layer>(layers);
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set => this.SetAndNotify(ref _Name, value);
        }

        MainBeat _MainBeat;
        public MainBeat MainBeat
        {
            get => _MainBeat;
            set => this.SetAndNotify(ref _MainBeat, value);
        }

        Camera _Camera;
        public Camera Camera
        {
            get => _Camera;
            set => this.SetAndNotify(ref _Camera, value);
        }

        public ObservableCollection<Layer> Layers { get; }
    }
}
