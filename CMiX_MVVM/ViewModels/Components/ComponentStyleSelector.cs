using System.Windows;
using System.Windows.Controls;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentStyleSelector : StyleSelector
    {
        public Style ProjectTemplate { get; set; }
        public Style LayerTemplate { get; set; }
        public Style SceneTemplate { get; set; }
        public Style EntityTemplate { get; set; }
        public Style CompositionTemplate { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style style = null;

            if (item != null)
            {
                if (item is Project)
                    style = ProjectTemplate;
                else if (item is Composition)
                    style = CompositionTemplate;
                else if (item is Layer)
                    style = LayerTemplate;
                else if (item is Scene)
                    style = SceneTemplate;
                else if (item is Entity)
                    style = EntityTemplate;
            }

            return style;
        }
    }
}