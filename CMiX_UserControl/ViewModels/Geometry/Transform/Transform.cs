using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Transform : ViewModel, ISendable, IUndoable
    {
        public Transform(string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(Transform)}/";
            MessageService = messageService;

            Translate = new Translate(MessageAddress, messageService, mementor);
            Scale = new Scale(MessageAddress, messageService, mementor);
            Rotation = new Rotation(MessageAddress, messageService, mementor);

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

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
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
                Sender.Disable();;

                var transformmodel = data.GetData("TransformModel") as TransformModel;
                var messageaddress = MessageAddress;
                this.Paste(transformmodel);
                //UpdateMessageAddress(messageaddress);
                this.Copy(transformmodel);

                Sender.Enable();
                Mementor.EndBatch();
                //this.SendMessages(nameof(TransformModel), transformmodel);
            }
        }

        public void ResetGeometry()
        {
            TransformModel transformmodel = new TransformModel();
            this.Reset();
            this.Copy(transformmodel);
            //this.SendMessages(nameof(TransformModel), transformmodel);
        }

        public void Copy(TransformModel transformModel)
        {
            Translate.Copy(transformModel.Translate);
            Scale.Copy(transformModel.Scale);
            Rotation.Copy(transformModel.Rotation);
            transformModel.Is3D = Is3D;
        }

        public void Paste(TransformModel transformModel)
        {
            Sender.Disable();

            Translate.Paste(transformModel.Translate);
            Scale.Paste(transformModel.Scale);
            Rotation.Paste(transformModel.Rotation);
            Is3D = transformModel.Is3D;

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();;
            //Mementor.BeginBatch();

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Is3D = false;
            //Mementor.EndBatch();
            Sender.Enable();

            TransformModel transformmodel = new TransformModel();
            this.Copy(transformmodel);
            //this.SendMessages(nameof(TransformModel), transformmodel);
            //QueueObjects(transformmodel);
            //SendQueues();
        }
        #endregion
    }
}