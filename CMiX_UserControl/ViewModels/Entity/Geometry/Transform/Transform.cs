using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Transform : Sendable, IUndoable
    {
        public Transform(string messageAddress, MessengerService messengerService, Mementor mementor)
            : base(messageAddress, messengerService)
        {
            MessengerService = messengerService;

            Translate = new Translate(MessageAddress, messengerService, mementor);
            Scale = new Scale(MessageAddress, messengerService, mementor);
            Rotation = new Rotation(MessageAddress, messengerService, mementor);

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

        public Mementor Mementor { get; set; }
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
                MessengerService.Disable();;

                var transformmodel = data.GetData("TransformModel") as TransformModel;
                var messageaddress = MessageAddress;
                this.Paste(transformmodel);

                MessengerService.Enable();
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
            MessengerService.Disable();

            Translate.Paste(transformModel.TranslateModel);
            Scale.Paste(transformModel.ScaleModel);
            Rotation.Paste(transformModel.RotationModel);
            Is3D = transformModel.Is3D;

            MessengerService.Enable();
        }

        public void Reset()
        {
            MessengerService.Disable();;
            //Mementor.BeginBatch();

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();
            Is3D = false;
            //Mementor.EndBatch();
            MessengerService.Enable();

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