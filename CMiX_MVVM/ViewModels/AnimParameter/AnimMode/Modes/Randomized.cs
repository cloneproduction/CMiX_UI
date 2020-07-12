using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Sendable, IAnimMode
    {
        public Randomized()
        {
            
        }
        public Randomized(Stopwatcher stopwatcher)
        {
            //Stopwatcher = stopwatcher;
            Random = new Random();
            //Stopwatcher.Change += new EventHandler(GenerateValue);
            //UpdateValue = new Action(Update);
        }

        //public RandomType RandomType { get; set; }
        public Random Random { get; set; }

        public double newValue { get; set; }
        public double oldValue { get; set; }


        public Range Range { get; set; }


        private EasingType _selectedEasingType;
        public EasingType SelectedEasingType
        {
            get => _selectedEasingType;
            set => SetAndNotify(ref _selectedEasingType, value);
        }

        public IEnumerable<EasingType> ModeTypeSource
        {
            get => Enum.GetValues(typeof(EasingType)).Cast<EasingType>();
        }

        public void Update()
        {
            //Console.WriteLine("Randomized Update");
        }

        private void GenerateValue(object sender, EventArgs e)
        {
            oldValue = newValue;
            newValue = Random.NextDouble();
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
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