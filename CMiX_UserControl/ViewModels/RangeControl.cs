using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public RangeControl(string messageAddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = messageAddress + "/";
            Range = new Slider(MessageAddress + nameof(Range), messenger, mementor);
            Modifier = ((RangeModifier)0).ToString();
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            Range.UpdateMessageAddress(messageaddress + nameof(Range) + "/");
        }
        #endregion

        #region PROPERTIES
        public Slider Range { get; }

        private string _modifier;
        public string Modifier
        {
            get => _modifier;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(Modifier));
                SetAndNotify(ref _modifier, value);
                //SendMessages(MessageAddress + nameof(Modifier), Modifier);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE
        public void CopyModel(IModel model)
        {
            RangeControlModel rangeControlModel = model as RangeControlModel;
            rangeControlModel.MessageAddress = MessageAddress;
            Range.CopyModel(rangeControlModel.Range);
            rangeControlModel.Modifier = Modifier;
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            RangeControlModel rangeControlModel = model as RangeControlModel;
            MessageAddress = rangeControlModel.MessageAddress;
            Range.PasteModel(rangeControlModel.Range);
            Modifier = rangeControlModel.Modifier;

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            Modifier = ((RangeModifier)0).ToString();
            Range.Reset();
            Messenger.Enable();
        }
        #endregion
    }
}