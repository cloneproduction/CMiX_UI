using CMiX.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifierTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TranslateXYZTemplate { get; set; }
        public DataTemplate RandomizedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            //var animParameter = item as AnimParameter;

            if (item != null)
            {
                if (item is TranslateModifier)
                    dataTemplate = TranslateXYZTemplate;
                else if (item is Randomized)
                    dataTemplate = RandomizedTemplate;
            }

            return dataTemplate;
        }
    }
}
