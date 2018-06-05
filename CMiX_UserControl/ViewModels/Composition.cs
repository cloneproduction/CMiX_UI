using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CMiX.ViewModels
{
    public class Composition : ViewModel
    {
        public Composition()
            : this(string.Empty, new Camera(), new MainBeat(), Enumerable.Empty<Layer>())
        { }

        public Composition(string name, Camera camera, MainBeat mainBeat, IEnumerable<Layer> layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException(nameof(layers));
            }

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

        public MainBeat MasterBeat { get; }

        public Camera Camera { get; }

        public ObservableCollection<Layer> Layers { get; }
    }
}