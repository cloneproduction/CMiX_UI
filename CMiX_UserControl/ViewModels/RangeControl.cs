using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel, ISendable, IUndoable
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
        public void Copy(RangeControlModel rangecontrolmodel)
        {
            rangecontrolmodel.MessageAddress = MessageAddress;
            Range.Copy(rangecontrolmodel.Range);
            rangecontrolmodel.Modifier = Modifier;
        }

        public void Paste(RangeControlModel rangecontrolmodel)
        {
            Messenger.Disable();

            MessageAddress = rangecontrolmodel.MessageAddress;
            Range.Paste(rangecontrolmodel.Range);
            Modifier = rangecontrolmodel.Modifier;

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