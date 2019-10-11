using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        #region CONSTRUCTORS
        public Geometry(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Geometry));
            Modifier = new Modifier(MessageAddress, oscvalidation, mementor, beat);

            Is3D = false;
            KeepAspectRatio = false;

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, oscvalidation, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, oscvalidation, mementor) { FileIsSelected = true, FileName = "Quad (default)" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, oscvalidation, mementor) { FileIsSelected = true, FileName = "Quad (default)" };

            //TranslateMode = new GeometryTranslate(MessageAddress + nameof(TranslateMode), oscvalidation, mementor);
            //ScaleMode = new GeometryScale(MessageAddress + nameof(ScaleMode), oscvalidation, mementor);
            //RotationMode = new GeometryRotation(MessageAddress + nameof(RotationMode), oscvalidation, mementor);

            

            //Translate = new Slider(MessageAddress + nameof(Translate), oscvalidation, mementor);
            //Scale = new Slider(MessageAddress + nameof(Scale), oscvalidation, mementor);
            //Scale.Amount = 0.25;
            //Rotation = new Slider(MessageAddress + nameof(Rotation), oscvalidation, mementor);


            Counter = new Counter(MessageAddress, oscvalidation, mementor);
            GeometryFX = new GeometryFX(MessageAddress, oscvalidation, mementor);

            Transform = new Transform(MessageAddress, oscvalidation, mementor);
            CopyGeometryCommand = new RelayCommand(p => CopyGeometry());
            PasteGeometryCommand = new RelayCommand(p => PasteGeometry());
            ResetGeometryCommand = new RelayCommand(p => ResetGeometry());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            FileSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(FileSelector)));

            //TranslateMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateMode)));
            //ScaleMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleMode)));
            //RotationMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationMode)));
            //Translate.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Translate)));
            //Scale.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Scale)));
            //Rotation.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Rotation)));

            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
            GeometryFX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(GeometryFX)));

            Modifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Modifier)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyGeometryCommand { get; }
        public ICommand PasteGeometryCommand { get; }
        public ICommand ResetGeometryCommand { get; }

        public FileSelector FileSelector { get; }
        public Counter Counter { get; }
        public Transform Transform { get; }

        public Modifier Modifier { get;  }

        //public GeometryTranslate TranslateMode { get; }
        //public Slider Translate { get; }
        //public GeometryRotation RotationMode { get; }
        //public Slider Rotation { get; }
        //public GeometryScale ScaleMode { get; }
        //public Slider Scale { get; }

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
        public void CopyGeometry()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Copy(geometrymodel);
            IDataObject data = new DataObject();
            data.SetData("GeometryModel", geometrymodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("GeometryModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var geometrymodel = data.GetData("GeometryModel") as GeometryModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(geometrymodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(geometrymodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(geometrymodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            GeometryModel geometrymodel = new GeometryModel();
            this.Reset();
            this.Copy(geometrymodel);
            QueueObjects(geometrymodel);
            SendQueues();
        }

        public void Copy(GeometryModel geometrymodel)
        {
            geometrymodel.MessageAddress = MessageAddress;
            FileSelector.Copy(geometrymodel.FileSelector);
            //TranslateMode.Copy(geometrymodel.GeometryTranslate);
            //ScaleMode.Copy(geometrymodel.GeometryScale);
            //RotationMode.Copy(geometrymodel.GeometryRotation);
            //Translate.Copy(geometrymodel.Translate);
            //Scale.Copy(geometrymodel.Scale);
            //Rotation.Copy(geometrymodel.Rotation);
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
            //Translate.Paste(geometrymodel.Translate);
            //Scale.Paste(geometrymodel.Scale);
            //Rotation.Paste(geometrymodel.Rotation);
            //TranslateMode.Paste(geometrymodel.GeometryTranslate);
            //ScaleMode.Paste(geometrymodel.GeometryScale);
            //RotationMode.Paste(geometrymodel.GeometryRotation);
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

            //TranslateMode.Reset();
            //ScaleMode.Reset();
            //RotationMode.Reset();

            //Translate.Reset();
            //Scale.Reset();
            //Rotation.Reset();
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