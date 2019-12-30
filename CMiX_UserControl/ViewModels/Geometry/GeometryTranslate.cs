using System;
using CMiX.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class GeometryTranslate : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public GeometryTranslate(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}/", messageaddress);
            MessageService = messageService;
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        private GeometryTranslateMode _Mode;
        public GeometryTranslateMode Mode
        {
            get => _Mode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessageService.DisabledMessages();
            Mode = default;
            MessageService.EnabledMessages();
        }

        public void Copy(GeometryTranslateModel geometrytranslatemodel)
        {
            geometrytranslatemodel.MessageAddress = MessageAddress;
            geometrytranslatemodel.Mode = Mode;
        }

        public void Paste(GeometryTranslateModel geometrytranslatemodel)
        {
            MessageService.DisabledMessages();
            MessageAddress = geometrytranslatemodel.MessageAddress;
            Mode = geometrytranslatemodel.Mode;
            MessageService.EnabledMessages();
        }
        #endregion
    }
}