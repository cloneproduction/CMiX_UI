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
        public Geometry(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, ObservableCollection<OSCValidation> cansendmessage, Mementor mementor) : base (oscmessengers, cansendmessage, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Geometry));

            Is3D = false;
            KeepAspectRatio = false;

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, oscmessengers, cansendmessage, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(FileSelector.MessageAddress, oscmessengers, cansendmessage, mementor) { FileIsSelected = true, FileName = "Quad (default)" });

            TranslateMode = new GeometryTranslate(MessageAddress + nameof(TranslateMode), oscmessengers, cansendmessage, mementor);
            ScaleMode = new GeometryScale(MessageAddress + nameof(ScaleMode), oscmessengers, cansendmessage, mementor);
            RotationMode = new GeometryRotation(MessageAddress + nameof(RotationMode), oscmessengers, cansendmessage, mementor);
       
            Translate = new Slider(MessageAddress + nameof(Translate), oscmessengers, cansendmessage, mementor);
            Scale = new Slider(MessageAddress + nameof(Scale), oscmessengers, cansendmessage, mementor);
            Scale.Amount = 0.25;
            Rotation = new Slider(MessageAddress + nameof(Rotation), oscmessengers, cansendmessage, mementor);
            Counter = new Counter(MessageAddress, oscmessengers, cansendmessage, mementor);
            GeometryFX = new GeometryFX(MessageAddress, oscmessengers, cansendmessage, mementor);

            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            FileSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(FileSelector)));
            TranslateMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateMode)));
            ScaleMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleMode)));
            RotationMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationMode)));

            Translate.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Translate)));
            Scale.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Scale)));
            Rotation.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Rotation)));
            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
            GeometryFX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(GeometryFX)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }

        public FileSelector FileSelector { get; }
        public Counter Counter { get; }
        public GeometryTranslate TranslateMode { get; }
        public Slider Translate { get; }
        public GeometryRotation RotationMode { get; }
        public Slider Rotation { get; }
        public GeometryScale ScaleMode { get; }
        public Slider Scale { get; }
        public GeometryFX GeometryFX { get; }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "Is3D");
                SetAndNotify(ref _is3D, value);
                SendMessages(MessageAddress + nameof(Is3D), Is3D.ToString());
            }
        }

        private bool _keepAspectRatio;
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
            Translate.Copy(geometrymodel.Translate);
            Scale.Copy(geometrymodel.Scale);
            Rotation.Copy(geometrymodel.Rotation);
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
            Translate.Paste(geometrymodel.Translate);
            Scale.Paste(geometrymodel.Scale);
            Rotation.Paste(geometrymodel.Rotation);
            TranslateMode.Paste(geometrymodel.GeometryTranslate);
            ScaleMode.Paste(geometrymodel.GeometryScale);
            RotationMode.Paste(geometrymodel.GeometryRotation);
            GeometryFX.Paste(geometrymodel.GeometryFX);
            Counter.Paste(geometrymodel.Counter);
            Is3D = geometrymodel.Is3D;
            KeepAspectRatio = geometrymodel.KeepAspectRatio;

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            Is3D = false;
            KeepAspectRatio = false;

            FileSelector.Reset();

            TranslateMode.Reset();
            ScaleMode.Reset();
            RotationMode.Reset();

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Counter.Reset();
            GeometryFX.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            QueueObjects(geometrymodel);
            SendQueues();
        }
        #endregion
    }
}