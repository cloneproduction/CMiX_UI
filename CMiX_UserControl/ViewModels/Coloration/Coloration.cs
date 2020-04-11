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
    public class Coloration : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Coloration(string messageAddress, MessageService messageService, Mementor mementor, Beat masterbeat) 
        {
            MessageAddress = $"{messageAddress}{nameof(Coloration)}/";
            MessageService = messageService;
            Mementor = mementor;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, messageService, mementor);
            ColorSelector = new ColorSelector(MessageAddress, messageService, mementor);

            Hue = new RangeControl(MessageAddress + nameof(Hue), messageService, mementor);
            Saturation = new RangeControl(MessageAddress + nameof(Saturation), messageService, mementor);
            Value = new RangeControl(MessageAddress + nameof(Value), messageService, mementor);

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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET

        //public void CopyModel(ColorationModel colorationModel)
        //{
        //    ColorSelector.CopyModel(colorationModel.ColorSelectorModel);
        //    BeatModifier.CopyModel(colorationModel.BeatModifierModel);
        //    Hue.CopyModel(colorationModel.HueDTO);
        //    Saturation.CopyModel(colorationModel.SatDTO);
        //    Value.CopyModel(colorationModel.ValDTO);
        //}

        public void SetViewModel(ColorationModel colorationModel)
        {
            MessageService.Disable();

            ColorSelector.SetViewModel(colorationModel.ColorSelectorModel);
            BeatModifier.SetViewModel(colorationModel.BeatModifierModel);
            Hue.SetViewModel(colorationModel.HueModel);
            Saturation.SetViewModel(colorationModel.SatModel);
            Value.SetViewModel(colorationModel.ValModel);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();

            MessageService.Enable();
        }

        public void CopyColoration()
        {
            IDataObject data = new DataObject();
            data.SetData("ColorationModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColoration()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorationModel"))
            {
                this.Mementor.BeginBatch();
                this.MessageService.Disable();

                var colorationmodel = data.GetData("ColorationModel") as ColorationModel;
                this.SetViewModel(colorationmodel);

                this.MessageService.Enable();
                this.Mementor.EndBatch();

                //SendMessages(nameof(ColorationModel), GetModel());
            }
        }

        public void ResetColoration()
        {
            this.Reset();
            //SendMessages(nameof(Coloration), GetModel());
        }
        #endregion
    }
}