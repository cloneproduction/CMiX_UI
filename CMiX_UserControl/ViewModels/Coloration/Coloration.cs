using System;
using System.Windows.Input;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class Coloration : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Coloration(string messageAddress, Messenger messenger, Mementor mementor, Beat masterbeat) 
        {
            MessageAddress = $"{messageAddress}{nameof(Coloration)}/";
            Messenger = messenger;
            Mementor = mementor;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messenger, mementor);
            ColorSelector = new ColorSelector(MessageAddress, messenger, mementor);

            Hue = new RangeControl(MessageAddress + nameof(Hue), messenger, mementor);
            Saturation = new RangeControl(MessageAddress + nameof(Saturation), messenger, mementor);
            Value = new RangeControl(MessageAddress + nameof(Value), messenger, mementor);

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public ColorSelector ColorSelector { get; }
        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            ColorSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(ColorSelector)));
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            Hue.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Hue)));
            Saturation.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Saturation)));
            Value.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Value)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyModel(IModel model)
        {
            ColorationModel colorationModel = model as ColorationModel;
            colorationModel.MessageAddress = MessageAddress;
            ColorSelector.CopyModel(colorationModel.ColorSelectorModel);
            BeatModifier.CopyModel(colorationModel.BeatModifierModel);
            Hue.CopyModel(colorationModel.HueDTO);
            Saturation.CopyModel(colorationModel.SatDTO);
            Value.CopyModel(colorationModel.ValDTO);
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            ColorationModel colorationModel = model as ColorationModel;
            MessageAddress = colorationModel.MessageAddress;
            ColorSelector.PasteModel(colorationModel.ColorSelectorModel);
            BeatModifier.PasteModel(colorationModel.BeatModifierModel);
            Hue.PasteModel(colorationModel.HueDTO);
            Saturation.PasteModel(colorationModel.SatDTO);
            Value.PasteModel(colorationModel.ValDTO);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();

            Messenger.Enable();
        }

        public void CopyColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            this.CopyModel(colorationmodel);
            IDataObject data = new DataObject();
            data.SetData("ColorationModel", colorationmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColoration()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorationModel"))
            {
                this.Mementor.BeginBatch();
                this.Messenger.Disable();

                var colorationmodel = data.GetData("ColorationModel") as ColorationModel;
                var colorationmessageaddress = MessageAddress;
                this.PasteModel(colorationmodel);
                this.UpdateMessageAddress(colorationmessageaddress);

                this.CopyModel(colorationmodel);
                this.Messenger.Enable();
                this.Mementor.EndBatch();

                //SendMessages(nameof(ColorationModel), colorationmodel);
            }
        }

        public void ResetColoration()
        {
            ColorationModel colorationmodel = new ColorationModel();
            this.Reset();
            this.CopyModel(colorationmodel);
            //SendMessages(nameof(Coloration), colorationmodel);
        }
        #endregion
    }
}