using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class RotationModifier : ViewModel, IUndoable
    {
        public RotationModifier(string messageaddress, MessengerService messengerService, Mementor mementor, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(RotationModifier)}/";
            MessengerService = messengerService;
            Rotation = new AnimParameter(MessageAddress + nameof(Rotation), messengerService, mementor, beat, true);
            RotationX = new AnimParameter(MessageAddress + nameof(RotationX), messengerService, mementor, beat, false);
            RotationY = new AnimParameter(MessageAddress + nameof(RotationY), messengerService, mementor, beat, false);
            RotationZ = new AnimParameter(MessageAddress + nameof(RotationZ), messengerService, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion


        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("RotationModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("RotationModifierModel"))
            {
                Mementor.BeginBatch();
                MessengerService.Disable();

                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                //this.Paste(Rotationmodifiermodel);

                MessengerService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(Rotationmodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }

        public void Reset()
        {
            MessengerService.Disable();

            Rotation.Reset();
            RotationX.Reset();
            RotationY.Reset();
            RotationZ.Reset();

            MessengerService.Enable();

            RotationModifierModel Rotationmodifiermodel = new RotationModifierModel();
            //this.Copy(Rotationmodifiermodel);
            //SendMessages(MessageAddress, Rotationmodifiermodel);
            //QueueObjects(Rotationmodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
