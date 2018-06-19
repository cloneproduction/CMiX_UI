using System.Collections.Generic;
using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class LayerControls : UserControl
    {
        public LayerControls()
        {
            InitializeComponent();
            //Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => { EnabledOSC = true; }));
        }

        #region Properties

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
        #endregion
    }
}