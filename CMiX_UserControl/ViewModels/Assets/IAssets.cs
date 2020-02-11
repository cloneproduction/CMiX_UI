using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public interface IAssets
    {
        void Rename();
        void AddAsset();
        void RemoveAsset();
        ObservableCollection<IAssets> ReorderAssets(ObservableCollection<IAssets> parentAssets);

        ICommand RenameCommand { get; set; }
        ICommand RemoveAssetCommand { get; set; }
        ICommand AddAssetCommand { get; set; }

        Enum Ponderation { get; set; }
        string Path { get; set; }
        string Name { get; set; }
        bool IsRenaming { get; set; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        ObservableCollection<IAssets> Assets { get; set; }
    }
}
