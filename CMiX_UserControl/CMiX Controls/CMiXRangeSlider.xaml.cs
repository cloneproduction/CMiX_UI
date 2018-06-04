using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace CMiX
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
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

    public class RangeValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range/100).ToString("0.00");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = null;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range * 100).ToString("0.00");
            }
            return result;
        }
    }

    public class IntToNormalizedDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.00;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range * 100);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0.00;
            double range = System.Convert.ToDouble(value);
            if (value != null)
            {
                result = (range / 100);
            }
            return result;
        }
    }
}
