using System.Collections.ObjectModel;

namespace CMiX.Core.Presentation.ViewModels.Assets
{
    public class AssetDragDrop
    {
        public AssetDragDrop()
        {
            SourceCollection = new ObservableCollection<Asset>();
        }

        public Asset DragObject { get; set; }
        public ObservableCollection<Asset> SourceCollection { get; set; }
    }
}