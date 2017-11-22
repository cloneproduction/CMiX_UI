using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class Counter : UserControl
    {
        public Counter()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register("Count", typeof(int), typeof(Counter), new PropertyMetadata(0));
        [Bindable(true)]
        public int Count
        {
            get { return (int)this.GetValue(CountProperty); }
            set { this.SetValue(CountProperty, value); }
        }
       
        private void Button_Add(object sender, RoutedEventArgs e)
        {


            if (Count <= 10)
            {
                Count += 1;
            }
            OnCounterChanged(e);
        }

        private void Button_Sub(object sender, RoutedEventArgs e)
        {
            if (Count >= 1)
            {
                Count -= 1;
            }
            OnCounterChanged(e);
        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        public event EventHandler CounterChanged;
        protected virtual void OnCounterChanged(EventArgs e)
        {
            var handler = CounterChanged;
            if (handler != null)
                handler(this, e);
        }

        private void CounterValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void CounterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnCounterChanged(e);
        }
    }
}
