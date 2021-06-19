﻿using System.Windows;
using System.Windows.Controls;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class ComponentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProjectTemplate { get; set; }
        public DataTemplate LayerTemplate { get; set; }
        public DataTemplate SceneTemplate { get; set; }
        public DataTemplate EntityTemplate { get; set; }
        public DataTemplate CompositionTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;

            if(item != null)
            {
                if (item is Project)
                    dataTemplate = ProjectTemplate;
                else if (item is Composition)
                    dataTemplate = CompositionTemplate;
                else if (item is Layer)
                    dataTemplate = LayerTemplate;
                else if (item is Scene)
                    dataTemplate = SceneTemplate;
                else if (item is Entity)
                    dataTemplate = EntityTemplate;
            }

            return dataTemplate;         
        }
    }
}