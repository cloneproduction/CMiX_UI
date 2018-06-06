using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using SharpOSC;
using System.Windows.Input;

namespace CMiX
{
    public partial class Composition : UserControl
    {
        //BeatSystem beatsystem = new BeatSystem();
        //CMiXData cmixdata = new CMiXData();
        Messenger message = new Messenger();

        //string IP = "127.0.0.1";
        //int Port = 55555;


        private IList<RadioButton> _items = new ObservableCollection<RadioButton>();

        public Composition()
        {
            InitializeComponent();
            DataContext = new ViewModels.Composition();

            //Singleton.Instance.MessageReceived += Instance_MessageReceived;
        }

        private void Instance_MessageReceived(OscBundle packet)
        {
            /*for (int i = 0; i < packet.Messages.Count; i++)
            {
                cmixdata.ChannelAlpha[i] = Convert.ToDouble(packet.Messages[i].Arguments[0]);
            }*/
        }

        #region Properties
        public static readonly DependencyProperty EnabledOSCProperty =
        DependencyProperty.Register("EnabledOSC", typeof(bool), typeof(Composition), new PropertyMetadata(false));
        [Bindable(true)]
        public bool EnabledOSC
        {
            get { return (bool)GetValue(EnabledOSCProperty); }
            set { SetValue(EnabledOSCProperty, value); }
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

        private List<string> _PresetsFileMask = new List<string>(new[] { ".txt", ".json", ".cmix" });
        public List<string> PresetsFileMask
        {
            get { return _PresetsFileMask; }
            set { _PresetsFileMask = value; }
        }


        #endregion

        #region Events
        private void ChannelTab_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("UIElement") == false)
            {
                RadioButton rb = sender as RadioButton;
                rb.IsChecked = true;
            }
        }

        private void SliderValueChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                /*var control = sender as FrameworkElement;
                cmixdata = (CMiXData)cmix.DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);

                List<string> slidervalue = new List<string>();
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                foreach (double st in (ObservableCollection<double>)list)
                {
                    slidervalue.Add(st.ToString("0.00"));
                }
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), slidervalue.ToArray()));*/
            }
        }

        private void ChannelSliderValueChanged(object sender, System.EventArgs e)
        {
            if (EnabledOSC == true)
            {
                CustomSlider slider = sender as CustomSlider;
                message.SendOSC(slider.Tag.ToString(), slider.Value.ToString());
            }
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                /*var control = sender as ComboBox;
                cmixdata = (CMiXData)DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);

                List<string> slidervalue = new List<string>();
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                foreach (string st in (ObservableCollection<string>)list)
                {
                    slidervalue.Add(st.ToString());
                }
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), slidervalue.ToArray()));*/
            }
        }

        private void ChannelComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                ComboBox combobox = sender as ComboBox;
                message.SendOSC(combobox.Tag.ToString(), combobox.SelectedItem.ToString());
            }
        }

        private void SingleComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                /*var control = sender as ComboBox;
                cmixdata = (CMiXData)DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);
                var list = cmixdata.GetType().GetProperty(control.Tag.ToString()).GetValue(cmixdata, null);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Tag.ToString(), list.ToString()));*/
            }
            Thread.Sleep(10);
        }

        private void BeatControlsChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                BeatControls beatcontrols = sender as BeatControls;
                message.SendOSC(beatcontrols.Tag.ToString(), beatcontrols.Multiplier.ToString());
            }
        }

        private void BeatControlsPostFX_BeatControlChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                /*var control = sender as BeatControls;
                cmixdata = (CMiXData)DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Name, cmixdata.PostFXBeatMultiplier[0].ToString()));*/
            }
        }

        private void BeatControlsCam_BeatControlChanged(object sender, EventArgs e)
        {
            if (cmix != null)
            {
                /*var control = sender as BeatControls;
                cmixdata = (CMiXData)DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);
                OSCSender.Send(new SharpOSC.OscMessage("/" + control.Name, cmixdata.CamBeatMultiplier[0].ToString()));*/
            }
        }

        #region main menu event
        private void New_Click(object sender, RoutedEventArgs e)
        {
            EnabledOSC = false;
            DataContext = new CMiXData();
            EnabledOSC = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            /*CMiXData cmixdata = (CMiXData)DataContext;

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

            }*/
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            /*SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                CMiXData cmixdata = (CMiXData)DataContext;

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
            }*/
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;

                if (Path.GetExtension(file) == ".json")
                {
                    CMiXData data = JsonConvert.DeserializeObject<CMiXData>(File.ReadAllText(openFileDialog.FileName));
                    EnabledOSC = false;

                    DataContext = data;
                    //message.SendAll(data);

                    EnabledOSC = true;
                }
            }*/
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion


        #endregion


        private bool _isDown;
        private bool _isDragging;
        private Point _startPoint;
        private UIElement _realDragSource;
        private UIElement _realDragSource2;
        private int _realDragIndex;
        private UIElement _dummyDragSource = new UIElement();

        private void sp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*if (e.Source == LayerEnableStack)
            {
                
            }
            else
            {
                _isDown = true;
                _startPoint = e.GetPosition(LayerEnableStack);
            }*/
        }

        private void sp_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*_isDown = false;
            _isDragging = false;
            if (_realDragSource != null)
            {
                _realDragSource.ReleaseMouseCapture();
            }*/
        }

        private void sp_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            /*if (_isDown)
            {
                if ((_isDragging == false) && ((Math.Abs(e.GetPosition(LayerEnableStack).X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(LayerEnableStack).Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                {
                    _isDragging = true;
                    _realDragSource = e.Source as UIElement;

                    _realDragIndex = LayerEnableStack.Children.IndexOf(e.Source as UIElement);
                    _realDragSource2 = LayerControlStack.Children[_realDragIndex];
                    _realDragSource.CaptureMouse();
                    DragDrop.DoDragDrop(_dummyDragSource, new DataObject("UIElement", e.Source, true), DragDropEffects.Move);
                }
            }*/
        }

        private void sp_DragEnter(object sender, DragEventArgs e)
        {
            /*if (e.Data.GetDataPresent("UIElement"))
            {
                e.Effects = DragDropEffects.Move;
            }*/
        }

        private void sp_Drop(object sender, DragEventArgs e)
        {
            /*if (e.Data.GetDataPresent("UIElement"))
            {
                CMiXData cmixdata = DataContext as CMiXData;

                UIElement droptarget = e.Source as UIElement;
                int droptargetIndex = -1;
                int i = 0;
                foreach (UIElement element in LayerEnableStack.Children)
                {
                    if (element.Equals(droptarget))
                    {
                        droptargetIndex = i;
                        break;
                    }
                    i++;
                }
                if (droptargetIndex != -1)
                {
                    LayerEnableStack.Children.Remove(_realDragSource);
                    LayerEnableStack.Children.Insert(droptargetIndex, _realDragSource);
                    LayerControlStack.Children.RemoveAt(_realDragIndex);
                    LayerControlStack.Children.Insert(droptargetIndex, _realDragSource2);
                }

                _isDown = false;
                _isDragging = false;
                _realDragSource.ReleaseMouseCapture();

                List<string> ChannelEnableName = new List<string>();
                foreach(ChannelEnable ce in LayerEnableStack.Children)
                {
                    ChannelEnableName.Add(ce.Tag.ToString());
                }
                message.SendOSCList("LayerIndex", ChannelEnableName);
            }*/
        }


        private void FocusUp(FrameworkElement obj)
        {
            /*TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }*/
        }

        private void FocusPrevious(FrameworkElement obj)
        {
            /*TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }*/
        }

        private void RangeSliderValueChanged(object sender, EventArgs e)
        {
            /*if (EnabledOSC == true)
            {
                CMiXRangeSlider slider = sender as CMiXRangeSlider;
                List<string> list = new List<string>();
                list.Add(slider.RangeMin.ToString());
                list.Add(slider.RangeMax.ToString());
                message.SendOSCList(slider.Name, list);
            }*/
        }

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            /*CMiXData cd = DataContext as CMiXData;
            //string name = (layercount).ToString();

            string layername = (LayerEnableStack.Children.Count).ToString();
            ChannelData chdata = new ChannelData ();
            ChannelLayer cl = new ChannelLayer { DataContext = chdata, Name = "ChannelLayer" + layername, Tag = layername };
            Views.LayerControls cc = new Views.LayerControls { DataContext = chdata, Name = "ChannelControl" + layername, Tag = layername };
            ChannelEnable ce = new ChannelEnable { DataContext = chdata, Name = "ChannelEnable" + layername, Tag = layername };

            //cd.ChData.Add(chdata);

            ContentControl.Children.Add(cc);
            LayerControlStack.Children.Add(cl);
            LayerEnableStack.Children.Add(ce);

            List<OscMessage> messagelist = message.ObjectToOscList(chdata, chdata.Name);

            List<string> channelnames = new List<string>();
            foreach(ChannelEnable chenable in LayerEnableStack.Children)
            {
                channelnames.Add("Layer" + chenable.Tag);
            }

            OscMessage layernames = new OscMessage("/LayerNames", channelnames.ToArray());
            messagelist.Add(layernames);

            List<string> ChannelEnableName = new List<string>();
            foreach (ChannelEnable ch in LayerEnableStack.Children)
            {
                ChannelEnableName.Add(ch.Tag.ToString());
            }
            OscMessage layerindex = new OscMessage("/LayerIndex", ChannelEnableName.ToArray());
            messagelist.Add(layerindex);

            message.SendBundle(messagelist);*/
        }

        /*private void RemoveLayer_Click(object sender, RoutedEventArgs e)
        {
            int removedindex = 0;
            CMiXData cd = this.DataContext as CMiXData;

            int layerenablecount = LayerEnableStack.Children.Count;

            for(int i = LayerEnableStack.Children.Count - 1; i >= 0; i--)
            {
                removedindex = i;
                ChannelEnable layerenable = LayerEnableStack.Children[i] as ChannelEnable;
                Views.LayerControls channelcontrols = ContentControl.Children[i] as Views.LayerControls;
                ChannelLayer channellayer = LayerControlStack.Children[i] as ChannelLayer;




                if (layerenable.CheckLayer.IsChecked == true)
                {
                    string name = layerenable.Tag.ToString();
                    for (int j = LayerControlStack.Children.Count - 1; j >= 0; j--)
                    {
                        ChannelLayer layercontrol = LayerControlStack.Children[j] as ChannelLayer;
                        if (layercontrol.Tag.ToString() == name)
                        {
                            LayerControlStack.Children.Remove(layercontrol);
                        }
                    }
                    for (int j = ContentControl.Children.Count - 1; j >= 0; j--)
                    {
                        Views.LayerControls channelcontrol = ContentControl.Children[j] as Views.LayerControls;

                        if (channelcontrol.Tag.ToString() == name)
                        {
                            ContentControl.Children.Remove(channelcontrol);
                        }
                    }

                    LayerEnableStack.Children.Remove(layerenable);

                    int tag = Convert.ToInt32(layerenable.Tag);
                    if (tag > removedindex)
                    {
                        tag -= 1;
                        layerenable.Tag = tag.ToString();
                        layerenable.Name = "ChannelEnable" + tag.ToString();

                        channelcontrols.Tag = tag.ToString();
                        channelcontrols.Name = "ChannelControl" + tag.ToString();

                        channellayer.Tag = tag.ToString();
                        channellayer.Name = "ChannelLayer" + tag.ToString();
                    }
                    break;
                }
            }

            List<OscMessage> messagelist = new List<OscMessage>();
            if (LayerEnableStack.Children.Count > 0)
            {
                List<string> channelnames = new List<string>();
                foreach (ChannelEnable channelenable in LayerEnableStack.Children)
                {
                    channelnames.Add("Layer" + channelenable.Tag.ToString());
                }
                OscMessage layernames = new OscMessage("/LayerNames", channelnames.ToArray());
                messagelist.Add(layernames);
            }

            List<string> ChannelEnableName = new List<string>();
            foreach (ChannelEnable ch in LayerEnableStack.Children)
            {
                int tag = Convert.ToInt32(ch.Tag);
                if (tag > removedindex)
                {
                    tag -= 1;
                    ch.Tag = tag.ToString();
                }
                ChannelEnableName.Add(ch.Tag.ToString());
            }

            OscMessage layerindex = new OscMessage("/LayerIndex", ChannelEnableName.ToArray());
            messagelist.Add(layerindex);

            message.SendBundle(messagelist);


            /*for (int i = cd.ChData.Count - 1; i >= 0; i--)
            {
                if (cd.ChData[i].EnabledLayer == true)
                {
                    if(i == cd.ChData.Count - 1 && cd.ChData.Count > 1) //last item in the list
                    {
                        cd.ChData[i - 1].EnabledLayer = true;
                    }
                    else if (i == 0 && cd.ChData.Count > 1) //first item in the list
                    {
                        cd.ChData[i + 1].EnabledLayer = true;
                    }
                    else if (i > 0 && i < cd.ChData.Count - 1) //item in the middle of the list
                    {
                        cd.ChData[i + 1].EnabledLayer = true;
                    }
                    //cd.ChData.RemoveAt(i);

                    //ContentControl.Children.RemoveAt(i);
                    //LayerControlStack.Children.RemoveAt(i);
                    //LayerEnableStack.Children.RemoveAt(i);
                }
            }
        }*/

        private void cmix_Loaded(object sender, RoutedEventArgs e)
        {
            EnabledOSC = true;
        }

        private void LayerButton_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.C && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                CMiXData cmixdata = this.DataContext as CMiXData;
                foreach (ChannelData channeldata in cmixdata.ChData)
                {
                    if (channeldata.EnabledLayer == true)
                    {
                        DataObject dataObject = new DataObject();
                        dataObject.SetData("ChData", channeldata.Clone(), false);
                        Clipboard.SetDataObject(dataObject);
                        break;
                    }
                }
            }*/

            if (e.Key == Key.V && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                /*foreach (ChannelControls cc in Utils.FindVisualChildren<ChannelControls>(cmix))
                {
                    ChannelData channeldata = cc.DataContext as ChannelData;
                    if (channeldata.EnabledLayer == true)
                    {
                        string channelname = channeldata.Name;
                        //cc.EnabledOSC = false;

                        ChannelData data = (ChannelData)Clipboard.GetData("ChData") as ChannelData;
                        if(data != null)
                        {
                            data.Name = channelname;
                            channeldata = (ChannelData)data.Clone();
                        }
                        //message.SendAll(cc.DataContext, cc.Name);
                        //Task.Delay(100);
                        //cc.EnabledOSC = true;
                    }
                }*/


                /*for (int i = 0; i < cmixdata.ChData.Count; i++)
                {
                    if (cmixdata.ChData[i].EnabledLayer == true)
                    {
                        string name = cmixdata.ChData[i].Name;

                        object data = Clipboard.GetData("ChData");
                        if (data != null)
                        {
                            ChannelData channeldata = data as ChannelData;
                            cmixdata.ChData[i] = (ChannelData)channeldata.Clone();
                            cmixdata.ChData[i].Name = name;
                        }
                        message.SendAll(cmixdata.ChData[i], cmixdata.ChData[i].Name);
                        MessageBox.Show(cmixdata.ChData[i].Name);
                        break;
                        Task.Delay(100);
                    }
                }*/
            }

            /*foreach (ChannelControls cc in Utils.FindVisualChildren<ChannelControls>(cmix))
            {
                if (cc.IsEnabled == true)
                {
                    cc.EnabledOSC = false;

                    object data = Clipboard.GetData("ChData");
                    if(data != null)
                    {
                        cc.DataContext = (ChannelData)data;
                    }
                    message.SendAll(cc.DataContext, cc.Name);
                    Task.Delay(100);
                    cc.EnabledOSC = true;
                }
            }*/
        }

        private void Layer0_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /*if (e.Key == Key.W)
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Up);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
        }*/

        /*if (e.Key == Key.S)
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Down);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
        }*/

        /*if (e.Key == Key.A)
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Left);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
        }*/

        /*if (e.Key == Key.D)
        {
            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Right);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
        }*/

        /*if (e.Key == Key.Add && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
        {
            var uielement = sender as UIElement;
            int count = LayerButtonStack.Children.Count;

            for(int i = 0; i < count; i++)
            {
                RadioButton rb = (RadioButton)LayerButtonStack.Children[i];
                Border br = (Border)LayerControlStack.Children[i];
                if(rb.IsChecked == true && rb.IsFocused == true)
                {
                    if (i > 0)
                    {
                        LayerButtonStack.Children.Remove(rb);
                        LayerButtonStack.Children.Insert(i - 1, rb);
                        LayerControlStack.Children.Remove(br);
                        LayerControlStack.Children.Insert(i - 1, br);
                    }
                }
            }

            List<string> LayerIndex = new List<string>();
            foreach (RadioButton rb in LayerButtonStack.Children)
            {
                LayerIndex.Add(rb.Tag.ToString());
            }

            if (cmix != null)
            {
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);
                OSCSender.Send(new SharpOSC.OscMessage("/" + "LayerIndex", LayerIndex.ToArray()));
            }
        }*/

        /*if (e.Key == Key.Subtract && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
        {
            var uielement = sender as UIElement;
            int count = LayerButtonStack.Children.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                RadioButton rb = (RadioButton)LayerButtonStack.Children[i];
                Border br = (Border)LayerControlStack.Children[i];
                if (rb.IsChecked == true && rb.IsFocused == true)
                {
                    if (i < count - 1)
                    {
                        LayerButtonStack.Children.Remove(rb);
                        LayerButtonStack.Children.Insert(i + 1, rb);
                        LayerControlStack.Children.Remove(br);
                        LayerControlStack.Children.Insert(i + 1, br);
                    }
                }
            }

            List<string> LayerIndex = new List<string>();
            foreach (RadioButton rb in LayerButtonStack.Children)
            {
                LayerIndex.Add(rb.Tag.ToString());
            }

            if (cmix != null)
            {
                cmixdata = (CMiXData)this.DataContext;
                var OSCSender = new SharpOSC.UDPSender(IP, Port);
                OSCSender.Send(new SharpOSC.OscMessage("/" + "LayerIndex", LayerIndex.ToArray()));
            }
        }*/

        /*if(e.Key == Key.B && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            MasterBeatControl.Focus();
        }*/

        /*if(e.Key == Key.NumPad0 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            CameraControl.Focus();
        }*/

        /*if(e.Key == Key.NumPad1 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            RadioButton rb = LayerButtonStack.Children[0] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/

        /*if (e.Key == Key.NumPad2 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            RadioButton rb = LayerButtonStack.Children[1] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/

        /*if (e.Key == Key.NumPad3 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            RadioButton rb = LayerButtonStack.Children[2] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/

        /*if (e.Key == Key.NumPad4 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            /*RadioButton rb = LayerButtonStack.Children[3] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/

        /*if (e.Key == Key.NumPad5 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            RadioButton rb = LayerButtonStack.Children[4] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/

        /*if (e.Key == Key.NumPad6 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            /*RadioButton rb = LayerButtonStack.Children[5] as RadioButton;
            rb.Focus();
            rb.IsChecked = true;
        }*/


    }
}