using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Sendable, IAnimMode
    {
        public Randomized()
        {
            Easing = new Easing(this);
            Random = new Random();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public Random Random { get; set; }
        public double newValue { get; set; }
        public double oldValue { get; set; }


        public Range Range { get; set; }

        public Easing Easing { get; set; }

        private void GenerateValue(object sender, EventArgs e)
        {
            oldValue = newValue;
            newValue = Random.NextDouble();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}