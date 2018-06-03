using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace CMiX
{
    public partial class ChannelControls : UserControl
    {
        Messenger message = new Messenger();
        ChannelData cd = new ChannelData();

        public ChannelControls()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { EnabledOSC = true; }));
        }

        #region Properties


        public static readonly DependencyProperty EnabledOSCProperty =
        DependencyProperty.Register("EnabledOSC", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public bool EnabledOSC
        {
            get { return (bool)this.GetValue(EnabledOSCProperty); }
            set { this.SetValue(EnabledOSCProperty, value); }
        }

        public static readonly DependencyProperty MasterPeriodProperty =
        DependencyProperty.Register("MasterPeriod", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double MasterPeriod
        {
            get { return (double)this.GetValue(MasterPeriodProperty); }
            set { this.SetValue(MasterPeriodProperty, value); }
        }

        private List<string> _GeometryFileMask = new List<string>(new[] { ".fbx", ".obj" });
        public List<string> GeometryFileMask
        {
            get { return _GeometryFileMask; }
            set { _GeometryFileMask = value; }
        }

        private List<string> _TextureFileMask = new List<string>(new[] { ".png", ".jpg", ".jpeg", ".mov", ".txt" });
        public List<string> TextureFileMask
        {
            get { return _TextureFileMask; }
            set { _TextureFileMask = value; }
        }

        private List<string> _TranslateNames = new List<string>(new[] { "STD_CTR", "STD_LT", "STD_TOP_LT", "STD_TOP", "STD_TOP_RT", "STD_RT", "STD_BOT_RT", "STD_BOT", "STD_BOT_LT", "FLA_RDM", "FLA_RDM_X", "FLA_RDM_Y", "FLA_RDM_Z", "FLA_RDM_SLDX", "FLA_RDM_SLDY", "FLA_RDM_SLDZ", "SLD_RDM", "SLD_RDM_X", "SLD_RDM_Y", "SLD_RDM_Z", "SLD_LT", "SLD_LTRT", "SLD_RT", "SLD_DN", "SLD_DNUP", "SLD_UP", "SLD_BA", "SLD_BAFO", "SLD_FO" });
        public List<string> TranslateNames
        {
            get { return _TranslateNames; }
            set { _TranslateNames = value; }
        }

        private List<string> _ScaleNames = new List<string>(new[] { "STD_CTR", "STD_RDM", "FLA_RDM", "SLD_RDM", "SLD_GRW", "SLD_CMP" });
        public List<string> ScaleNames
        {
            get { return _ScaleNames; }
            set { _ScaleNames = value; }
        }

        private List<string> _RotationNames = new List<string>(new[] { "STD_CTR", "STD_+45", "STD_+90", "STD_180", "STD_-90", "STD_-45", "FLA_RDM", "SLD_RDM", "SLD_CLK", "SLD_CCK", "SLD_LIN" });
        public List<string> RotationNames
        {
            get { return _RotationNames; }
            set { _RotationNames = value; }
        }

        private List<string> _TexTransformNames = new List<string>(new[] { "NONE", "MIR_CTR", "MIR_LT", "MIR_TOP", "MIR_RT", "MIR_BOT", "CLA_LT", "CLA_TOP", "CLA_RT", "CLA_BOT" });
        public List<string> TexTransformNames
        {
            get { return _TexTransformNames; }
            set { _TexTransformNames = value; }
        }

        private List<string> _ViewNames = new List<string>(new[] { "STD_CTR", "STD_+45", "STD_+90", "STD_180", "STD_-90", "STD_-45", "FLA_RDM", "SLD_RDM", "SLD_CLK", "SLD_CCK" });
        public List<string> ViewNames
        {
            get { return _ViewNames; }
            set { _ViewNames = value; }
        }

        private List<string> _ColorNames = new List<string>(new[] { "STD_CTR", "SLD_RDM", "FLA_RDM", "SLD_LIN" });
        public List<string> ColorNames
        {
            get { return _ColorNames; }
            set { _ColorNames = value; }
        }

        private List<string> _InvertNames = new List<string>(new[] { "INV_RGB", "INV_VAL" });
        public List<string> InvertNames
        {
            get { return _InvertNames; }
            set { _InvertNames = value; }
        }

        #endregion

        #region Registered Event
        public static readonly RoutedEvent ChannelChangedEvent =
        EventManager.RegisterRoutedEvent("ChannelChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChannelControls));

        public event RoutedEventHandler ChannelChanged
        {
            add { AddHandler(ChannelChangedEvent, value); }
            remove { RemoveHandler(ChannelChangedEvent, value); }
        }

        void ChannelControlChanged()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ChannelControls.ChannelChangedEvent);
            RaiseEvent(newEventArgs);
        }
        #endregion

        #region Event

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                ComboBox combobox = sender as ComboBox;
                if (combobox.SelectedItem != null)
                {
                    cd = this.DataContext as ChannelData;
                    message.SendOSC(cd.Name + "/" + combobox.Name, combobox.SelectedItem.ToString());
                }

            }
        }

        private void SliderValueChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                CustomSlider slider = sender as CustomSlider;
                cd = this.DataContext as ChannelData;
                if (cd != null && slider != null)
                {
                    message.SendOSC(cd.Name + "/" + slider.Name, slider.Value.ToString());
                }
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                ToggleButton togglebutton = sender as ToggleButton;
                cd = this.DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + togglebutton.Name, togglebutton.IsChecked.ToString());
            }
        }

        private void ToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                ToggleButton togglebutton = sender as ToggleButton;
                cd = this.DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + togglebutton.Name, togglebutton.IsChecked.ToString());
            }
        }

        /*private void CounterChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                Counter counter = sender as Counter;
                cd = this.DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + counter.Name, counter.Count.ToString());
            }
        }*/

        private void ChannelColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (EnabledOSC == true)
            {
                ColorPicker.ColorPicker colorpicker = sender as ColorPicker.ColorPicker;
                if(cd != null)
                {
                    Color col = colorpicker.SelectedColor;
                    string hex = Utils.ColorToHexString(col);
                    cd = this.DataContext as ChannelData;
                    message.SendOSC(cd.Name + "/" + colorpicker.Name, hex);
                }
            }
        }

        public void FileSelectorChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                FileSelector fileselector = sender as FileSelector;
                List<string> selectedfilename = new List<string>();
                foreach (ListBoxFileName item in fileselector.SelectedItems)
                {
                    if (item.FileIsSelected)
                    {
                        selectedfilename.Add(item.FileName);
                    }
                }
                cd = this.DataContext as ChannelData;
                message.SendOSCList(cd.Name + "/" + fileselector.Name, selectedfilename);
            }
        }

        private void BeatControlChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                BeatControls beatcontrols = sender as BeatControls;
                cd = this.DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + beatcontrols.Name, beatcontrols.Multiplier.ToString());
            }
        }
        #endregion

        private void Tap(object sender, RoutedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                FileSelector fileselector = sender as FileSelector;
                if (fileselector.MouseDown == true)
                {
                    List<string> selectedfilename = new List<string>();
                    foreach (ListBoxFileName item in fileselector.SelectedItems)
                    {
                        if (item.FileIsSelected)
                        {
                            selectedfilename.Add(item.FileName);
                        }
                    }
                    cd = this.DataContext as ChannelData;
                    message.SendOSCList(cd.Name + "/" + fileselector.Name, selectedfilename);
                }

            }
        }

        private void TranslateSelector_SelectionChanged(object sender, EventArgs e)
        {
            if (EnabledOSC == true)
            {
                TranslateSelector ts = sender as TranslateSelector;
                if (ts.SelectedName != null)
                {
                    cd = this.DataContext as ChannelData;
                    message.SendOSC(cd.Name + "/" + ts.Name, ts.SelectedName);
                }
            }
        }

        private void ScaleSelector_SelectionChanged(object sender, EventArgs e)
        {
            if (EnabledOSC == true)
            {
                ScaleSelector ss = sender as ScaleSelector;
                cd = this.DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + ss.Name, ss.SelectedName);
            }

        }
        /*private void RotationSelector_SelectionChanged(object sender, EventArgs e)
        {
            if (EnabledOSC == true)
            {
                RotationSelector rs = sender as RotationSelector;
                List<string> list = new List<string>();
                foreach (string st in rs.Rotation)
                {
                    list.Add(st);
                }
                cd = this.DataContext as ChannelData;
                message.SendOSCList(cd.Name + "/" + rs.Name, list);
            }
        }*/

        private void Ch_Parameters_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MaskPopupTranslate.IsOpen = false;
                SpritePopupTranslate.IsOpen = false;
                MaskPopupScale.IsOpen = false;
                SpritePopupScale.IsOpen = false;
                //MaskPopupRotation.IsOpen = false;
                //SpritePopupRotation.IsOpen = false;
            }
        }
    }
}