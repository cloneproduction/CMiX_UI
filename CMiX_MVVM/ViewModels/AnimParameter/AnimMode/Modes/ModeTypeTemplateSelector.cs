using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class ModeTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LFOTemplate { get; set; }
        public DataTemplate StepperTemplate { get; set; }
        public DataTemplate SteadyTemplate { get; set; }
        public DataTemplate RandomizedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;

            if (item != null)
            {
                if (item is LFO)
                    dataTemplate = LFOTemplate;
                else if (item is Stepper)
                    dataTemplate = StepperTemplate;
                else if (item is Steady)
                    dataTemplate = SteadyTemplate;
                else if (item is Randomized)
                    dataTemplate = RandomizedTemplate;
            }

            return dataTemplate;
        }
    }
}