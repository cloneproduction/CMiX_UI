using System.Collections.Generic;
using System.Windows.Controls;

namespace CMiX
{
    public partial class ChannelLayer : UserControl
    {
        public ChannelLayer()
        {
            InitializeComponent();

        }

        private List<string> _ChannelsBlendMode = new List<string>(new[] { "Normal", "Add", "Substract", "Lighten", "Darken", "Multiply" });
        public List<string> ChannelsBlendMode
        {
            get { return _ChannelsBlendMode; }
            set { _ChannelsBlendMode = value; }
        }
    }
}
