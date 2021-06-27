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

        //double parentWidth;
        //double parentHeight;
        public TranslateTransform translateTransform;
        //private void ExceptionPopup_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (PlacementTarget != null)
        //    {
        //        if (PlacementTarget is FrameworkElement)
        //        {
        //            parentWidth = (PlacementTarget as FrameworkElement).ActualWidth;
        //            parentHeight = (PlacementTarget as FrameworkElement).ActualHeight;

        //        }
        //    }
        //}


        //protected override void OnOpened(EventArgs e)
        //{
        //    this.HorizontalOffset = this.ActualWidth;// - parentWidth;
        //    Console.WriteLine("ActualWidth " + this.ActualWidth);
        //    //this.VerticalOffset = parentHeight;
        //    base.OnOpened(e);
        //}

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
