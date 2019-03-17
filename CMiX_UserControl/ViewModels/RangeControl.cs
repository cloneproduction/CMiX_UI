using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel
    {
        #region CONSTRUCTORS
        public RangeControl(ObservableCollection<OSCMessenger> messengers, string layername, ActionManager actionmanager, Mementor mementor)
        : this(

            messageaddress: String.Format("{0}/", layername),
            messengers: messengers,
            range: new Slider(layername, messengers, actionmanager, mementor),
            modifier: ((RangeModifier)0).ToString(),
            actionmanager: actionmanager
          )
        { }

        public RangeControl
            (

                string messageaddress,
                ObservableCollection<OSCMessenger> messengers,
                Slider range,
                string modifier,
                ActionManager actionmanager
            )
            : base(actionmanager)
        {
            MessageAddress = messageaddress;
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
            Range = range ?? throw new ArgumentNullException(nameof(range));
            Modifier = modifier;
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
                SetAndNotify(ref _modifier, value);
                SendMessages(MessageAddress + nameof(Modifier), Modifier);
            }
        }
        #endregion

        #region COPY/PASTE
        public void Copy(RangeControlDTO rangecontroldto)
        {
            Range.Copy(rangecontroldto.Range);
            rangecontroldto.Modifier = Modifier;
        }

        public void Paste(RangeControlDTO rangecontroldto)
        {
            DisabledMessages();
            Range.Paste(rangecontroldto.Range);
            Modifier = rangecontroldto.Modifier;
            EnabledMessages();
        }
        #endregion
    }
}