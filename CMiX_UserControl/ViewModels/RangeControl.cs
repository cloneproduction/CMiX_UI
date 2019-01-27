using System;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class RangeControl : ViewModel
    {
        #region CONSTRUCTORS
        public RangeControl(OSCMessenger messenger, string layername, ActionManager actionmanager)
        : this(
            actionmanager: actionmanager,
            messenger: messenger,
            range: new Slider(layername, messenger, actionmanager),
            modifier: ((RangeModifier)0).ToString(),
            messageaddress: String.Format("{0}/", layername)
          )
        { }

        public RangeControl
            (
                ActionManager actionmanager,
                OSCMessenger messenger,
                Slider range,
                string modifier,
                string messageaddress
            )
            : base(actionmanager)
        {
            Range = range ?? throw new ArgumentNullException(nameof(range));
            Modifier = modifier;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public Slider Range { get; }

        private string _modifier;
        [OSC]
        public string Modifier
        {
            get => _modifier;
            set
            {
                SetAndNotify(ref _modifier, value);
                //Messenger.SendMessage(MessageAddress + nameof(Modifier), Modifier);
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
            Messenger.SendEnabled = false;
            Range.Paste(rangecontroldto.Range);
            Modifier = rangecontroldto.Modifier;
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}