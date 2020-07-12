using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels
{
    public class Stepper : Sendable, IAnimMode 
    {
        public Stepper()
        {

        }
        public Stepper(Stopwatcher stopwatcher)
        {
            //Stopwatcher = stopwatcher;
            //Stopwatcher.Change += new EventHandler(MovePosition);
            SelectedEasingType = EasingType.None;
            CurrentStepPos = 0.0;
            StepCount = 4;
            //UpdateValue = new Action(Update);
        }

        public int StepCount { get; set; }

        public double CurrentStepPos { get; set; }
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

        private void MovePosition(object sender, EventArgs e)
        {
            //CurrentStepPos += 1 / (Convert.ToDouble(StepCount) - 1);
            //if (CurrentStepPos > 1.0)
            //    CurrentStepPos = 0.0;
            //ParameterValue = CurrentStepPos;
        }

        public void Update()
        {
            //Console.WriteLine("Stepper Update");
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}