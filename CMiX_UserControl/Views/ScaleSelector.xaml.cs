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
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ScaleSelector : UserControl
    {
        public ScaleSelector()
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
        DependencyProperty.Register("SelectedName", typeof(string), typeof(ScaleSelector), new PropertyMetadata("STD_CTR"));
        [Bindable(true)]
        public string SelectedName
        {
            get { return (string)this.GetValue(SelectedNameProperty); }
            set { this.SetValue(SelectedNameProperty, value); }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb.IsChecked == true)
            {
                SelectedName = rb.Name;
            }
            OnSelectionChanged();
        }
    }
}
