using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class GeometrySelector : ViewModel
    {
        public GeometrySelector()
        {

        }

        public ObservableCollection<string> GeometryPaths { get; }
    }
}