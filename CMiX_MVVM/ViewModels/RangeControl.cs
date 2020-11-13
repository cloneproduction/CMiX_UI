using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Pouet : Sender
    {
        public Pouet()
        {
            Modifier = ((RangeModifier)0).ToString();
            Range = new Slider(nameof(Range));
        }

        public Slider Range { get; }

        private string _modifier;
        public string Modifier
        {
            get => _modifier;
            set => SetAndNotify(ref _modifier, value);
        }

        //public void Reset()
        //{
        //    Modifier = ((RangeModifier)0).ToString();
        //    Range.Reset();
        //}

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            
        }
    }
}