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

            X = new Slider(MessageAddress + nameof(X), messageService, mementor);
            Y = new Slider(MessageAddress + nameof(Y), messageService, mementor);
            Z = new Slider(MessageAddress + nameof(Z), messageService, mementor);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public string MessageAddress { get ; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }


        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", this.GetModel(), false);
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
            ScaleModel scalemodel = this.GetModel();
            this.Reset();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }

        public void Paste(ScaleModel scalemodel)
        {
            MessageService.Disable();

            X.SetViewModel(scalemodel.X);
            Y.SetViewModel(scalemodel.Y);
            Z.SetViewModel(scalemodel.Z);
            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            ScaleModel scalemodel = this.GetModel();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }
        #endregion
    }
}
