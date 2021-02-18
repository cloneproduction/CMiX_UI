using CMiX.MVVM.Resources;

namespace CMiX.MVVM.ViewModels
{
    public interface IDirectory
    {
        string Path { get; set; }
        string Name { get; set; }
        bool IsSelected { get; set; }
        string Ponderation { get; set; }
        bool IsRenaming { get; set; }
        bool IsExpanded { get; set; }
        bool IsRoot { get; set; }
        SortableObservableCollection<Asset> Assets { get; set; }

        void Rename();
        void AddAsset(Asset asset);
        void RemoveAsset(Asset asset);
        void SortAssets();
    }
}