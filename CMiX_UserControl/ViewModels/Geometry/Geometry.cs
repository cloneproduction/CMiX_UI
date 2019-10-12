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

            KeepAspectRatio = false;

            FileSelector = new FileSelector(MessageAddress, "Single", new List<string> { ".FBX", ".OBJ" }, oscvalidation, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, oscvalidation, mementor) { FileIsSelected = true, FileName = "Quad (default)" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, oscvalidation, mementor) { FileIsSelected = true, FileName = "Quad (default)" };

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
            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
            GeometryFX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(GeometryFX)));
            Modifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Modifier)));
            Transform.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Transform)));
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
        public GeometryFX GeometryFX { get; }

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
            Transform.Copy(geometrymodel.Transform);
            Counter.Copy(geometrymodel.Counter);
            GeometryFX.Copy(geometrymodel.GeometryFX);
        }

        public void Paste(GeometryModel geometrymodel)
        {
            DisabledMessages();

            MessageAddress = geometrymodel.MessageAddress;
            FileSelector.Paste(geometrymodel.FileSelector);
            Transform.Paste(geometrymodel.Transform);
            GeometryFX.Paste(geometrymodel.GeometryFX);
            Counter.Paste(geometrymodel.Counter);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            KeepAspectRatio = false;

            FileSelector.Reset();
            Transform.Reset();

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