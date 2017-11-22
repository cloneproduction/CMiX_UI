using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMiX
{
    public partial class ChannelControls : UserControl
    {
        CMiXData data = new CMiXData();
        Messenger message = new Messenger();

        public ChannelControls()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region Properties
        public static readonly DependencyProperty EnabledOSCProperty =
        DependencyProperty.Register("EnabledOSC", typeof(bool), typeof(ChannelControls), new PropertyMetadata());
        [Bindable(true)]
        public bool EnabledOSC
        {
            get { return (bool)this.GetValue(EnabledOSCProperty); }
            set { this.SetValue(EnabledOSCProperty, value); }
        }

        public static readonly DependencyProperty ChannelBrightnessProperty =
        DependencyProperty.Register("ChannelBrightness", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelBrightness
        {
            get { return (double)this.GetValue(ChannelBrightnessProperty); }
            set { this.SetValue(ChannelBrightnessProperty, value); }
        }

        public static readonly DependencyProperty ChannelContrastProperty =
        DependencyProperty.Register("ChannelContrast", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelContrast
        {
            get { return (double)this.GetValue(ChannelContrastProperty); }
            set { this.SetValue(ChannelContrastProperty, value); }
        }

        public static readonly DependencyProperty ChannelSaturationProperty =
        DependencyProperty.Register("ChannelSaturation", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelSaturation
        {
            get { return (double)this.GetValue(ChannelSaturationProperty); }
            set { this.SetValue(ChannelSaturationProperty, value); }
        }

        public static readonly DependencyProperty ChannelInvertProperty =
        DependencyProperty.Register("ChannelInvert", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelInvert
        {
            get { return (double)this.GetValue(ChannelInvertProperty); }
            set { this.SetValue(ChannelInvertProperty, value); }
        }

        public static readonly DependencyProperty ChannelFeedBackProperty =
        DependencyProperty.Register("ChannelFeedBack", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelFeedBack
        {
            get { return (double)this.GetValue(ChannelFeedBackProperty); }
            set { this.SetValue(ChannelFeedBackProperty, value); }
        }

        public static readonly DependencyProperty ChannelHueRangeProperty =
        DependencyProperty.Register("ChannelHueRange", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelHueRange
        {
            get { return (double)this.GetValue(ChannelHueRangeProperty); }
            set { this.SetValue(ChannelHueRangeProperty, value); }
        }

        public static readonly DependencyProperty ChannelSatRangeProperty =
        DependencyProperty.Register("ChannelSatRange", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelSatRange
        {
            get { return (double)this.GetValue(ChannelSatRangeProperty); }
            set { this.SetValue(ChannelSatRangeProperty, value); }
        }

        public static readonly DependencyProperty ChannelValRangeProperty =
        DependencyProperty.Register("ChannelValRange", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelValRange
        {
            get { return (double)this.GetValue(ChannelValRangeProperty); }
            set { this.SetValue(ChannelValRangeProperty, value); }
        }

        public static readonly DependencyProperty ChannelSelectedSpriteTexProperty =
        DependencyProperty.Register("ChannelSelectedSpriteTex", typeof(ObservableCollection<ListBoxFileName>), typeof(ChannelControls), new PropertyMetadata());
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> ChannelSelectedSpriteTex
        {
            get { return (ObservableCollection<ListBoxFileName>)this.GetValue(ChannelSelectedSpriteTexProperty); }
            set { this.SetValue(ChannelSelectedSpriteTexProperty, value); }
        }

        public static readonly DependencyProperty ChannelSelectedSpriteGeomProperty =
        DependencyProperty.Register("ChannelSelectedSpriteGeom", typeof(ObservableCollection<ListBoxFileName>), typeof(ChannelControls), new PropertyMetadata());
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> ChannelSelectedSpriteGeom
        {
            get { return (ObservableCollection<ListBoxFileName>)this.GetValue(ChannelSelectedSpriteGeomProperty); }
            set { this.SetValue(ChannelSelectedSpriteGeomProperty, value); }
        }

        public static readonly DependencyProperty ChannelSelectedMaskTexProperty =
        DependencyProperty.Register("ChannelSelectedMaskTex", typeof(ObservableCollection<ListBoxFileName>), typeof(ChannelControls), new PropertyMetadata());
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> ChannelSelectedMaskTex
        {
            get { return (ObservableCollection<ListBoxFileName>)this.GetValue(ChannelSelectedMaskTexProperty); }
            set { this.SetValue(ChannelSelectedMaskTexProperty, value); }
        }

        public static readonly DependencyProperty ChannelSelectedMaskGeomProperty =
        DependencyProperty.Register("ChannelSelectedMaskGeom", typeof(ObservableCollection<ListBoxFileName>), typeof(ChannelControls), new PropertyMetadata());
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> ChannelSelectedMaskGeom
        {
            get { return (ObservableCollection<ListBoxFileName>)this.GetValue(ChannelSelectedMaskGeomProperty); }
            set { this.SetValue(ChannelSelectedMaskGeomProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteCountProperty =
        DependencyProperty.Register("ChannelSpriteCount", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelSpriteCount
        {
            get { return (string)this.GetValue(ChannelSpriteCountProperty); }
            set { this.SetValue(ChannelSpriteCountProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskCountProperty =
        DependencyProperty.Register("ChannelMaskCount", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelMaskCount
        {
            get { return (string)this.GetValue(ChannelMaskCountProperty); }
            set { this.SetValue(ChannelMaskCountProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteTranslateNameProperty =
        DependencyProperty.Register("ChannelSpriteTranslateName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelSpriteTranslateName
        {
            get { return (string)this.GetValue(ChannelSpriteTranslateNameProperty); }
            set { this.SetValue(ChannelSpriteTranslateNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteScaleNameProperty =
        DependencyProperty.Register("ChannelSpriteScaleName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelSpriteScaleName
        {
            get { return (string)this.GetValue(ChannelSpriteScaleNameProperty); }
            set { this.SetValue(ChannelSpriteScaleNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteRotateNameProperty =
        DependencyProperty.Register("ChannelSpriteRotateName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelSpriteRotateName
        {
            get { return (string)this.GetValue(ChannelSpriteRotateNameProperty); }
            set { this.SetValue(ChannelSpriteRotateNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskTranslateNameProperty =
        DependencyProperty.Register("ChannelMaskTranslateName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelMaskTranslateName
        {
            get { return (string)this.GetValue(ChannelMaskTranslateNameProperty); }
            set { this.SetValue(ChannelMaskTranslateNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskScaleNameProperty =
        DependencyProperty.Register("ChannelMaskScaleName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelMaskScaleName
        {
            get { return (string)this.GetValue(ChannelMaskScaleNameProperty); }
            set { this.SetValue(ChannelMaskScaleNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskRotateNameProperty =
        DependencyProperty.Register("ChannelMaskRotateName", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelMaskRotateName
        {
            get { return (string)this.GetValue(ChannelMaskRotateNameProperty); }
            set { this.SetValue(ChannelMaskRotateNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelTexTransformProperty =
        DependencyProperty.Register("ChannelTexTransform", typeof(string), typeof(ChannelControls), new PropertyMetadata("1"));
        [Bindable(true)]
        public string ChannelTexTransform
        {
            get { return (string)this.GetValue(ChannelTexTransformProperty); }
            set { this.SetValue(ChannelTexTransformProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteTranslateProperty =
        DependencyProperty.Register("ChannelSpriteTranslate", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelSpriteTranslate
        {
            get { return (double)this.GetValue(ChannelSpriteTranslateProperty); }
            set { this.SetValue(ChannelSpriteTranslateProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteScaleProperty =
        DependencyProperty.Register("ChannelSpriteScale", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelSpriteScale
        {
            get { return (double)this.GetValue(ChannelSpriteScaleProperty); }
            set { this.SetValue(ChannelSpriteScaleProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteRotateProperty =
        DependencyProperty.Register("ChannelSpriteRotate", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelSpriteRotate
        {
            get { return (double)this.GetValue(ChannelSpriteRotateProperty); }
            set { this.SetValue(ChannelSpriteRotateProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskAspectRatioProperty =
        DependencyProperty.Register("ChannelMaskAspectRatio", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public double ChannelMaskAspectRatio
        {
            get { return (double)this.GetValue(ChannelMaskAspectRatioProperty); }
            set { this.SetValue(ChannelMaskAspectRatioProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskTranslateProperty =
        DependencyProperty.Register("ChannelMaskTranslate", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelMaskTranslate
        {
            get { return (double)this.GetValue(ChannelMaskTranslateProperty); }
            set { this.SetValue(ChannelMaskTranslateProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskScaleProperty =
        DependencyProperty.Register("ChannelMaskScale", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelMaskScale
        {
            get { return (double)this.GetValue(ChannelMaskScaleProperty); }
            set { this.SetValue(ChannelMaskScaleProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskRotateProperty =
        DependencyProperty.Register("ChannelMaskRotate", typeof(double), typeof(ChannelControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double ChannelMaskRotate
        {
            get { return (double)this.GetValue(ChannelMaskRotateProperty); }
            set { this.SetValue(ChannelMaskRotateProperty, value); }
        }

        public static readonly DependencyProperty ChannelSpriteAspectRatioProperty =
        DependencyProperty.Register("ChannelSpriteAspectRatio", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public double ChannelSpriteAspectRatio
        {
            get { return (double)this.GetValue(ChannelSpriteAspectRatioProperty); }
            set { this.SetValue(ChannelSpriteAspectRatioProperty, value); }
        }

        public static readonly DependencyProperty ChannelMask3DProperty =
        DependencyProperty.Register("ChannelMask3D", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public double ChannelMask3D
        {
            get { return (double)this.GetValue(ChannelMask3DProperty); }
            set { this.SetValue(ChannelMask3DProperty, value); }
        }

        public static readonly DependencyProperty ChannelMaskOnProperty =
        DependencyProperty.Register("ChannelMaskOn", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public double ChannelMaskOn
        {
            get { return (double)this.GetValue(ChannelMaskOnProperty); }
            set { this.SetValue(ChannelMaskOnProperty, value); }
        }

        public static readonly DependencyProperty ChannelSprite3DProperty =
        DependencyProperty.Register("ChannelSprite3D", typeof(bool), typeof(ChannelControls), new PropertyMetadata(false));
        [Bindable(true)]
        public double ChannelSprite3D
        {
            get { return (double)this.GetValue(ChannelSprite3DProperty); }
            set { this.SetValue(ChannelSprite3DProperty, value); }
        }

        public static readonly DependencyProperty _ChannelColor = 
        DependencyProperty.Register("ChannelColor", typeof(Color), typeof(ChannelControls));
        [Bindable(true)]
        public Color ChannelColor
        {
            get {return (Color)GetValue(_ChannelColor);}
            set{SetValue(_ChannelColor, value);}
        }

        private List<string> _GeometryFileMask = new List<string>(new[] { ".fbx", ".obj" });
        public List<string> GeometryFileMask
        {
            get { return _GeometryFileMask; }
            set { _GeometryFileMask = value; }
        }

        private List<string> _TextureFileMask = new List<string>(new[] { ".png", ".jpg", ".mov" });
        public List<string> TextureFileMask
        {
            get { return _TextureFileMask; }
            set { _TextureFileMask = value; }
        }

        private List<string> _TranslateNames = new List<string>(new[] {"STD_CTR", "STD_LEFT", "STD_RIGHT", "STD_TOP", "STD_BOT", "STD_TOPRIGHT", "STD_TOPLEFT", "STD_BOTRIGHT", "STD_BOTLEFT", "SLD_LTRT", "SLD_DNUP", "SLD_BAFO", "SLD_LT", "SLD_RT", "SLD_UP", "SLD_DN", "SLD_BA", "SLD_FO", "R_FLA", "R_FLA_X", "R_FLA_Y", "R_FLA_Z", "R_FLA_SLDX", "R_FLA_SLDY", "R_FLA_SLDZ", "R_SLD", "R_SLD_X", "R_SLD_Y", "R_SLD_Z" });
        public List<string> TranslateNames
        {
            get { return _TranslateNames; }
            set { _TranslateNames = value; }
        }

        private List<string> _ScaleNames = new List<string>(new[] { "STD", "R_BNC", "R_FLA", "R_STD", "SLD_GRW", "SLD_CMP" });
        public List<string> ScaleNames
        {
            get { return _ScaleNames; }
            set { _ScaleNames = value; }
        }

        private List<string> _RotationNames = new List<string>(new[] { "STD_CTR", "STD_+45", "STD_+90", "STD_180", "STD_-90", "STD_-45", "RDM_BNC", "RDM_FLA", "SLD_CLK", "SLD_CCK", "SLD_RDM", "SLD_LIN" });
        public List<string> RotationNames
        {
            get { return _RotationNames; }
            set { _RotationNames = value; }
        }

        private List<string> _TexTransformNames = new List<string>(new[] { "None", "Mir_Left", "Mir_Right", "Mir_Top", "Mir_Bot", "Mir_Ctr" });
        public List<string> TexTransformNames
        {
            get { return _TexTransformNames; }
            set { _TexTransformNames = value; }
        }

        private List<string> _FFTNames = new List<string>(new[] { "None", "Mir_Left", "Mir_Right", "Mir_Top", "Mir_Bot", "Mir_Ctr" });
        public List<string> FFTNames
        {
            get { return _FFTNames; }
            set { _FFTNames = value; }
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
            RoutedEventArgs newEventArgs =new RoutedEventArgs(ChannelControls.ChannelChangedEvent);
            RaiseEvent(newEventArgs);
        }
        #endregion

        #region Event
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void SliderValueChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void ToggleButton_UnChecked(object sender, RoutedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void CounterChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void ChannelColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }

        private void FileSelectorChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                data = (CMiXData)Utils.FindParent<CMiX_UI>(this).DataContext;
                message.Send(data, sender as FrameworkElement);
            }
        }
        #endregion
    }
}