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
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TransformModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TransformModel"))
            {
                Mementor.BeginBatch();
                MessageService.Disable();;

                var transformmodel = data.GetData("TransformModel") as TransformModel;
                var messageaddress = MessageAddress;
                this.Paste(transformmodel);

                MessageService.Enable();
                Mementor.EndBatch();
                //this.SendMessages(nameof(TransformModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //this.SendMessages(nameof(TransformModel), GetModel());
        }



        //public void Copy(TransformModel transformModel)
        //{
        //    Translate.Copy(transformModel.Translate);
        //    Scale.Copy(transformModel.Scale);
        //    Rotation.Copy(transformModel.Rotation);
        //    transformModel.Is3D = Is3D;
        //}

        public void Paste(TransformModel transformModel)
        {
            MessageService.Disable();

            Translate.Paste(transformModel.TranslateModel);
            Scale.Paste(transformModel.ScaleModel);
            Rotation.Paste(transformModel.RotationModel);
            Is3D = transformModel.Is3D;

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();;
            //Mementor.BeginBatch();

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Is3D = false;
            //Mementor.EndBatch();
            MessageService.Enable();

            //TransformModel transformmodel = GetModel();
            //this.SendMessages(nameof(TransformModel), transformmodel);
            //QueueObjects(transformmodel);
            //SendQueues();
        }


        //public TransformModel GetModel()
        //{
        //    TransformModel transformModel = new TransformModel();

        //    return transformModel;
        //}

        public void SetViewModel(TransformModel model)
        {
            
        }
        #endregion
    }
}