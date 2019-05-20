using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.Services;
using CMiX.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", messageaddress, nameof(Geometry));
            FileSelector = new FileSelector("Single", new List<string> { ".FBX", ".OBJ" }, oscmessengers, String.Format("{0}/{1}/", messageaddress, nameof(Geometry)), mementor);
            TranslateMode = new GeometryTranslate(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            ScaleMode = new GeometryScale(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            RotationMode = new GeometryRotation(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            GeometryFX = new GeometryFX(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            TranslateAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Translate"), oscmessengers, mementor);
            ScaleAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Scale"), oscmessengers, mementor);
            RotationAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Rotation"), oscmessengers, mementor);
            Counter = new Counter(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Counter"), oscmessengers, mementor);
            Is3D = false;
            KeepAspectRatio = false;
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
                if(Mementor != null)
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
                if (Mementor != null)
                    Mementor.PropertyChange(this, "KeepAspectRatio");
                SetAndNotify(ref _keepAspectRatio, value);
                SendMessages(MessageAddress + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(GeometryModel geometrydto)
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

        public void Paste(GeometryModel geometrydto)
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
            GeometryModel geometrydto = new GeometryModel();
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
                var geometrydto = (GeometryModel)data.GetData("Geometry") as GeometryModel;
                this.Paste(geometrydto);

                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            GeometryModel geometrydto = new GeometryModel();
            this.Paste(geometrydto);
        }
        #endregion
    }
}