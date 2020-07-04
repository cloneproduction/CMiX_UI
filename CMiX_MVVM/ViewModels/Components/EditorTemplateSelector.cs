using System.Windows;
using System.Windows.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class EditorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerTemplate { get; set; }
        public DataTemplate EntityTemplate { get; set; }
        public DataTemplate CompositionTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ////System.Console.WriteLine("DataTemplateSelector is " + item.GetType()); ;
            if (item != null && item is Composition)
                return CompositionTemplate;
            else if (item != null && item is Layer)
                return LayerTemplate;
            else if (item != null && item is Entity)
                return EntityTemplate;
            else
                return null;
        }
    }
}