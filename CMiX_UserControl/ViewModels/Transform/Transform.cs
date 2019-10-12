using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using Memento;
using CMiX.MVVM.Models;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Transform : ViewModel
    {
        public Transform(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Transform));
            Translate = new Translate(MessageAddress, oscvalidation, mementor);
            Scale = new Scale(MessageAddress, oscvalidation, mementor);
            Rotation = new Rotation(MessageAddress, oscvalidation, mementor);

            Is3D = false;
        }

        #region PROPERTY
        public Translate Translate { get; }
        public Scale Scale { get; }
        public Rotation Rotation { get; }

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
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Translate.UpdateMessageAddress(messageaddress);
            Scale.UpdateMessageAddress(messageaddress);
            Rotation.UpdateMessageAddress(messageaddress);
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            TransformModel transformmodel = new TransformModel();
            this.Copy(transformmodel);
            IDataObject data = new DataObject();
            data.SetData("TransformModel", transformmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TransformModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var transformmodel = data.GetData("TransformModel") as TransformModel;
                var messageaddress = MessageAddress;
                this.Paste(transformmodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(transformmodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(transformmodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TransformModel transformmodel = new TransformModel();
            this.Reset();
            this.Copy(transformmodel);
            QueueObjects(transformmodel);
            SendQueues();
        }

        public void Copy(TransformModel transformmodel)
        {
            transformmodel.MessageAddress = MessageAddress;
            Translate.Copy(transformmodel.Translate);
            Scale.Copy(transformmodel.Scale);
            Rotation.Copy(transformmodel.Rotation);
            transformmodel.Is3D = Is3D;
        }

        public void Paste(TransformModel transformmodel)
        {
            DisabledMessages();

            MessageAddress = transformmodel.MessageAddress;

            Translate.Paste(transformmodel.Translate);
            Scale.Paste(transformmodel.Scale);
            Rotation.Paste(transformmodel.Rotation);

            Is3D = transformmodel.Is3D;

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();

            Is3D = false;

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            TransformModel transformmodel = new TransformModel();
            this.Copy(transformmodel);
            QueueObjects(transformmodel);
            SendQueues();
        }
        #endregion
    }
}