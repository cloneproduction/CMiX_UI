using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    [Serializable]
    public class ScaleModifier : ViewModel
    {
        public ScaleModifier(string messageaddress, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(ScaleModifier)}/";

            Scale = new AnimParameter(MessageAddress + nameof(Scale), beat, true);
            ScaleX = new AnimParameter(MessageAddress + nameof(ScaleX), beat, false);
            ScaleY = new AnimParameter(MessageAddress + nameof(ScaleY), beat, false);
            ScaleZ = new AnimParameter(MessageAddress + nameof(ScaleZ), beat, false);
        }

        #region PROPERTIES
        public AnimParameter Scale { get; set; }
        public AnimParameter ScaleX { get; set; }
        public AnimParameter ScaleY { get; set; }
        public AnimParameter ScaleZ { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("ScaleModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModifierModel"))
            {
                //Mementor.BeginBatch();


                var Scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
                var messageaddress = MessageAddress;
                //this.Paste(Scalemodifiermodel);

                //Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(Scalemodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(Scalemodifiermodel);
            //SendQueues();
        }

        public void Reset()
        {
            MessengerService.Disable();

            Scale.Reset();
            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            MessengerService.Enable();
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(Scalemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}