using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Services;
using CMiX.Controls;
using CMiX.Models;
using System.Windows;
using System.Windows.Input;
using MonitoredUndo;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public Geometry(string layername, IMessenger messenger)
            : this(
                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Geometry)),

                  count: 1,
                  geometrytranslate: new GeometryTranslate(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometryscale: new GeometryScale(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometryrotation: new GeometryRotation(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),
                  geometrypaths: new ObservableCollection<ListBoxFileName>(),
                  geometryfx : new GeometryFX(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger),

                  translateAmount: 0.0,
                  scaleAmount: 0.0,
                  rotationAmount: 0.0,
                  is3D: false,    
                  keepAspectRatio: false,

                  messageEnabled: true
                  )
        {
            GeometryPaths = new ObservableCollection<ListBoxFileName>();
            GeometryPaths.CollectionChanged += ContentCollectionChanged;
        }

        public Geometry(
            int count,
            IEnumerable<ListBoxFileName> geometrypaths,
            GeometryTranslate geometrytranslate,
            GeometryScale geometryscale,
            GeometryRotation geometryrotation,

            GeometryFX geometryfx,

            double translateAmount,
            double scaleAmount,
            double rotationAmount,
            bool is3D,
            bool keepAspectRatio,

            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
            if (geometrypaths == null)
            {
                throw new ArgumentNullException(nameof(geometrypaths));
            }

            Count = count;
            GeometryPaths = new ObservableCollection<ListBoxFileName>() ;
            GeometryPaths.CollectionChanged += ContentCollectionChanged;

            GeometryTranslate = geometrytranslate ?? throw new ArgumentNullException(nameof(geometryrotation));
            GeometryRotation = geometryrotation ?? throw new ArgumentNullException(nameof(geometryrotation));
            GeometryScale = geometryscale ?? throw new ArgumentNullException(nameof(geometryscale));

            GeometryFX = geometryfx;

            TranslateAmount = translateAmount;
            RotationAmount = rotationAmount;
            ScaleAmount = scaleAmount;

            Is3D = is3D;
            KeepAspectRatio = keepAspectRatio;

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;

            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public GeometryFX GeometryFX { get; }

        public GeometryTranslate GeometryTranslate { get; }

        public GeometryRotation GeometryRotation { get; }

        public GeometryScale GeometryScale { get; }

        [OSC]
        public ObservableCollection<ListBoxFileName> GeometryPaths { get; set; }

        private int _count;
        [OSC]
        public int Count
        {
            get => _count;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, nameof(Count), _count, value, "Count Changed");
                SetAndNotify(ref _count, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Count), Count);
            }
        }

        private double _translateAmount;
        [OSC]
        public double TranslateAmount
        {
            get => _translateAmount;
            set
            {
                DefaultChangeFactory.Current.OnChanging(this, nameof(TranslateAmount), _translateAmount, value, "TranslateAmount Changed");
                SetAndNotify(ref _translateAmount, value);
                if (MessageEnabled)
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
                DefaultChangeFactory.Current.OnChanging(this, nameof(ScaleAmount), _scaleAmount, value, "ScaleAmount Changed");
                SetAndNotify(ref _scaleAmount, value);
                if (MessageEnabled)
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
                DefaultChangeFactory.Current.OnChanging(this, nameof(RotationAmount), _rotationAmount, value, "RotationAmount Changed");
                SetAndNotify(ref _rotationAmount, value);
                if (MessageEnabled)
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
                DefaultChangeFactory.Current.OnChanging(this, nameof(Is3D), _is3D, value, "Is3D Changed");
                SetAndNotify(ref _is3D, value);
                if (MessageEnabled)
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
                DefaultChangeFactory.Current.OnChanging(this, nameof(KeepAspectRatio), _keepAspectRatio, value, "KeepAspectRatio Changed");
                SetAndNotify(ref _keepAspectRatio, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
            }
        }
        #endregion

        #region COLLECTIONCHANGED
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
            if(MessageEnabled)
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
            if(MessageEnabled)
                Messenger.SendMessage(MessageAddress + nameof(GeometryPaths), filename.ToArray());
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryDTO geometrydto)
        {
            geometrydto.Count = Count;

            foreach (ListBoxFileName lbfn in GeometryPaths)
            {
                geometrydto.GeometryPaths.Add(lbfn);
            }

            geometrydto.TranslateAmount = TranslateAmount;

            GeometryTranslate.Copy(geometrydto.GeometryTranslate);
            GeometryScale.Copy(geometrydto.GeometryScale);
            GeometryRotation.Copy(geometrydto.GeometryRotation);

            GeometryFX.Copy(geometrydto.GeometryFX);

            geometrydto.ScaleAmount = ScaleAmount;
            geometrydto.RotationAmount = RotationAmount;
            geometrydto.Is3D = Is3D;
            geometrydto.KeepAspectRatio = KeepAspectRatio;
        }

        public void Paste(GeometryDTO geometrydto)
        {
            MessageEnabled = false;

            Count = geometrydto.Count;

            GeometryPaths.Clear();
            foreach (ListBoxFileName lbfn in geometrydto.GeometryPaths)
            {
                GeometryPaths.Add(lbfn);
            }

            TranslateAmount = geometrydto.TranslateAmount;
            ScaleAmount = geometrydto.ScaleAmount;
            RotationAmount = geometrydto.RotationAmount;

            GeometryTranslate.Paste(geometrydto.GeometryTranslate);
            GeometryScale.Paste(geometrydto.GeometryScale);
            GeometryRotation.Paste(geometrydto.GeometryRotation);

            GeometryFX.Paste(geometrydto.GeometryFX);

            Is3D = geometrydto.Is3D;
            KeepAspectRatio = geometrydto.KeepAspectRatio;

            MessageEnabled = true;
        }

        public void CopySelf()
        {
            GeometryDTO geometrydto = new GeometryDTO();
            this.Copy(geometrydto);
            IDataObject data = new DataObject();
            data.SetData("Geometry", geometrydto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Geometry"))
            {
                var geometrydto = (GeometryDTO)data.GetData("Geometry") as GeometryDTO;
                this.Paste(geometrydto);

                Messenger.QueueObject(this);
                Messenger.SendQueue();
            }
        }

        public void ResetSelf()
        {
            GeometryDTO geometrydto = new GeometryDTO();
            this.Paste(geometrydto);
        }
        #endregion
    }
}