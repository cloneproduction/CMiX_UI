using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class MasterBeatControl : UserControl
    {
        BeatSystem beatsystem = new BeatSystem();
        SharpOSC.OscMessage message;

        public MasterBeatControl()
        {
            InitializeComponent();
        }

        #region Properties
        public static readonly DependencyProperty MasterPeriodProperty =
        DependencyProperty.Register("MasterPeriod", typeof(double), typeof(MasterBeatControl));
        [Bindable(true)]
        public double MasterPeriod
        {
            get { return (double)GetValue(MasterPeriodProperty); }
            set { SetValue(MasterPeriodProperty, value); }
        }
        #endregion

        #region Events
        private void Main_ResyncBPM_Click(object sender, RoutedEventArgs e)
        {
            var Button = sender as Button;
            var message = new SharpOSC.OscMessage("/" + Button.Name.ToString(), beatsystem.GetCurrentTime().ToString());
            var pouet = new SharpOSC.UDPSender("127.0.0.1", 55555);
            pouet.Send(message);
        }

        private void Main_MultiplyBPM_Click(object sender, RoutedEventArgs e)
        {
            MasterPeriod /= 2.0;

            message = new SharpOSC.OscMessage("/" + "MasterPeriod", MasterPeriod.ToString());
            var pouet = new SharpOSC.UDPSender("127.0.0.1", 55555);
            pouet.Send(message);
        }

        private void Main_DivideBPM_Click(object sender, RoutedEventArgs e)
        {
            MasterPeriod *= 2.0;

            message = new SharpOSC.OscMessage("/" + "MasterPeriod", MasterPeriod.ToString());
            var pouet = new SharpOSC.UDPSender("127.0.0.1", 55555);
            pouet.Send(message);
        }

        private void Main_TapBPM_Click(object sender, RoutedEventArgs e)
        {
            MasterPeriod = beatsystem.GetMasterPeriod();

            message = new SharpOSC.OscMessage("/" + "MasterPeriod", MasterPeriod.ToString());
            var pouet = new SharpOSC.UDPSender("127.0.0.1", 55555);
            pouet.Send(message);
        }
        #endregion
    }
}
