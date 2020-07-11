using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Mode, IAnimMode
    {
        public Randomized()
        {
            
        }
        public Randomized(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            Random = new Random();
            Stopwatcher.Change += new EventHandler(GenerateValue);
            //UpdateValue = new Action(Update);
            RandomType = RandomType.Jump;
        }

        public RandomType RandomType { get; set; }
        public Random Random { get; set; }

        public double newValue { get; set; }
        public double oldValue { get; set; }


        public Range Range { get; set; }
        public EasingType EasingType { get; set; }

        public void Update()
        {
            //Console.WriteLine("Randomized Update");
        }

        private void GenerateValue(object sender, EventArgs e)
        {
            oldValue = newValue;
            newValue = Random.NextDouble();
        }

        //public void Update()
        //{
        //    switch (this.RandomType)
        //    {
        //        case RandomType.Jump:
        //            {
        //                ParameterValue = newValue;
        //                break;
        //            }
        //        case RandomType.Linear:
        //            {
        //                //ParameterValue = VMath.Lerp(oldValue, newValue, Stopwatcher.LFO);
        //                break;
        //            }
        //        default: break;
        //    }
        //}
    }
}