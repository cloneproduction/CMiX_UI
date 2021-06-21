﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Core.Presentation.ViewModels
{
    public class TransformModifierTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TranslateXYZTemplate { get; set; }
        public DataTemplate RandomXYZTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            //var animParameter = item as AnimParameter;

            if (item != null)
            {
                if (item is TranslateModifier)
                    dataTemplate = TranslateXYZTemplate;
                else if (item is RandomXYZ)
                    dataTemplate = RandomXYZTemplate;
            }

            return dataTemplate;
        }
    }
}
