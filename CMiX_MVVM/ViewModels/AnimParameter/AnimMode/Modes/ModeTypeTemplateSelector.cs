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
        public DataTemplate NoneTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            var animParameter = item as AnimParameter;

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
                else if (item is None)
                    dataTemplate = NoneTemplate;
            }

            return dataTemplate;
        }
    }
}