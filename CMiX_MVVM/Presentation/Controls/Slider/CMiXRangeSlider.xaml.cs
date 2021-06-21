// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Core.Presentation.Controls
{
    public partial class CMiXRangeSlider : UserControl
    {
        public CMiXRangeSlider()
        {
            InitializeComponent();
        }

        public event EventHandler RangeSliderValueChanged;
        protected virtual void OnRangeSliderValueChanged(EventArgs e)
        {
            var handler = RangeSliderValueChanged;
            if (handler != null)
                handler(this, e);
        }

        #region Properties
        public static readonly DependencyProperty RangeMinProperty =
        DependencyProperty.Register("RangeMin", typeof(double), typeof(CMiXRangeSlider), new PropertyMetadata(0.0));
        public double RangeMin
        {
            get { return (double)GetValue(RangeMinProperty); }
            set { SetValue(RangeMinProperty, value); }
        }

        public static readonly DependencyProperty RangeMaxProperty =
        DependencyProperty.Register("RangeMax", typeof(double), typeof(CMiXRangeSlider), new PropertyMetadata(1.0));
        public double RangeMax
        {
            get { return (double)GetValue(RangeMaxProperty); }
            set { SetValue(RangeMaxProperty, value); }
        }
        #endregion

        private void RangeSliderUserControl_RangeSelectionChanged(object sender, RangeSelectionChangedEventArgs e)
        {
            OnRangeSliderValueChanged(e);
        }
    }
}
