using System;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel
    {
        #region CONSTRUCTORS
        public RangeControl(ObservableCollection<OSCMessenger> oscmessengers, string messageaddress, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = messageaddress + "/";
            Range = new Slider(MessageAddress + nameof(Range), oscmessengers, mementor);
            Modifier = ((RangeModifier)0).ToString();
        }

        /*public RangeControl(ObservableCollection<OSCMessenger> messengers, string layername, Mementor mementor)
        : this
            (
                messageaddress: String.Format("{0}/", layername),
                mementor: mementor,
                messengers: messengers,
                range: new Slider(layername, messengers, mementor),
                modifier: ((RangeModifier)0).ToString()
            )
        { }

        public RangeControl
            (
                Mementor mementor,
                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                Slider range,
                string modifier
            )
            : base(messengers, mementor)
        {
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            Range = range ?? throw new ArgumentNullException(nameof(range));
            Modifier = modifier;
            Mementor = mementor;
        }*/
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
                SendMessages(MessageAddress + nameof(Modifier), Modifier);
            }
        }
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
            DisabledMessages();
            MessageAddress = rangecontrolmodel.MessageAddress;
            Range.Paste(rangecontrolmodel.Range);
            Modifier = rangecontrolmodel.Modifier;
            EnabledMessages();
        }
        #endregion
    }
}