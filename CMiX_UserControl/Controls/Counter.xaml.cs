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
            //DataContext = this;
        }

        public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register("Count", typeof(int), typeof(Counter), new PropertyMetadata(0));
        [Bindable(true)]
        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Count += 1;
        }

        private void Button_Sub(object sender, RoutedEventArgs e)
        {
            if (Count >= 1)
            {
                Count -= 1;
            }
        }
    }
}
