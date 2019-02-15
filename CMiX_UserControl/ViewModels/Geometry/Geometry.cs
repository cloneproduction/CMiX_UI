using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        #region CONSTRUCTORS
        public Geometry(string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Geometry)),
                fileselector: new FileSelector(messenger, String.Format("{0}/{1}/", layername, nameof(Geometry)), actionmanager),
                translatemode: new GeometryTranslate(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger, actionmanager),
                scalemode: new GeometryScale(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger, actionmanager),
                rotationmode: new GeometryRotation(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger, actionmanager),
                geometryfx : new GeometryFX(String.Format("{0}/{1}", layername, nameof(Geometry)), messenger, actionmanager),
                translateAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Translate"), messenger, actionmanager),
                scaleAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Scale"), messenger, actionmanager),
                rotationAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Rotation"), messenger, actionmanager),
                counter: new Counter(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Count"), messenger, actionmanager),
                is3D: false,    
                keepAspectRatio: false
            )
        { }

        public Geometry
            (
                ActionManager actionmanager,
                OSCMessenger messenger,
                FileSelector fileselector,
                GeometryTranslate translatemode,
                GeometryScale scalemode,
                GeometryRotation rotationmode,
                GeometryFX geometryfx,
                Slider translateAmount,
                Slider scaleAmount,
                Slider rotationAmount,
                Counter counter,
                bool is3D,
                bool keepAspectRatio,
                string messageaddress
            )
            : base (actionmanager, messenger)
        {
            FileSelector = fileselector ?? throw new ArgumentNullException(nameof(FileSelector));
            TranslateMode = translatemode ?? throw new ArgumentNullException(nameof(TranslateMode));
            RotationMode = rotationmode ?? throw new ArgumentNullException(nameof(RotationMode));
            ScaleMode = scalemode ?? throw new ArgumentNullException(nameof(ScaleMode));
            GeometryFX = geometryfx;
            TranslateAmount = translateAmount;
            RotationAmount = rotationAmount;
            ScaleAmount = scaleAmount;
            Counter = counter;
            Is3D = is3D;
            KeepAspectRatio = keepAspectRatio;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public FileSelector FileSelector { get; }
        public Counter Counter { get; }
        public GeometryTranslate TranslateMode { get; }
        public Slider TranslateAmount { get; }
        public GeometryRotation RotationMode { get; }
        public Slider RotationAmount { get; }
        public GeometryScale ScaleMode { get; }
        public Slider ScaleAmount { get; }
        public GeometryFX GeometryFX { get; }

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
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryDTO geometrydto)
        {
            FileSelector.Copy(geometrydto.FileSelector);
            TranslateMode.Copy(geometrydto.GeometryTranslate);
            ScaleMode.Copy(geometrydto.GeometryScale);
            RotationMode.Copy(geometrydto.GeometryRotation);
            TranslateAmount.Copy(geometrydto.TranslateAmount);
            ScaleAmount.Copy(geometrydto.ScaleAmount);
            RotationAmount.Copy(geometrydto.RotationAmount);
            geometrydto.Is3D = Is3D;
            geometrydto.KeepAspectRatio = KeepAspectRatio;
            GeometryFX.Copy(geometrydto.GeometryFX);
        }

        public void Paste(GeometryDTO geometrydto)
        {
            Messenger.SendEnabled = false;

            FileSelector.Paste(geometrydto.FileSelector);
            TranslateAmount.Paste(geometrydto.TranslateAmount);
            ScaleAmount.Paste(geometrydto.ScaleAmount);
            RotationAmount.Paste(geometrydto.RotationAmount);
            TranslateMode.Paste(geometrydto.GeometryTranslate);
            ScaleMode.Paste(geometrydto.GeometryScale);
            RotationMode.Paste(geometrydto.GeometryRotation);
            GeometryFX.Paste(geometrydto.GeometryFX);
            Is3D = geometrydto.Is3D;
            KeepAspectRatio = geometrydto.KeepAspectRatio;

            Messenger.SendEnabled = true;
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