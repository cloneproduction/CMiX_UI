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
            FileSelector = new FileSelector("Single", new List<string> { ".FBX", ".OBJ" }, oscmessengers, String.Format("{0}/{1}", messageaddress, nameof(Geometry)), mementor);

            TranslateMode = new GeometryTranslate(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            ScaleMode = new GeometryScale(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            RotationMode = new GeometryRotation(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);

            GeometryFX = new GeometryFX(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
            TranslateAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Translate"), oscmessengers, mementor);
            ScaleAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Scale"), oscmessengers, mementor);
            RotationAmount = new Slider(String.Format("{0}/{1}/{2}", messageaddress, nameof(Geometry), "Rotation"), oscmessengers, mementor);
            Counter = new Counter(String.Format("{0}/{1}", messageaddress, nameof(Geometry)), oscmessengers, mementor);
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
        public void Copy(GeometryModel geometrymodel)
        {
            geometrymodel.MessageAddress = MessageAddress;
            FileSelector.Copy(geometrymodel.FileSelector);
            TranslateMode.Copy(geometrymodel.GeometryTranslate);
            ScaleMode.Copy(geometrymodel.GeometryScale);
            RotationMode.Copy(geometrymodel.GeometryRotation);
            TranslateAmount.Copy(geometrymodel.TranslateAmount);
            ScaleAmount.Copy(geometrymodel.ScaleAmount);
            RotationAmount.Copy(geometrymodel.RotationAmount);
            Counter.Copy(geometrymodel.Counter);
            geometrymodel.Is3D = Is3D;
            geometrymodel.KeepAspectRatio = KeepAspectRatio;
            GeometryFX.Copy(geometrymodel.GeometryFX);
        }

        public void Paste(GeometryModel geometrymodel)
        {
            DisabledMessages();

            MessageAddress = geometrymodel.MessageAddress;
            FileSelector.Paste(geometrymodel.FileSelector);
            TranslateAmount.Paste(geometrymodel.TranslateAmount);
            ScaleAmount.Paste(geometrymodel.ScaleAmount);
            RotationAmount.Paste(geometrymodel.RotationAmount);
            TranslateMode.Paste(geometrymodel.GeometryTranslate);
            ScaleMode.Paste(geometrymodel.GeometryScale);
            RotationMode.Paste(geometrymodel.GeometryRotation);
            GeometryFX.Paste(geometrymodel.GeometryFX);
            Counter.Paste(geometrymodel.Counter);
            Is3D = geometrymodel.Is3D;
            KeepAspectRatio = geometrymodel.KeepAspectRatio;

            EnabledMessages();
        }

        public void CopySelf()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            IDataObject data = new DataObject();
            data.SetData("Geometry", geometrymodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Geometry"))
            {
                var geometrymodel = (GeometryModel)data.GetData("Geometry") as GeometryModel;
                this.Paste(geometrymodel);

                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Paste(geometrymodel);
        }
        #endregion
    }
}