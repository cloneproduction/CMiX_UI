using System.Windows;
using System.Windows.Controls;
using SharpOSC;

namespace CMiX
{
    public partial class Composition : UserControl
    {
        public Composition()
        {
            InitializeComponent();
            //Singleton.Instance.MessageReceived += Instance_MessageReceived;
        }

        private void Instance_MessageReceived(OscBundle packet)
        {
            /*for (int i = 0; i < packet.Messages.Count; i++)
            {
                cmixdata.ChannelAlpha[i] = Convert.ToDouble(packet.Messages[i].Arguments[0]);
            }*/
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
    }
}