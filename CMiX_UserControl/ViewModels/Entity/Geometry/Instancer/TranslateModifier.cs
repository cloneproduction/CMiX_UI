using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class TranslateModifier : ViewModel, IUndoable
    {
        public TranslateModifier(string messageaddress, MessengerService messengerService, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(TranslateModifier)}/";
            MessengerService = messengerService;

            Translate = new AnimParameter(MessageAddress + nameof(Translate), messengerService, mementor, beat, true);
            TranslateX = new AnimParameter(MessageAddress + nameof(TranslateX), messengerService, mementor, beat, false);
            TranslateY = new AnimParameter(MessageAddress + nameof(TranslateY), messengerService, mementor, beat, false);
            TranslateZ = new AnimParameter(MessageAddress + nameof(TranslateZ), messengerService, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TranslateModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModifierModel"))
            {
                Mementor.BeginBatch();
                MessengerService.Disable();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.SetViewModel(translatemodifiermodel);

                MessengerService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(translatemodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
        }

        //public TranslateModifierModel GetModel()
        //{
        //    TranslateModifierModel translateModifierModel = new TranslateModifierModel();

        //    return translateModifierModel;
        //}

        public void Reset()
        {
            MessengerService.Disable();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            MessengerService.Enable();

            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(translatemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
