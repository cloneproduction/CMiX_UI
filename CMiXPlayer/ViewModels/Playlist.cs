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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public ObservableCollection<CompositionModel> Compositions { get; set; }
    }
}
