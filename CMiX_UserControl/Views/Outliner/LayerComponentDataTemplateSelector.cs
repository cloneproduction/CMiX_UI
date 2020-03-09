using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CMiX.Studio.ViewModels;

namespace CMiX.Views
{
    public class LayerComponentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerTemplate { get; set; }
        public DataTemplate MaskTemplate { get; set; }
        public DataTemplate ProjectTemplate { get; set; }
        public DataTemplate CompositionTemplate { get; set; }
        public DataTemplate EntityTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //DataTemplate dataTemplate = new DataTemplate();

            //if (item is Project)
            //    return ProjectTemplate;
            //else if (item is Composition)
            //    return CompositionTemplate;
            Console.WriteLine("Item Type is " + item.GetType());
            if (item is Studio.ViewModels.Layer)
            {
                Console.WriteLine("POUETPOUET");
                if (((Studio.ViewModels.Layer)item).IsMask == false)
                {
                    Console.WriteLine("IsMask");
                    return MaskTemplate;
                }
                    
                else
                    return base.SelectTemplate(item, container);
            }
            else
                return base.SelectTemplate(item, container);
            //else if (item is Entity)
            //    return EntityTemplate;
        }
    }
}
