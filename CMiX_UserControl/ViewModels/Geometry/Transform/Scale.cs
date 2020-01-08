using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Scale : ViewModel, ISendable, IUndoable
    {
        public Scale(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = $"{messageaddress}{nameof(Scale)}/";
            MessageService = messageService;

            ScaleX = new Slider(MessageAddress + nameof(ScaleX), messageService, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), messageService, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), messageService, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }
        public string MessageAddress { get ; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }


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
                MessageService.Disable();

                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodel);

                this.Copy(scalemodel);

                MessageService.Enable();
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
            MessageService.Disable();

            ScaleX.PasteModel(scalemodel.ScaleX);
            ScaleY.PasteModel(scalemodel.ScaleY);
            ScaleZ.PasteModel(scalemodel.ScaleZ);
            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            ScaleModel scalemodel = new ScaleModel();
            this.Copy(scalemodel);
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
