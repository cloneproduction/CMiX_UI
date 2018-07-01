using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Services;
using CMiX.Controls;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel, IMessengerData
    {
        public Geometry(string layername, IMessenger messenger)
            : this(
                  messenger: messenger,
                  messageaddress : String.Format("{0}/{1}/", layername, nameof(Geometry)),
                  count: 1,
                  geometrytranslate: new GeometryTranslate(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometryscale: new GeometryScale(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometryrotation: new GeometryRotation(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometrypaths: new ObservableCollection<ListBoxFileName>(),
                  translateAmount: 0.0,
                  scaleAmount: 0.0,
                  rotationAmount: 0.0,
                  is3D: false,    
                  keepAspectRatio: false)
        {
            GeometryPaths = new ObservableCollection<ListBoxFileName>();
            GeometryPaths.CollectionChanged += ContentCollectionChanged;
        }

        public Geometry(
            IMessenger messenger,
            string messageaddress,
            int count,
            IEnumerable<ListBoxFileName> geometrypaths,
            GeometryTranslate geometrytranslate,
            GeometryScale geometryscale,
            GeometryRotation geometryrotation,
            double translateAmount,
            double scaleAmount,
            double rotationAmount,
            bool is3D,
            bool keepAspectRatio)
        {
            if (geometrypaths == null)
            {
                throw new ArgumentNullException(nameof(geometrypaths));
            }

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;

            Count = count;

            GeometryPaths = new ObservableCollection<ListBoxFileName>() ;
            GeometryPaths.CollectionChanged += ContentCollectionChanged;

            GeometryTranslate = geometrytranslate ?? throw new ArgumentNullException(nameof(geometryrotation));
            GeometryRotation = geometryrotation ?? throw new ArgumentNullException(nameof(geometryrotation));
            GeometryScale = geometryscale ?? throw new ArgumentNullException(nameof(geometryscale));

            TranslateAmount = translateAmount;
            RotationAmount = rotationAmount;
            ScaleAmount = scaleAmount;

            Is3D = is3D;
            KeepAspectRatio = keepAspectRatio;
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }


        private int _count;
        [OSC]
        public int Count
        {
            get => _count;
            set
            {
                SetAndNotify(ref _count, value);
                Messenger.SendMessage(MessageAddress + nameof(Count), Count);
            }
        }

        [OSC]
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
            Messenger.SendMessage(MessageAddress + nameof(GeometryPaths), filename.ToArray());
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
            Messenger.SendMessage(MessageAddress + nameof(GeometryPaths), filename.ToArray());
        }

        public GeometryTranslate GeometryTranslate { get; }

        public GeometryRotation GeometryRotation { get; }

        public GeometryScale GeometryScale { get; }

        private double _translateAmount;
        [OSC]
        public double TranslateAmount
        {
            get => _translateAmount;
            set
            {
                SetAndNotify(ref _translateAmount, value);
                Messenger.SendMessage(MessageAddress + nameof(TranslateAmount), TranslateAmount);
            }
        }

        private double _scaleAmount;
        [OSC]
        public double ScaleAmount
        {
            get => _scaleAmount;
            set
            {
                SetAndNotify(ref _scaleAmount, value);
                Messenger.SendMessage(MessageAddress + nameof(ScaleAmount), ScaleAmount);
            }
        }

        private double _rotationAmount;
        [OSC]
        public double RotationAmount
        {
            get => _rotationAmount;
            set
            {
                SetAndNotify(ref _rotationAmount, value);
                Messenger.SendMessage(MessageAddress + nameof(RotationAmount), RotationAmount);
            }
        }

        private bool _is3D;
        [OSC]
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                Messenger.SendMessage(MessageAddress + nameof(Is3D), Is3D.ToString());
            }
        }

        private bool _keepAspectRatio;
        [OSC]
        public bool KeepAspectRatio
        {
            get => _keepAspectRatio;
            set
            {
                SetAndNotify(ref _keepAspectRatio, value);
                Messenger.SendMessage(MessageAddress + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
            }
        }
    }
}