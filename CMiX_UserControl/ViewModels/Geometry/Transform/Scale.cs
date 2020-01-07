using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Scale : ViewModel, ISendable, IUndoable
    {
        public Scale(string messageaddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = $"{messageaddress}{nameof(Scale)}/";
            Sender = sender;

            ScaleX = new Slider(MessageAddress + nameof(ScaleX), sender, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), sender, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), sender, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }
        public string MessageAddress { get ; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }

        //public void UpdateMessageAddress(string messageaddress)
        //{
        //    MessageAddress = messageaddress;
        //    ScaleX.UpdateMessageAddress($"{messageaddress}{nameof(ScaleX)}/");
        //    ScaleY.UpdateMessageAddress($"{messageaddress}{nameof(ScaleY)}/");
        //    ScaleZ.UpdateMessageAddress($"{messageaddress}{nameof(ScaleZ)}/");
        //}

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
                Sender.Disable();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);
                //UpdateMessageAddress(messageaddress);
                this.Copy(scalemodel);

                Sender.Enable();
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
            ScaleX.CopyModel(scalemodel.ScaleX);
            ScaleY.CopyModel(scalemodel.ScaleY);
            ScaleZ.CopyModel(scalemodel.ScaleZ);
        }

        public void Paste(ScaleModel scalemodel)
        {
            Sender.Disable();

            ScaleX.PasteModel(scalemodel.ScaleX);
            ScaleY.PasteModel(scalemodel.ScaleY);
            ScaleZ.PasteModel(scalemodel.ScaleZ);
            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();
            //Mementor.BeginBatch();

            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            //Mementor.EndBatch();
            Sender.Enable();

            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
