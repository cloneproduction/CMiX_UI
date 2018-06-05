using System;
using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class Geometry : UserControl
    {
        public Geometry()
        {
            InitializeComponent();
            DataContext = new ViewModels.Geometry(); 
        }
    }
}
