using System.Windows;
using System.Windows.Controls;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class ComponentPanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerPanelTemplate { get; set; }
        public DataTemplate EntityPanelTemplate { get; set; }
        public DataTemplate CompositionPanelTemplate { get; set; }
        public DataTemplate ScenePanelTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;

            if(item != null)
            {
                if (item is Composition)
                    dataTemplate = CompositionPanelTemplate;
                else if (item is Layer)
                    dataTemplate = LayerPanelTemplate;
                else if (item is Scene)
                    dataTemplate = ScenePanelTemplate;
                else if (item is Entity)
                    dataTemplate = EntityPanelTemplate;
            }

            return dataTemplate;
        }
    }
}