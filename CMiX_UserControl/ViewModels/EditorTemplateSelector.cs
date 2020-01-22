using System.Windows;
using System.Windows.Controls;

namespace CMiX.Studio.ViewModels
{
    public class EditorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerTemplate { get; set; }
        public DataTemplate EntityTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is Layer)
                return LayerTemplate;
            else if (item != null && item is Entity)
                return EntityTemplate;
            else
                return null;
        }
    }
}