using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;


namespace CMiX
{

    public partial class CMiX_UI : UserControl//, INotifyPropertyChanged
    {
       // public event PropertyChangedEventHandler PropertyChanged;

        public CMiX_UI()
        {
            InitializeComponent();
            this.DataContext = this;

            //Ch0_Alpha.ApplyTemplate();
            Ch1_Alpha.ApplyTemplate();
            Ch2_Alpha.ApplyTemplate();
            Ch3_Alpha.ApplyTemplate();
            Ch4_Alpha.ApplyTemplate();
            Ch5_Alpha.ApplyTemplate();

            CameraZoom.ApplyTemplate();
            CameraFOV.ApplyTemplate();

            /*Thumb thumb0 = (Ch0_Alpha.Template.FindName("PART_Track", Ch0_Alpha) as Track).Thumb;
            thumb0.MouseEnter += new MouseEventHandler(thumb_MouseEnter);*/
            Thumb thumb1 = (Ch1_Alpha.Template.FindName("PART_Track", Ch1_Alpha) as Track).Thumb;
            thumb1.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Thumb thumb2 = (Ch2_Alpha.Template.FindName("PART_Track", Ch2_Alpha) as Track).Thumb;
            thumb2.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Thumb thumb3 = (Ch3_Alpha.Template.FindName("PART_Track", Ch3_Alpha) as Track).Thumb;
            thumb3.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Thumb thumb4 = (Ch4_Alpha.Template.FindName("PART_Track", Ch4_Alpha) as Track).Thumb;
            thumb4.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Thumb thumb5 = (Ch5_Alpha.Template.FindName("PART_Track", Ch5_Alpha) as Track).Thumb;
            thumb5.MouseEnter += new MouseEventHandler(thumb_MouseEnter);

            Thumb thumb6 = (CameraZoom.Template.FindName("PART_Track", CameraZoom) as Track).Thumb;
            thumb6.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Thumb thumb7 = (CameraFOV.Template.FindName("PART_Track", CameraFOV) as Track).Thumb;
            thumb7.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
        }

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }


        #region BeatControls

        public static readonly DependencyProperty ChannelBeatResetProperty =
        DependencyProperty.Register("ChannelBeatReset", typeof(ObservableCollection<bool>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<bool>(new[] { false, false, false, false, false, false, false, false })));
        [Bindable(true)]
        public ObservableCollection<bool> ChannelBeatReset
        {
            get { return (ObservableCollection<bool>)this.GetValue(ChannelBeatResetProperty); }
            set { this.SetValue(ChannelBeatResetProperty, value); }
        }

        public static readonly DependencyProperty ChannelBeatPauseProperty =
        DependencyProperty.Register("ChannelBeatPause", typeof(ObservableCollection<bool>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<bool>(new[] { false, false, false, false, false, false, false, false })));
        [Bindable(true)]
        public ObservableCollection<bool> ChannelBeatPause
        {
            get { return (ObservableCollection<bool>)this.GetValue(ChannelBeatPauseProperty); }
            set { this.SetValue(ChannelBeatPauseProperty, value); }
        }

        public static readonly DependencyProperty ChannelBeatMultiplyProperty =
        DependencyProperty.Register("ChannelBeatMultiply", typeof(ObservableCollection<bool>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<bool>(new[] { false, false, false, false, false, false, false, false })));
        [Bindable(true)]
        public ObservableCollection<bool> ChannelBeatMultiply
        {
            get { return (ObservableCollection<bool>)this.GetValue(ChannelBeatMultiplyProperty); }
            set { this.SetValue(ChannelBeatMultiplyProperty, value); }
        }

        public static readonly DependencyProperty ChannelBeatDivideProperty =
        DependencyProperty.Register("ChannelBeatDivide", typeof(ObservableCollection<bool>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<bool>(new[] { false, false, false, false, false, false, false, false })));
        [Bindable(true)]
        public ObservableCollection<bool> ChannelBeatDivide
        {
            get { return (ObservableCollection<bool>)this.GetValue(ChannelBeatDivideProperty); }
            set { this.SetValue(ChannelBeatDivideProperty, value); }
        }

        public static readonly DependencyProperty ChannelChanceToHitProperty =
        DependencyProperty.Register("ChannelChanceToHit", typeof(ObservableCollection<double>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 })));
        [Bindable(true)]
        public ObservableCollection<double> ChannelChanceToHit
        {
            get { return (ObservableCollection<double>)this.GetValue(ChannelChanceToHitProperty); }
            set { this.SetValue(ChannelChanceToHitProperty, value); }
        }

        public static readonly DependencyProperty ChannelBeatDisplayProperty =
        DependencyProperty.Register("ChannelBeatDisplay", typeof(ObservableCollection<string>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<string>(new[] { "0", "0", "0", "0", "0", "0", "0", "0" })));
        [Bindable(true)]
        public ObservableCollection<string> ChannelBeatDisplay
        {
            get { return (ObservableCollection<string>)this.GetValue(ChannelBeatDisplayProperty); }
            set { this.SetValue(ChannelBeatDisplayProperty, value); }
        }
        #endregion

        #region ChannelsParameters
        private ObservableCollection<double> _ChannelsAlpha = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelsAlpha
        {
            get { return _ChannelsAlpha; }
            set { _ChannelsAlpha = value; }
        }


        private ObservableCollection<Color> _ChannelColors = new ObservableCollection<Color>(new[] { Color.FromArgb(0, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0) });
        public ObservableCollection<Color> ChannelColors
        {
            get { return _ChannelColors; }
            set { _ChannelColors = value; }
        }

        private ObservableCollection<Color> _ChannelBgColors = new ObservableCollection<Color>(new[] { Color.FromArgb(0, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 0, 0) });
        public ObservableCollection<Color> ChannelBgColors
        {
            get { return _ChannelBgColors; }
            set { _ChannelBgColors = value; }
        }

        private ObservableCollection<double> _ChannelsHueRange = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelsHueRange
        {
            get { return _ChannelsHueRange; }
            set { _ChannelsHueRange = value; }
        }

        private ObservableCollection<double> _ChannelsSatRange = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelsSatRange
        {
            get { return _ChannelsSatRange; }
            set { _ChannelsSatRange = value; }
        }

        private ObservableCollection<double> _ChannelsValRange = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelsValRange
        {
            get { return _ChannelsValRange; }
            set { _ChannelsValRange = value; }
        }


        private ObservableCollection<string> _ChannelsBPM = new ObservableCollection<string>(new[] { "0", "0", "0", "0", "0", "0"});
        public ObservableCollection<string> ChannelsBPM
        {
            get { return _ChannelsBPM; }
            set { _ChannelsBPM = value; }
        }

        private ObservableCollection<string> _ChannelsBlendMode = new ObservableCollection<string>(new[] { ""});
        public ObservableCollection<string> ChannelsBlendMode
        {
            get { return _ChannelsBlendMode; }
            set { _ChannelsBlendMode = value; }
        }

        private ObservableCollection<string> _SelectedBlendMode = new ObservableCollection<string>(new[] { "Normal", "Normal", "Normal", "Normal", "Normal", "Normal" });
        public ObservableCollection<string> SelectedBlendMode
        {
            get { return _SelectedBlendMode; }
            set { _SelectedBlendMode = value; }
        }

        private ObservableCollection<bool> _MultiplyBPM = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> MultiplyBPM
        {
            get { return _MultiplyBPM; }
            set { _MultiplyBPM = value; }
        }

        private void MultiplyBPM_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int buttonTag = int.Parse(button.Tag.ToString());
            MultiplyBPM[buttonTag] = true;
        }

        private ObservableCollection<bool> _DivideBPM = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> DivideBPM
        {
            get { return _DivideBPM; }
            set { _DivideBPM = value; }
        }
        private void DivideBPM_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int buttonTag = int.Parse(button.Tag.ToString());
            DivideBPM[buttonTag] = true;
        }

        private ObservableCollection<bool> _ResetBPM = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> ResetBPM
        {
            get { return _ResetBPM; }
            set { _ResetBPM = value; }
        }
        private void ResetBPM_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int buttonTag = int.Parse(button.Tag.ToString());
            ResetBPM[buttonTag] = true;
        }

        #region Channel Color Correction

        private ObservableCollection<double> _ChannelBrightness = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelBrightness
        {
            get { return _ChannelBrightness; }
            set { _ChannelBrightness = value; }
        }

        private ObservableCollection<double> _ChannelSaturation = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelSaturation
        {
            get { return _ChannelSaturation; }
            set { _ChannelSaturation = value; }
        }

        private ObservableCollection<double> _ChannelContrast = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelContrast
        {
            get { return _ChannelContrast; }
            set { _ChannelContrast = value; }
        }

        private ObservableCollection<double> _ChannelInvert = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> ChannelInvert
        {
            get { return _ChannelInvert; }
            set { _ChannelInvert = value; }
        }
        #region VideoFlipXY
        private ObservableCollection<bool> _VideoFlipX = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoFlipX
        {
            get { return _VideoFlipX; }
            set { _VideoFlipX = value; }
        }

        private ObservableCollection<bool> _VideoFlipY = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoFlipY
        {
            get { return _VideoFlipY; }
            set { _VideoFlipY = value; }
        }
        #endregion

        #endregion

        #endregion

        #region MainBPMParameters
        public static readonly DependencyProperty MainBPMProperty =
        DependencyProperty.Register("MainBPM", typeof(string), typeof(CMiX_UI), new PropertyMetadata("0"));

        [Bindable(true)]
        public string MainBPM
        {
            get { return (string)this.GetValue(MainBPMProperty); }
            set { this.SetValue(MainBPMProperty, value); }
        }

        public static readonly DependencyProperty TapBPMProperty =
        DependencyProperty.Register("TapBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));

        [Bindable(true)]
        public bool TapBPM
        {
            get { return (bool)this.GetValue(TapBPMProperty); }
            set { this.SetValue(TapBPMProperty, value); }
        }

        private void Main_TapBPM_Click(object sender, RoutedEventArgs e)
        {
                    TapBPM = true;
        }

        public static readonly DependencyProperty ResyncBPMProperty =
        DependencyProperty.Register("ResyncBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));

        [Bindable(true)]
        public bool ResyncBPM
        {
            get { return (bool)this.GetValue(ResyncBPMProperty); }
            set { this.SetValue(ResyncBPMProperty, value); }
        }

        private void Main_ResyncBPM_Click(object sender, RoutedEventArgs e)
        {
        ResyncBPM = true;

                }

        public static readonly DependencyProperty Main_MulBPMProperty =
        DependencyProperty.Register("Main_MulBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));

        [Bindable(true)]
        public bool Main_MulBPM
        {
            get { return (bool)this.GetValue(Main_MulBPMProperty); }
            set { this.SetValue(Main_MulBPMProperty, value); }
        }

        private void Main_MulBPM_Click(object sender, RoutedEventArgs e)
        {
            Main_MulBPM = true;
        }

        public static readonly DependencyProperty Main_DivBPMProperty =
        DependencyProperty.Register("Main_DivBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));

        [Bindable(true)]
        public bool Main_DivBPM
        {
            get { return (bool)this.GetValue(Main_DivBPMProperty); }
            set { this.SetValue(Main_DivBPMProperty, value); }
        }

        private void Main_DivBPM_Click(object sender, RoutedEventArgs e)
        {
        Main_DivBPM = true;
        }

        public static readonly DependencyProperty Main_ResBPMProperty =
        DependencyProperty.Register("Main_ResetBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));

        [Bindable(true)]
        public bool Main_ResBPM
        {
            get { return (bool)this.GetValue(Main_ResBPMProperty); }
            set { this.SetValue(Main_ResBPMProperty, value); }
        }

        private void Main_ResBPM_Click(object sender, RoutedEventArgs e)
        {
            Main_ResBPM = true;
        }
        #endregion

        #region CameraParameters
        private ObservableCollection<string> _CamRotationStyle = new ObservableCollection<string>(new[] { "" });
        public ObservableCollection<string> CamRotationStyle
        {
            get { return _CamRotationStyle; }
            set { _CamRotationStyle = value; }
        }

        public static readonly DependencyProperty SelectedCamRotationStyleProperty =
        DependencyProperty.Register("SelectedCamRotationStyle", typeof(string), typeof(CMiX_UI));
        [Bindable(true)]
        public string SelectedCamRotationStyle
        {
            get { return (string)this.GetValue(SelectedCamRotationStyleProperty); }
            set { this.SetValue(SelectedCamRotationStyleProperty, value); }
        }


        private ObservableCollection<string> _CamLookAtStyle = new ObservableCollection<string>(new[] { "" });
        public ObservableCollection<string> CamLookAtStyle
        {
            get { return _CamLookAtStyle; }
            set { _CamLookAtStyle = value; }
        }


        public static readonly DependencyProperty SelectedCamLookAtStyleProperty =
        DependencyProperty.Register("SelectedCamLookAtStyle", typeof(string), typeof(CMiX_UI));
        [Bindable(true)]
        public string SelectedCamLookAtStyle
        {
            get { return (string)this.GetValue(SelectedCamLookAtStyleProperty); }
            set { this.SetValue(SelectedCamLookAtStyleProperty, value); }
        }


        private ObservableCollection<string> _CamViewStyle = new ObservableCollection<string>(new[] { "" });
        public ObservableCollection<string> CamViewStyle
        {
            get { return _CamViewStyle; }
            set { _CamViewStyle = value; }
        }


        public static readonly DependencyProperty SelectedCamViewStyleProperty =
        DependencyProperty.Register("SelectedCamViewStyle", typeof(string), typeof(CMiX_UI));
        [Bindable(true)]
        public string SelectedCamViewStyle
        {
            get { return (string)this.GetValue(SelectedCamViewStyleProperty); }
            set { this.SetValue(SelectedCamViewStyleProperty, value); }
        }


        public static readonly DependencyProperty CamCutsProperty =
        DependencyProperty.Register("CamCuts", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool CamCuts
        {
            get { return (bool)this.GetValue(CamCutsProperty); }
            set { this.SetValue(CamCutsProperty, value); }
        }


        public static readonly DependencyProperty CamZoomProperty =
        DependencyProperty.Register("CamZoom", typeof(double), typeof(CMiX_UI));
        [Bindable(true)]
        public double CamZoom
        {
            get { return (double)this.GetValue(CamZoomProperty); }
            set { this.SetValue(CamZoomProperty, value); }
        }


        public static readonly DependencyProperty CamFOVProperty =
        DependencyProperty.Register("CamFOV", typeof(double), typeof(CMiX_UI));
        [Bindable(true)]
        public double CamFOV
        {
            get { return (double)this.GetValue(CamFOVProperty); }
            set { this.SetValue(CamFOVProperty, value); }
        }


        public static readonly DependencyProperty Cam_MulBPMProperty =
        DependencyProperty.Register("Cam_MulBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool Cam_MulBPM
        {
            get { return (bool)this.GetValue(Cam_MulBPMProperty); }
            set { this.SetValue(Cam_MulBPMProperty, value); }
        }


        private void Cam_MulBPM_Click(object sender, RoutedEventArgs e)
        {
        Cam_MulBPM = true;
        }

        public static readonly DependencyProperty Cam_DivBPMProperty =
        DependencyProperty.Register("Cam_DivBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool Cam_DivBPM
        {
            get { return (bool)this.GetValue(Cam_DivBPMProperty); }
            set { this.SetValue(Cam_DivBPMProperty, value); }
        }

        private void Cam_DivBPM_Click(object sender, RoutedEventArgs e)
        {
        Cam_DivBPM = true;
        }

        public static readonly DependencyProperty Cam_ResBPMProperty =
        DependencyProperty.Register("Cam_ResBPM", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool Cam_ResBPM
        {
            get { return (bool)this.GetValue(Cam_ResBPMProperty); }
            set { this.SetValue(Cam_ResBPMProperty, value); }
        }

        private void Cam_ResBPM_Click(object sender, RoutedEventArgs e)
        {
            Cam_ResBPM = true;
        }

        public static readonly DependencyProperty Cam_DisBPMProperty =
        DependencyProperty.Register("Cam_DisBPM", typeof(string), typeof(CMiX_UI), new PropertyMetadata("0"));
        [Bindable(true)]
        public string Cam_DisBPM
        {
            get { return (string)this.GetValue(Cam_DisBPMProperty); }
            set { this.SetValue(Cam_DisBPMProperty, value); }
        }
        #endregion


        #region VideoParameters

        #region Video File Selector
        public static readonly DependencyProperty VideoDefaultFolderProperty =
        DependencyProperty.Register("VideoDefaultFolder", typeof(string), typeof(CMiX_UI), new PropertyMetadata("C:"));
        [Bindable(true)]
        public string VideoDefaultFolder
        {
            get { return (string)this.GetValue(VideoDefaultFolderProperty); }
            set { this.SetValue(VideoDefaultFolderProperty, value); }
        }

        public static readonly DependencyProperty VideoFileNamesProperty =
               DependencyProperty.Register("VideoFileNames", typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>), typeof(CMiX_UI),
               new PropertyMetadata(new ObservableCollection<ObservableCollection<ListBoxFileName>>{
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  })}));
        [Bindable(true)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> VideoFileNames
        {
            get { return (ObservableCollection<ObservableCollection<ListBoxFileName>>)this.GetValue(VideoFileNamesProperty); }
            set { this.SetValue(VideoFileNamesProperty, value); }
        }
        #endregion

        #region Video PlayerControl

        private ObservableCollection<bool> _VideoPlay = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoPlay
        {
        get { return _VideoPlay; }
        set { _VideoPlay = value; }
        }

        private ObservableCollection<bool> _VideoLoop = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoLoop
        {
        get { return _VideoLoop; }
        set { _VideoLoop = value; }
        }

        private ObservableCollection<bool> _VideoOnBeat = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoOnBeat
        {
        get { return _VideoOnBeat; }
        set { _VideoOnBeat = value; }
        }

        private ObservableCollection<bool> _VideoReset = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> VideoReset
        {
        get { return _VideoReset; }
        set { _VideoReset = value; }
        }

        private void ResetVideo_Click(object sender, RoutedEventArgs e)
        {
        var channel = sender as ChannelControls;
        int channelTag = int.Parse(channel.Tag.ToString());
        VideoReset[channelTag] = true;
        }

        #endregion

        #region Video Speed

        private ObservableCollection<double> _VideoSpeed = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> VideoSpeed
        {
        get { return _VideoSpeed; }
        set { _VideoSpeed = value; }
        }

        #endregion

        #endregion

        #region Sprite Parameters

        #region Sprite File Selector

        public static readonly DependencyProperty SpriteDefaultFolderProperty =
        DependencyProperty.Register("SpriteDefaultFolder", typeof(string), typeof(CMiX_UI), new PropertyMetadata("C:"));
        [Bindable(true)]
        public string SpriteDefaultFolder
        {
            get { return (string)this.GetValue(SpriteDefaultFolderProperty); }
            set { this.SetValue(SpriteDefaultFolderProperty, value); }
        }

        public static readonly DependencyProperty SpriteFileNamesProperty =
        DependencyProperty.Register("SpriteFileNames", typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>), typeof(CMiX_UI),
        new PropertyMetadata(new ObservableCollection<ObservableCollection<ListBoxFileName>>{
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  })}));
        [Bindable(true)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> SpriteFileNames
        {
            get { return (ObservableCollection<ObservableCollection<ListBoxFileName>>)this.GetValue(SpriteFileNamesProperty); }
            set { this.SetValue(SpriteFileNamesProperty, value); }
        }

        #endregion

        #region Sprite Geometry
        private ObservableCollection<string> _SpriteGeometryNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> SpriteGeometryNames
        {
            get { return _SpriteGeometryNames; }
            set { _SpriteGeometryNames = value; }
        }

        private ObservableCollection<string> _SelectedSpriteGeometryNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedSpriteGeometryNames
        {
            get { return _SelectedSpriteGeometryNames; }
            set { _SelectedSpriteGeometryNames = value; }
        }
        #endregion

        #region Sprite 2D/3D
        private ObservableCollection<bool> _Sprite2D3D = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> Sprite2D3D
        {
            get { return _Sprite2D3D; }
            set { _Sprite2D3D = value; }
        }
        #endregion

        #region Texture AspectRatio
        private ObservableCollection<bool> _TextureAspectRatio = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> TextureAspectRatio
        {
            get { return _TextureAspectRatio; }
            set { _TextureAspectRatio = value; }
        }
        #endregion

        #region Sprite SpreadCount
        private ObservableCollection<string> _SpriteSpreadCountNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> SpriteSpreadCountNames
        {
            get { return _SpriteSpreadCountNames; }
            set { _SpriteSpreadCountNames = value; }
        }

        private ObservableCollection<string> _SelectedSpriteSpreadCountNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedSpriteSpreadCountNames
        {
            get { return _SelectedSpriteSpreadCountNames; }
            set { _SelectedSpriteSpreadCountNames = value; }
        }
        #endregion

        #region Sprite Transforms
        private ObservableCollection<string> _SpriteTranslateNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> SpriteTranslateNames
        {
            get { return _SpriteTranslateNames; }
            set { _SpriteTranslateNames = value; }
        }

        private ObservableCollection<string> _SelectedSpriteTranslateNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedSpriteTranslateNames
        {
            get { return _SelectedSpriteTranslateNames; }
            set { _SelectedSpriteTranslateNames = value; }
        }

        private ObservableCollection<string> _SpriteScaleNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> SpriteScaleNames
        {
            get { return _SpriteScaleNames; }
            set { _SpriteScaleNames = value; }
        }

        private ObservableCollection<string> _SelectedSpriteScaleNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedSpriteScaleNames
        {
            get { return _SelectedSpriteScaleNames; }
            set { _SelectedSpriteScaleNames = value; }
        }

        private ObservableCollection<string> _SpriteRotationNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> SpriteRotationNames
        {
            get { return _SpriteRotationNames; }
            set { _SpriteRotationNames = value; }
        }

        private ObservableCollection<string> _SelectedSpriteRotationNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedSpriteRotationNames
        {
            get { return _SelectedSpriteRotationNames; }
            set { _SelectedSpriteRotationNames = value; }
        }
        #endregion

        #region Sprite Scale/Rotation
        private ObservableCollection<double> _SpriteScale = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> SpriteScale
        {
            get { return _SpriteScale; }
            set { _SpriteScale = value; }
        }

        private ObservableCollection<double> _SpriteRotation = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> SpriteRotation
        {
            get { return _SpriteRotation; }
            set { _SpriteRotation = value; }
        }
        #endregion

        #endregion

        #region Text Parameters

        #region Open Text Files
    private ObservableCollection<bool> _OpenText = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
    public ObservableCollection<bool> OpenText
    {
        get { return _OpenText; }
        set { _OpenText = value; }
    }

    private void OpenText_Click(object sender, RoutedEventArgs e)
    {
        var channel = sender as ChannelControls;
        int channelTag = int.Parse(channel.Tag.ToString());
        OpenText[channelTag] = true;
    }
    #endregion

        #region Text Selector
    private ObservableCollection<string> _TextContent = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
    public ObservableCollection<string> TextContent
    {
        get { return _TextContent; }
        set { _TextContent = value; }
    }

    private ObservableCollection<string> _CurrentText = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
    public ObservableCollection<string> CurrentText
    {
        get { return _CurrentText; }
        set { _CurrentText = value; }
    }
    #endregion

        #region Text Player

        private ObservableCollection<bool> _TextBackward = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> TextBackward
        {
            get { return _TextBackward; }
            set { _TextBackward = value; }
        }

        private ObservableCollection<bool> _TextManual = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> TextManual
        {
            get { return _TextManual; }
            set { _TextManual = value; }
        }

        private ObservableCollection<bool> _TextNext = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> TextNext
        {
            get { return _TextNext; }
            set { _TextNext = value; }
        }
        private void TextNext_Click(object sender, RoutedEventArgs e)
        {
            var channel = sender as ChannelControls;
            int channelTag = int.Parse(channel.Tag.ToString());
            TextNext[channelTag] = true;
        }

        private ObservableCollection<bool> _TextReset = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> TextReset
        {
            get { return _TextReset; }
            set { _TextReset = value; }
        }
        private void TextReset_Click(object sender, RoutedEventArgs e)
        {
            var channel = sender as ChannelControls;
            int channelTag = int.Parse(channel.Tag.ToString());
            TextReset[channelTag] = true;
        }
        #endregion

        #region Text 2D/3D
        private ObservableCollection<bool> _Text2D3D = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> Text2D3D
        {
            get { return _Text2D3D; }
            set { _Text2D3D = value; }
        }
        #endregion

        #region Text SpreadCount

        private ObservableCollection<string> _TextSpreadCountNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextSpreadCountNames
        {
            get { return _TextSpreadCountNames; }
            set { _TextSpreadCountNames = value; }
        }

        private ObservableCollection<string> _SelectedTextSpreadCountNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextSpreadCountNames
        {
            get { return _SelectedTextSpreadCountNames; }
            set { _SelectedSpriteSpreadCountNames = value; }
        }

        #endregion

        #region Text Transforms

        private ObservableCollection<string> _TextCharTransformNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextCharTransformNames
        {
            get { return _TextCharTransformNames; }
            set { _TextCharTransformNames = value; }
        }

        private ObservableCollection<string> _SelectedTextCharTransformNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextCharTransformNames
        {
            get { return _SelectedTextCharTransformNames; }
            set { _SelectedTextCharTransformNames = value; }
        }

        #endregion

        #region Text Transforms
        private ObservableCollection<string> _TextTranslateNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextTranslateNames
        {
            get { return _TextTranslateNames; }
            set { _TextTranslateNames = value; }
        }

        private ObservableCollection<string> _SelectedTextTranslateNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextTranslateNames
        {
            get { return _SelectedTextTranslateNames; }
            set { _SelectedTextTranslateNames = value; }
        }

        private ObservableCollection<string> _TextScaleNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextScaleNames
        {
            get { return _TextScaleNames; }
            set { _TextScaleNames = value; }
        }

        private ObservableCollection<string> _SelectedTextScaleNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextScaleNames
        {
            get { return _SelectedTextScaleNames; }
            set { _SelectedTextScaleNames = value; }
        }

        private ObservableCollection<string> _TextRotationNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextRotationNames
        {
            get { return _TextRotationNames; }
            set { _TextRotationNames = value; }
        }

        private ObservableCollection<string> _SelectedTextRotationNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextRotationNames
        {
            get { return _SelectedTextRotationNames; }
            set { _SelectedTextRotationNames = value; }
        }
        #endregion

        #region Text Font
        private ObservableCollection<double> _TextScale = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> TextScale
        {
            get { return _TextScale; }
            set { _TextScale = value; }
        }

        private ObservableCollection<double> _TextRotation = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> TextRotation
        {
            get { return _TextRotation; }
            set { _TextRotation = value; }
        }

        private ObservableCollection<string> _TextFontnameNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TextFontnameNames
        {
            get { return _TextFontnameNames; }
            set { _TextFontnameNames = value; }
        }

        private ObservableCollection<string> _SelectedTextFontnameNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTextFontnameNames
        {
            get { return _SelectedTextFontnameNames; }
            set { _SelectedTextFontnameNames = value; }
        }
        #endregion

        #endregion

        #region Geometry Parameters

        #region Geometry File Selector

        public static readonly DependencyProperty GeometryDefaultFolderProperty =
        DependencyProperty.Register("GeometryDefaultFolder", typeof(string), typeof(CMiX_UI), new PropertyMetadata("C:"));
        [Bindable(true)]
        public string GeometryDefaultFolder
        {
            get { return (string)this.GetValue(GeometryDefaultFolderProperty); }
            set { this.SetValue(GeometryDefaultFolderProperty, value); }
        }

        public static readonly DependencyProperty GeometryFileNamesProperty =
        DependencyProperty.Register("GeometryFileNames", typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>), typeof(CMiX_UI),
        new PropertyMetadata(new ObservableCollection<ObservableCollection<ListBoxFileName>>{
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  })}));
        [Bindable(true)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> GeometryFileNames
        {
            get { return (ObservableCollection<ObservableCollection<ListBoxFileName>>)this.GetValue(GeometryFileNamesProperty); }
            set { this.SetValue(GeometryFileNamesProperty, value); }
        }
        #endregion

        #endregion

        #region Mask Parameters

        #region Mask File Selector

        public static readonly DependencyProperty MaskDefaultFolderProperty =
        DependencyProperty.Register("MaskDefaultFolder", typeof(string), typeof(CMiX_UI), new PropertyMetadata("C:"));
        [Bindable(true)]
        public string MaskDefaultFolder
        {
            get { return (string)this.GetValue(MaskDefaultFolderProperty); }
            set { this.SetValue(MaskDefaultFolderProperty, value); }
        }

        public static readonly DependencyProperty MaskFileNamesProperty =
               DependencyProperty.Register("MaskFileNames", typeof(ObservableCollection<ObservableCollection<ListBoxFileName>>), typeof(CMiX_UI),
               new PropertyMetadata(new ObservableCollection<ObservableCollection<ListBoxFileName>>{
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  }),
                           new ObservableCollection<ListBoxFileName>( new ListBoxFileName[] {  })}));
        [Bindable(true)]
        public ObservableCollection<ObservableCollection<ListBoxFileName>> MaskFileNames
        {
            get { return (ObservableCollection<ObservableCollection<ListBoxFileName>>)this.GetValue(MaskFileNamesProperty); }
            set { this.SetValue(MaskFileNamesProperty, value); }
        }
        #endregion

        #region Mask 2D/3D
        private ObservableCollection<bool> _Mask2D3D = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> Mask2D3D
        {
            get { return _Mask2D3D; }
            set { _Mask2D3D = value; }
        }
        #endregion

        #region Mask Transforms
        private ObservableCollection<string> _MaskTranslateNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> MaskTranslateNames
        {
            get { return _MaskTranslateNames; }
            set { _MaskTranslateNames = value; }
        }

        private ObservableCollection<string> _SelectedMaskTranslateNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedMaskTranslateNames
        {
            get { return _SelectedMaskTranslateNames; }
            set { _SelectedMaskTranslateNames = value; }
        }

        private ObservableCollection<string> _MaskScaleNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> MaskScaleNames
        {
            get { return _MaskScaleNames; }
            set { _MaskScaleNames = value; }
        }

        private ObservableCollection<string> _SelectedMaskScaleNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedMaskScaleNames
        {
            get { return _SelectedMaskScaleNames; }
            set { _SelectedMaskScaleNames = value; }
        }

        private ObservableCollection<string> _MaskRotationNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> MaskRotationNames
        {
            get { return _MaskRotationNames; }
            set { _MaskRotationNames = value; }
        }

        private ObservableCollection<string> _SelectedMaskRotationNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedMaskRotationNames
        {
            get { return _SelectedMaskRotationNames; }
            set { _SelectedMaskRotationNames = value; }
        }
        #endregion

        #region Mask On
        private ObservableCollection<bool> _MaskOn = new ObservableCollection<bool>(new[] { false, false, false, false, false, false });
        public ObservableCollection<bool> MaskOn
        {
            get { return _MaskOn; }
            set { _MaskOn = value; }
        }
        #endregion

        #region Mask Scale Rotate
        private ObservableCollection<double> _MaskScale = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> MaskScale
        {
            get { return _MaskScale; }
            set { _MaskScale = value; }
        }

        private ObservableCollection<double> _MaskRotation = new ObservableCollection<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
        public ObservableCollection<double> MaskRotation
        {
            get { return _MaskRotation; }
            set { _MaskRotation = value; }
        }
        #endregion

        #region Mask SpreadCount
        private ObservableCollection<string> _MaskSpreadCountNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> MaskSpreadCountNames
        {
            get { return _MaskSpreadCountNames; }
            set { _MaskSpreadCountNames = value; }
        }

        private ObservableCollection<string> _SelectedMaskSpreadCountNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedMaskSpreadCountNames
        {
            get { return _SelectedMaskSpreadCountNames; }
            set { _SelectedMaskSpreadCountNames = value; }
        }
        #endregion

        #endregion

        #region Texture Transform
        private ObservableCollection<string> _TexTransformNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> TexTransformNames
        {
            get { return _TexTransformNames; }
            set { _TexTransformNames = value; }
        }

        private ObservableCollection<string> _SelectedTexTransformNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedTexTransformNames
        {
            get { return _SelectedTexTransformNames; }
            set { _SelectedTexTransformNames = value; }
        }
        #endregion
        #region FFT Parameters

        public static readonly DependencyProperty FFTAmountProperty =
        DependencyProperty.Register("FFTAmount", typeof(ObservableCollection<double>), typeof(CMiX_UI), new PropertyMetadata(new ObservableCollection<double>(new[] { 0.84, 0.0, 0.0, 0.0, 0.0, 0.0 })));
        [Bindable(true)]
        public ObservableCollection<double> FFTAmount
        {
            get { return (ObservableCollection<double>)this.GetValue(FFTAmountProperty); }
            set { this.SetValue(FFTAmountProperty, value); }
        }

        private ObservableCollection<string> _FFTNames = new ObservableCollection<string>(new[] { "null" });
        public ObservableCollection<string> FFTNames
        {
            get { return _FFTNames; }
            set { _FFTNames = value; }
        }

        private ObservableCollection<string> _SelectedFFTNames = new ObservableCollection<string>(new[] { "null", "null", "null", "null", "null", "null" });
        public ObservableCollection<string> SelectedFFTNames
        {
            get { return _SelectedFFTNames; }
            set { _SelectedFFTNames = value; }
        }

        #endregion

        private void Main_PauseBPM_Click(object sender, RoutedEventArgs e)
        {

        }


        #region POSTFX
        public static readonly DependencyProperty FX1Property =
        DependencyProperty.Register("FX1", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool FX1
        {
            get { return (bool)this.GetValue(FX1Property); }
            set { this.SetValue(FX1Property, value); }
        }

        public static readonly DependencyProperty FX2Property =
        DependencyProperty.Register("FX2", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool FX2
        {
            get { return (bool)this.GetValue(FX2Property); }
            set { this.SetValue(FX2Property, value); }
        }

        public static readonly DependencyProperty FX3Property =
        DependencyProperty.Register("FX3", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool FX3
        {
            get { return (bool)this.GetValue(FX3Property); }
            set { this.SetValue(FX3Property, value); }
        }

        public static readonly DependencyProperty FX4Property =
        DependencyProperty.Register("FX4", typeof(bool), typeof(CMiX_UI), new PropertyMetadata(false));
        [Bindable(true)]
        public bool FX4
        {
            get { return (bool)this.GetValue(FX4Property); }
            set { this.SetValue(FX4Property, value); }
        }
        public static readonly DependencyProperty SaturationProperty =
        DependencyProperty.Register("Saturation", typeof(double), typeof(CMiX_UI), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double Saturation
        {
            get { return (double)this.GetValue(SaturationProperty); }
            set { this.SetValue(SaturationProperty, value); }
        }

        public static readonly DependencyProperty BrightnessProperty =
        DependencyProperty.Register("Brightness", typeof(double), typeof(CMiX_UI), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double Brightness
        {
            get { return (double)this.GetValue(BrightnessProperty); }
            set { this.SetValue(BrightnessProperty, value); }
        }

        public static readonly DependencyProperty ContrastProperty =
        DependencyProperty.Register("Contrast", typeof(double), typeof(CMiX_UI), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double Contrast
        {
            get { return (double)this.GetValue(ContrastProperty); }
            set { this.SetValue(ContrastProperty, value); }
        }
        #endregion

        private void CMiX_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.D1 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                //Channel0.IsSelected = true;
            }
            if(e.Key == Key.D2 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Channel1.IsSelected = true;
            }
            if(e.Key == Key.D3 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Channel2.IsSelected = true;
            }
            if(e.Key == Key.D4 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Channel3.IsSelected = true;
            }
            if(e.Key == Key.D5 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Channel4.IsSelected = true;
            }
            if(e.Key == Key.D6 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Channel5.IsSelected = true;
            }
        }

        private void ChannelTab_DragOver(object sender, DragEventArgs e)
        {
            TabItem SelTabItem = sender as TabItem;
            SelTabItem.IsSelected = true;
        }

        private void FileBin_PreviewDrop(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                var fileDrop = e.Data.GetData(DataFormats.FileDrop, true);
                var filesOrDirectories = fileDrop as string[];
                if (filesOrDirectories != null && filesOrDirectories.Length > 0)
                {
                    foreach (string fullPath in filesOrDirectories)
                    {
                        if (Directory.Exists(fullPath))
                        {
                            MessageBox.Show(@"{0} is a directory" + fullPath);
                        }
                        else if (File.Exists(fullPath))
                        {
                            MessageBox.Show(@"{0} is a file", fullPath);
                        }
                        else
                        {
                            MessageBox.Show(@"{0} is not a file and not a directory", fullPath);
                        }
                    }
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }

}
 