using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class RangeControl : Sendable
    {
        #region CONSTRUCTORS
        public RangeControl(string messageAddress)
        {
            Modifier = ((RangeModifier)0).ToString();
            Range = new Slider("Range", this);
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
        #endregion

        #region COPY/PASTE

        public void Reset()
        {
            Modifier = ((RangeModifier)0).ToString();
            Range.Reset();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            
        }
        #endregion
    }
}