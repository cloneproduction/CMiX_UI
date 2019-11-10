using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Transform : ViewModel
    {
        public Transform(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base(serverValidations, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Transform));
            Translate = new Translate(MessageAddress, serverValidations, mementor);
            Scale = new Scale(MessageAddress, serverValidations, mementor);
            Rotation = new Rotation(MessageAddress, serverValidations, mementor);

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
                //SendMessages(MessageAddress + nameof(Is3D), Is3D.ToString());
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
                //this.SendMessages(nameof(TransformModel), transformmodel);
                //QueueObjects(transformmodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TransformModel transformmodel = new TransformModel();
            this.Reset();
            this.Copy(transformmodel);
            //this.SendMessages(nameof(TransformModel), transformmodel);
            //QueueObjects(transformmodel);
            //SendQueues();
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

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Is3D = false;
            //Mementor.EndBatch();
            EnabledMessages();

            TransformModel transformmodel = new TransformModel();
            this.Copy(transformmodel);
            //this.SendMessages(nameof(TransformModel), transformmodel);
            //QueueObjects(transformmodel);
            //SendQueues();
        }
        #endregion
    }
}