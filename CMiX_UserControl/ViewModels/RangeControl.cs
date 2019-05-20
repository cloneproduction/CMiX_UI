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
        public RangeControl(ObservableCollection<OSCMessenger> messengers, string layername, Mementor mementor)
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
        }
        #endregion

        #region PROPERTIES
        public Slider Range { get; }

        private string _modifier;
        [OSC]
        public string Modifier
        {
            get => _modifier;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Modifier");
                SetAndNotify(ref _modifier, value);
                SendMessages(MessageAddress + nameof(Modifier), Modifier);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(RangeControlModel rangecontroldto)
        {
            Range.Copy(rangecontroldto.Range);
            rangecontroldto.Modifier = Modifier;
        }

        public void Paste(RangeControlModel rangecontroldto)
        {
            DisabledMessages();
            Range.Paste(rangecontroldto.Range);
            Modifier = rangecontroldto.Modifier;
            EnabledMessages();
        }
        #endregion
    }
}