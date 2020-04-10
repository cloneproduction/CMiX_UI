using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class AssetDragDrop
    {
        public AssetDragDrop()
        {
            SourceCollection = new ObservableCollection<IAssets>();
        }

        public IAssets DragObject { get; set; }
        public ObservableCollection<IAssets> SourceCollection { get; set; }
    }
}