using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Sendable, IAnimMode
    {
        public Randomized()
        {
            Random = new Random();
        }

        public Randomized(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        public Random Random { get; set; }
        public double newValue { get; set; }
        public double oldValue { get; set; }

        private void GenerateValue(object sender, EventArgs e)
        {
            oldValue = newValue;
            newValue = Random.NextDouble();
        }

        public void Update()
        {

        }
    }
}