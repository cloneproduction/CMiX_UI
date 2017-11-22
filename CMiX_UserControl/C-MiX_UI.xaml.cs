using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;
using Microsoft.Win32;
using SharpOSC;

namespace CMiX
{

    public partial class CMiX_UI : UserControl
    {
        BeatSystem beatsystem = new BeatSystem();
        CMiXData cmixdata = new CMiXData();
        Messenger message = new Messenger();

        public CMiX_UI()
        {
            InitializeComponent();
            this.DataContext = cmixdata;

            Singleton.Instance.MessageReceived += Instance_MessageReceived;
        }

        private void Instance_MessageReceived(string message)
        {
            MessageBox.Show(message);
        }

        #region Properties
        public static readonly DependencyProperty EnabledOSCProperty =
        DependencyProperty.Register("EnabledOSC", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(true));
        [Bindable(true)]
        public bool EnabledOSC
        {
            get { return (bool)this.GetValue(EnabledOSCProperty); }
            set { this.SetValue(EnabledOSCProperty, value); }
        }

        private List<string> _ChannelsBlendMode = new List<string>(new[] { "Normal", "Add", "Substract", "Lighten", "Darken", "Multiply" });
        public List<string> ChannelsBlendMode
        {
            get { return _ChannelsBlendMode; }
            set { _ChannelsBlendMode = value; }
        }

        private List<string> _CamRotation = new List<string>(new[] { "STD_CTR", "STD_UP", "STD_DN", "STD_LT", "STD_RT", "STD_MID", "SLD_RDM", "SLD_DNUP", "SLD_DN", "SLD_UP", "SLD_LT", "SLD_RT", "SLD_LTRT", "FLA_RDM", "FLA_DNUP", "FLA_DN", "FLA_UP", "FLA_LT", "FLA_RT", "FLA_LTRT" });
        public List<string> CamRotation
        {
            get { return _CamRotation; }
            set { _CamRotation = value; }
        }

        private List<string> _CamLookAt = new List<string>(new[] { "STD", "RandomFlash", "RandomSlide" });
        public List<string> CamLookAt
        {
            get { return _CamLookAt; }
            set { _CamLookAt = value; }
        }

        private List<string> _CamView = new List<string>(new[] { "STD", "Oblique-125", "Oblique125", "FlashRandom", "SpinRandom", "SpinRight", "SpinLeft" });
        public List<string> CamView
        {
            get { return _CamView; }
            set { _CamView = value; }
        }
        #endregion

        #region Events
        private void ChannelTab_DragOver(object sender, DragEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            rb.IsChecked = true;
        }

        private void Channel_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string layername = rb.Tag.ToString();
            ChannelControls cc = (ChannelControls)ContentControl.FindName(layername);
            if (cc != null)
            {
                cc.Visibility = Visibility.Visible;
            }
        }

        private void Channel_Unchecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string layername = rb.Tag.ToString();
            ChannelControls cc = (ChannelControls)ContentControl.FindName(layername);
            if (cc != null)
            {
                cc.Visibility = Visibility.Collapsed;
            }
        }

        private void SliderValueChanged(object sender, System.EventArgs e)
        {
            if (cmix != null)
            {
                var control = sender as FrameworkElement;
                cmixdata = (CMiXData)cmix.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);

                List<string> slidervalue = new List<string>();
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                foreach (double st in (ObservableCollection<double>)list)
                {
                    slidervalue.Add(st.ToString("0.00"));
                }
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), slidervalue.ToArray()));
            }
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmix != null)
            {
                var control = sender as ComboBox;
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);

                List<string> slidervalue = new List<string>();
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                foreach (string st in (ObservableCollection<string>)list)
                {
                    slidervalue.Add(st.ToString());
                }
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), slidervalue.ToArray()));
            }
        }

        private void SingleComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                var control = sender as ComboBox;
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), list.ToString()));
            }
            Thread.Sleep(10);
        }

        private void BeatControlsChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                var control = sender as BeatControls;
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);

                List<string> value = new List<string>();
                foreach (double st in cmixdata.ChannelBeatMultiplier)
                {
                    value.Add(st.ToString());
                }
                OSCSender.Send(new SharpOSC.OscMessage("/" + "ChannelBeatMultiplier", value.ToArray()));
            }
        }

        private void BeatControlsPostFX_BeatControlChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                var control = sender as BeatControls;
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Name, cmixdata.PostFXBeatMultiplier[0].ToString()));
            }
        }

        private void BeatControlsCam_BeatControlChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                var control = sender as BeatControls;
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender("127.0.0.1", 55555);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Name, cmixdata.CamBeatMultiplier[0].ToString()));
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            this.EnabledOSC = false;
            this.DataContext = new CMiXData();
            message.SendAll(new CMiXData());
            this.EnabledOSC = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CMiXData cmixdata = (CMiXData)this.DataContext;

            if (File.Exists(cmixdata.FilePath[0]) == true)
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(cmixdata.FilePath[0]))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, cmixdata);
                }
            }
            else 
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = "json";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filename = saveFileDialog.FileName;

                    cmixdata.CompoName[0] = Path.GetFileNameWithoutExtension(filename);
                    cmixdata.FilePath[0] = Path.GetFullPath(filename);

                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    using (StreamWriter sw = new StreamWriter(filename))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, cmixdata);
                    }
                }

            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                CMiXData cmixdata = (CMiXData)this.DataContext;

                cmixdata.CompoName[0] = Path.GetFileNameWithoutExtension(filename);
                cmixdata.FilePath[0] = Path.GetFullPath(filename);

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(filename))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, cmixdata);
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                
                if(Path.GetExtension(file) == ".json")
                {
                    CMiXData data = JsonConvert.DeserializeObject<CMiXData>(File.ReadAllText(openFileDialog.FileName));
                    this.EnabledOSC = false;

                    this.DataContext = data;
                    message.SendAll(data);

                    this.EnabledOSC = true;
                }
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion


    }
}