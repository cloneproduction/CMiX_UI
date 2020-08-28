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

            if (animParameter != null)
            {
                if (animParameter.SelectedModeType == ModeType.LFO)
                {
                    System.Console.WriteLine("DATATEMPLATE IS LFO");
                    dataTemplate = LFOTemplate;
                }
                    
                else if (animParameter.SelectedModeType == ModeType.Stepper)
                    dataTemplate = StepperTemplate;
                else if (animParameter.SelectedModeType == ModeType.Steady)
                    dataTemplate = SteadyTemplate;
                else if (animParameter.SelectedModeType == ModeType.Random)
                    dataTemplate = RandomizedTemplate;
                else if (animParameter.SelectedModeType == ModeType.None)
                    dataTemplate = NoneTemplate;
            }

            return dataTemplate;
        }
    }
}