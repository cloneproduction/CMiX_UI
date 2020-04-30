using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class RotationModifier : ViewModel
    {
        public RotationModifier(string messageaddress, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(RotationModifier)}/";

            Rotation = new AnimParameter(MessageAddress + nameof(Rotation), beat, true);
            RotationX = new AnimParameter(MessageAddress + nameof(RotationX), beat, false);
            RotationY = new AnimParameter(MessageAddress + nameof(RotationY), beat, false);
            RotationZ = new AnimParameter(MessageAddress + nameof(RotationZ), beat, false);
        }

        #region PROPERTIES
        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
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
                MessengerService.Disable();

                var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                //this.Paste(Rotationmodifiermodel);

                MessengerService.Enable();
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
