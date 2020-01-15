using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class RangeControl : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public RangeControl(string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = messageAddress + "/";
            MessageService = messageService;
            Range = new Slider(MessageAddress + nameof(Range), messageService, mementor);
            Modifier = ((RangeModifier)0).ToString();
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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE
        public RangeControlModel GetModel()
        {
            RangeControlModel rangeControlModel = new RangeControlModel();
            rangeControlModel.Range = Range.GetModel();
            rangeControlModel.Modifier = Modifier;
            return rangeControlModel;
        }

        //public void CopyModel(RangeControlModel rangeControlModel)
        //{
        //    Range.CopyModel(rangeControlModel.Range);
        //    rangeControlModel.Modifier = Modifier;
        //}

        public void SetViewModel(RangeControlModel rangeControlModel)
        {
            MessageService.Disable();

            Range.SetViewModel(rangeControlModel.Range);
            Modifier = rangeControlModel.Modifier;

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            Modifier = ((RangeModifier)0).ToString();
            Range.Reset();
            MessageService.Enable();
        }
        #endregion
    }
}