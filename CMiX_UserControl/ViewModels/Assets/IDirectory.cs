using CMiX.MVVM.Resources;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public interface IDirectory : IAssets
    {
        bool IsRenaming { get; set; }
        bool IsExpanded { get; set; }
        bool IsRoot { get; set; }
        SortableObservableCollection<IAssets> Assets { get; set; }

        void Rename();
        void AddAsset(IAssets asset);
        void RemoveAsset(IAssets asset);
        void SortAssets();
    }
}