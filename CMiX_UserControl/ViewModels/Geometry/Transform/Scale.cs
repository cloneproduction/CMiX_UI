using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Scale : ViewModel, ISendable, IUndoable
    {
        public Scale(string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = $"{messageaddress}{nameof(Scale)}/";
            Messenger = messenger;

            ScaleX = new Slider(MessageAddress + nameof(ScaleX), messenger, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), messenger, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), messenger, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }
        public string MessageAddress { get ; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            ScaleX.UpdateMessageAddress($"{messageaddress}{nameof(ScaleX)}/");
            ScaleY.UpdateMessageAddress($"{messageaddress}{nameof(ScaleY)}/");
            ScaleZ.UpdateMessageAddress($"{messageaddress}{nameof(ScaleZ)}/");
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", scalemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModel"))
            {
                Mementor.BeginBatch();
                Messenger.Disable();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(scalemodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), scalemodel);
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = new ScaleModel();
            this.Reset();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
        }

        public void Copy(ScaleModel scalemodel)
        {
            scalemodel.MessageAddress = MessageAddress;
            ScaleX.CopyModel(scalemodel.ScaleX);
            ScaleY.CopyModel(scalemodel.ScaleY);
            ScaleZ.CopyModel(scalemodel.ScaleZ);
        }

        public void Paste(ScaleModel scalemodel)
        {
            Messenger.Disable();

            MessageAddress = scalemodel.MessageAddress;
            ScaleX.PasteModel(scalemodel.ScaleX);
            ScaleY.PasteModel(scalemodel.ScaleY);
            ScaleZ.PasteModel(scalemodel.ScaleZ);
            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            //Mementor.BeginBatch();

            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            //Mementor.EndBatch();
            Messenger.Enable();

            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
