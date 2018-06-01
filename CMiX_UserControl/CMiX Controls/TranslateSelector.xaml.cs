using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CMiX
{
    public partial class TranslateSelector : UserControl
    {
        public TranslateSelector()
        {
            InitializeComponent();
        }

        public event EventHandler SelectionChanged;

        private void OnSelectionChanged()
        {
            //Null check makes sure the main page is attached to the event
            if (this.SelectionChanged != null)
                this.SelectionChanged(this, new EventArgs());
        }

        public static readonly DependencyProperty SelectedNameProperty =
        DependencyProperty.Register("SelectedName", typeof(string), typeof(TranslateSelector), new UIPropertyMetadata("STD_CTR"));
        public string SelectedName
        {
            get { return (string)this.GetValue(SelectedNameProperty); }
            set { this.SetValue(SelectedNameProperty, value); }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OnSelectionChanged();
        }
    }

    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
