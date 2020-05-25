using System.Windows;
using System.Windows.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class ComponentPanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerPanelTemplate { get; set; }
        public DataTemplate MaskPanelTemplate { get; set; }
        public DataTemplate EntityPanelTemplate { get; set; }
        public DataTemplate CompositionPanelTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is Composition)
                return CompositionPanelTemplate;
            else if (item != null && item is Layer)
                return LayerPanelTemplate;
            else if (item != null && item is Mask)
                return MaskPanelTemplate;
            else if (item != null && item is Entity)
                return EntityPanelTemplate;
            else
                return null;
        }
    }
}