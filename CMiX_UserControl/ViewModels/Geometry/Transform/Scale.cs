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
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", GetModel(), false);
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

                MessageService.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = GetModel();
            this.Reset();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }

        public ScaleModel GetModel()
        {
            ScaleModel scaleModel = new ScaleModel();
            scaleModel.ScaleX = ScaleX.GetModel();
            scaleModel.ScaleY = ScaleY.GetModel();
            scaleModel.ScaleZ = ScaleZ.GetModel();
            return scaleModel;
        }

        public void Paste(ScaleModel scalemodel)
        {
            MessageService.Disable();

            ScaleX.SetViewModel(scalemodel.ScaleX);
            ScaleY.SetViewModel(scalemodel.ScaleY);
            ScaleZ.SetViewModel(scalemodel.ScaleZ);
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

            ScaleModel scalemodel = GetModel();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
