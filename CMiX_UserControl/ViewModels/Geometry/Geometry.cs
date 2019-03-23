using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        #region CONSTRUCTORS
        public Geometry(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
            : this
            (
                mementor: mementor,
                messengers: messengers,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Geometry)),
                fileselector: new FileSelector("Single", new List<string> { ".FBX", ".OBJ" }, messengers, String.Format("{0}/{1}/", layername, nameof(Geometry)), mementor),
                translatemode: new GeometryTranslate(String.Format("{0}/{1}", layername, nameof(Geometry)), messengers, mementor),
                scalemode: new GeometryScale(String.Format("{0}/{1}", layername, nameof(Geometry)), messengers, mementor),
                rotationmode: new GeometryRotation(String.Format("{0}/{1}", layername, nameof(Geometry)), messengers, mementor),
                geometryfx : new GeometryFX(String.Format("{0}/{1}", layername, nameof(Geometry)), messengers, mementor),
                translateAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Translate"), messengers, mementor),
                scaleAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Scale"), messengers, mementor),
                rotationAmount: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Rotation"), messengers, mementor),
                counter: new Counter(String.Format("{0}/{1}/{2}", layername, nameof(Geometry), "Counter"), messengers, mementor),
                is3D: false,    
                keepAspectRatio: false
            )
        { }

        public Geometry
            (
                Mementor mementor,
                ObservableCollection<OSCMessenger> messengers,
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
            : base (messengers)
        {
            Mementor = mementor;
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
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
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
                Mementor.PropertyChange(this, "Is3D");
                SetAndNotify(ref _is3D, value);
                SendMessages(MessageAddress + nameof(Is3D), Is3D.ToString());
            }
        }

        private bool _keepAspectRatio;
        [OSC]
        public bool KeepAspectRatio
        {
            get => _keepAspectRatio;
            set
            {
                Mementor.PropertyChange(this, "KeepAspectRatio");
                SetAndNotify(ref _keepAspectRatio, value);
                SendMessages(MessageAddress + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
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
            DisabledMessages();

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

            EnabledMessages();
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

                QueueObjects(this);
                SendQueues();
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