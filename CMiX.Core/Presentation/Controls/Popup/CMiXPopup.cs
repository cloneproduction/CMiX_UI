// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CMiX.Core.Presentation.Controls
{
    public class CMiXPopup : Popup
    {
        public CMiXPopup()
        {
            translateTransform = new TranslateTransform();
            this.RenderTransform = translateTransform;
            OnApplyTemplate();
            //Loaded += new RoutedEventHandler(ExceptionPopup_Loaded);

            this.CustomPopupPlacementCallback = (popupSize, targetSize, offset) => new[]
                {
                    new CustomPopupPlacement
                    {
                        Point = new Point(targetSize.Width - popupSize.Width, targetSize.Height)
                    }
                };
        }

        public TranslateTransform translateTransform;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
