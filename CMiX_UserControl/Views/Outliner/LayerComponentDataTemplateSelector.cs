using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Views
{
    class LayerComponentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LayerComponentTemplate { get; set; }
        public DataTemplate MaskComponentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return LayerComponentTemplate;
        }
    }
}
