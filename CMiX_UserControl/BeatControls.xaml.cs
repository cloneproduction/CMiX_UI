using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX
{
    public partial class BeatControls : UserControl
    {
        public BeatControls()
        {
            InitializeComponent();
        }

        #region Properties
        public static readonly DependencyProperty ChanceToHitProperty =
        DependencyProperty.Register("ChanceToHit", typeof(double), typeof(BeatControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChanceToHit
        {
            get { return (double)this.GetValue(ChanceToHitProperty); }
            set { this.SetValue(ChanceToHitProperty, value); }
        }

        public static readonly DependencyProperty MultiplierProperty =
        DependencyProperty.Register("Multiplier", typeof(double), typeof(BeatControls), new PropertyMetadata(1.0, new PropertyChangedCallback(PropertyChanged)));
        [Bindable(true)]
        public double Multiplier
        {
            get { return (double)this.GetValue(MultiplierProperty); }
            set { this.SetValue(MultiplierProperty, value); }
        }

        public static readonly DependencyProperty PeriodProperty =
        DependencyProperty.Register("Period", typeof(double), typeof(BeatControls), new PropertyMetadata(0.0, new PropertyChangedCallback(PropertyChanged)));
        [Bindable(true)]
        public double Period
        {
            get { return (double)this.GetValue(PeriodProperty); }
            set { this.SetValue(PeriodProperty , value) ; }
        }

        public static readonly DependencyProperty ControlPeriodProperty =
        DependencyProperty.Register("ControlPeriod", typeof(double), typeof(BeatControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ControlPeriod
        {
            get { return (double)this.GetValue(ControlPeriodProperty); }
            set { this.SetValue(ControlPeriodProperty, value); }
        }
        #endregion

        #region Events
        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var beatcontrol = d as BeatControls;
            if(beatcontrol != null)
            {
                beatcontrol.ControlPeriod = beatcontrol.Period * beatcontrol.Multiplier;
            }
        }

        public event EventHandler BeatControlChanged;
        protected virtual void OnBeatControlChanged(RoutedEventArgs e)
        {
            var handler = BeatControlChanged;
            if (handler != null)
                handler(this, e);
        }

        private void BeatMultiply_Click(object sender, RoutedEventArgs e)
        {
            Multiplier /= 2.0;
            OnBeatControlChanged(e);
        }

        private void BeatDivide_Click(object sender, RoutedEventArgs e)
        {
            Multiplier *= 2.0;
            OnBeatControlChanged(e);
        }

        private void ResetBPM_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
