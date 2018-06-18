using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Services;
using CMiX.Controls;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        public Geometry(string layername, IMessenger messenger)
            : this(
                  layerName: layername,
                  messenger: messenger,
                  count: 1,
                  geometryscale: new GeometryScale(layername + "/" + nameof(Geometry), messenger),
                  geometryrotation: new GeometryRotation(layername + "/" + nameof(Geometry), messenger),
                  geometryPaths: new ObservableCollection<ListBoxFileName>(),
                  translateMode: default,
                  translateAmount: 0.0,
                  scaleMode: default,
                  scaleAmount: 0.0,
                  rotationAmount: 0.0,
                  is3D: false,
                              
                  keepAspectRatio: false)
        {

            GeometryPaths = new ObservableCollection<ListBoxFileName>();
            GeometryPaths.CollectionChanged += ContentCollectionChanged;

        }

        public Geometry(

            string layerName,
            IMessenger messenger,
            GeometryRotation geometryrotation,
            GeometryScale geometryscale,
            int count,
            IEnumerable<ListBoxFileName> geometryPaths,
            GeometryTranslateMode translateMode,
            double translateAmount,
            GeometryScaleMode scaleMode,
            double scaleAmount,
            double rotationAmount,
            bool is3D,
            bool keepAspectRatio)
        {

            if (geometryPaths == null)
            {
                throw new ArgumentNullException(nameof(geometryPaths));
            }

            LayerName = layerName;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Count = count;

            GeometryPaths = new ObservableCollection<ListBoxFileName>() ;
            GeometryPaths.CollectionChanged += ContentCollectionChanged;
            GeometryRotation = geometryrotation ?? throw new ArgumentNullException(nameof(geometryrotation));
            GeometryScale = geometryscale ?? throw new ArgumentNullException(nameof(geometryscale));

            RotationAmount = rotationAmount;

            TranslateMode = translateMode;
            TranslateAmount = translateAmount;

            ScaleAmount = scaleAmount;



            Is3D = is3D;
            KeepAspectRatio = keepAspectRatio;
        }

        private string Address => String.Format("{0}/{1}/", LayerName, nameof(Geometry));

        public IMessenger Messenger { get; }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                SetAndNotify(ref _count, value);
                Messenger.SendMessage(Address + nameof(Count), Count);
            }
        }

        public ObservableCollection<ListBoxFileName> GeometryPaths { get; }


        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ListBoxFileName item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ListBoxFileName item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in GeometryPaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            Messenger.SendMessage(Address + nameof(GeometryPaths), filename.ToArray());
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in GeometryPaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            Messenger.SendMessage(Address + nameof(GeometryPaths), filename.ToArray());
        }


        public GeometryRotation GeometryRotation { get; }
        public GeometryScale GeometryScale { get; }

        private GeometryTranslateMode _translateMode;
        public GeometryTranslateMode TranslateMode
        {
            get => _translateMode;
            set => SetAndNotify(ref _translateMode, value);
        }

        private double _translateAmount;
        public double TranslateAmount
        {
            get => _translateAmount;
            set
            {
                SetAndNotify(ref _translateAmount, value);
                Messenger.SendMessage(Address + nameof(TranslateAmount), TranslateAmount);
            }
        }

        private double _scaleAmount;
        public double ScaleAmount
        {
            get => _scaleAmount;
            set
            {
                SetAndNotify(ref _scaleAmount, value);
                Messenger.SendMessage(Address + nameof(ScaleAmount), ScaleAmount);
            }
        }

        private double _rotationAmount;
        public double RotationAmount
        {
            get => _rotationAmount;
            set
            {
                SetAndNotify(ref _rotationAmount, value);
                Messenger.SendMessage(Address + nameof(RotationAmount), RotationAmount);
            }
        }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                Messenger.SendMessage(Address + nameof(Is3D), Is3D.ToString());
            }
        }

        private bool _keepAspectRatio;
        public bool KeepAspectRatio
        {
            get => _keepAspectRatio;
            set
            {
                SetAndNotify(ref _keepAspectRatio, value);
                Messenger.SendMessage(Address + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
            }
        }
    }
}
