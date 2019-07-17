using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Collections.ObjectModel;

namespace CMiXPlayer.ViewModels
{
    public class Playlist : ViewModel
    {
        public Playlist()
        {
            Compositions = new ObservableCollection<CompositionModel>();
        }

        public string Name { get; set; }

        public ObservableCollection<CompositionModel> Compositions { get; set; }
    }
}
