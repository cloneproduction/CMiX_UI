using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX.Controls
{
    public partial class IPBox : UserControl
    {
        public IPBox()
        {
            InitializeComponent();
        }

        #region PROPERTIES
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public static readonly DependencyProperty IPAddressProperty =
        DependencyProperty.Register("IPAddress", typeof(string), typeof(IPBox), new PropertyMetadata("0.0.0.0"));
        [Bindable(true)]
        public string IPAddress
        {
            get
            {
                return (string)GetValue(IPAddressProperty);
            }
            set
            {
                string[] splitValues = value.Split('.');
                txtboxFirstPart.Text = splitValues[0];
                txtboxSecondPart.Text = splitValues[1];
                txtboxThridPart.Text = splitValues[2];
                txtboxFourthPart.Text = splitValues[3];
                NotifyPropertyChanged("IPAddress");
                Console.WriteLine("IPBox IPAddress Changed");
                SetValue(IPAddressProperty, value);
            }
        }

        private bool focusMoved = false;
        #endregion

        #region METHODS
        private static void TextboxTextCheck(object sender)
        {
            TextBox txtbox = (TextBox)sender;
            txtbox.Text = GetNumberFromString(txtbox.Text);
            if (!string.IsNullOrWhiteSpace(txtbox.Text))
            {
                if (Convert.ToInt32(txtbox.Text) > 255)
                    txtbox.Text = "255";
                else if (Convert.ToInt32(txtbox.Text) < 0)
                    txtbox.Text = "0";
            }
            txtbox.CaretIndex = txtbox.Text.Length;
        }

        private static string GetNumberFromString(string str)
        {
            StringBuilder numberBuilder = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsNumber(c))
                    numberBuilder.Append(c);
            }
            return numberBuilder.ToString();
        }

        void UpdateIPAddress(object sender)
        {
            TextBox txtbox = (TextBox)sender;
            if (txtbox != null && txtbox.IsLoaded)
            {
                if (txtbox.Text != String.Empty)
                {
                    IPAddress = txtboxFirstPart.Text + "." + txtboxSecondPart.Text + "." + txtboxThridPart.Text + "." + txtboxFourthPart.Text;
                }
            }
        }
        #endregion

        #region EVENTS
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateIPAddress(sender);
        }

        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextboxTextCheck(sender);
            UpdateIPAddress(sender);
        }

        private void txtboxFirstPart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                txtboxSecondPart.Focus();
                txtboxSecondPart.Text = String.Empty;
                focusMoved = true;
            }
            else
            {
                focusMoved = false;
            }
        }

        private void txtboxSecondPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod || e.Key == Key.Decimal && !focusMoved)
            {
                txtboxThridPart.Focus();
                txtboxThridPart.Text = String.Empty;
                focusMoved = true;
            }
            else
            {
                focusMoved = false;
            }
        }

        private void txtboxThridPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                txtboxFourthPart.Focus();
                txtboxFourthPart.Text = String.Empty;
            } 
        }
        #endregion
    }
}