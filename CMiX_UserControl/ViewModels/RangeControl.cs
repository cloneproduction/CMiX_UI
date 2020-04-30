using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class RangeControl : ViewModel
    {
        #region CONSTRUCTORS
        public RangeControl(string messageAddress)
        {
            MessageAddress = messageAddress + "/";
            Range = new Slider(MessageAddress + nameof(Range));
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
                //if(Mementor != null)
                //    Mementor.PropertyChange(this, nameof(Modifier));
                SetAndNotify(ref _modifier, value);
                //SendMessages(MessageAddress + nameof(Modifier), Modifier);
            }
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region COPY/PASTE


        //public void CopyModel(RangeControlModel rangeControlModel)
        //{
        //    Range.CopyModel(rangeControlModel.Range);
        //    rangeControlModel.Modifier = Modifier;
        //}

        public void Reset()
        {
            Modifier = ((RangeModifier)0).ToString();
            Range.Reset();
        }
        #endregion
    }
}