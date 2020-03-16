using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : ViewModel, IDirectory, IGetSet<DirectoryAssetModel>
    {
        public DirectoryItem()
        {

        }
        public DirectoryItem(string name)
        {
            Name = name;
            Assets = new SortableObservableCollection<IAssets>();
            Assets.CollectionChanged += CollectionChanged;
            IsExpanded = false;
            IsSelected = false;
        }

        public SortableObservableCollection<IAssets> Assets { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private string _ponderation = "a";
        public string Ponderation
        {
            get => _ponderation;
            set => _ponderation = value;
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isRoot = false;
        public bool IsRoot
        {
            get => _isRoot;
            set => SetAndNotify(ref _isRoot, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        public void AddAsset(IAssets asset)
        {
            Assets.Add(asset);
            SortAssets();
        }

        public void RemoveAsset(IAssets asset)
        {
            if (Assets.Contains(asset))
                Assets.Remove(asset);
        }

        public void Rename()
        {
            IsRenaming = true;
        }

        public void SortAssets()
        {
            Assets.Sort(c => c.Name);
            Assets.Sort(c => c.Ponderation.ToString());
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= item_PropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
        }

        public void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name))
                SortAssets();
        }

        public DirectoryAssetModel GetModel()
        {
            DirectoryAssetModel directoryAssetModel = new DirectoryAssetModel();
            directoryAssetModel.Name = Name;
            foreach (var asset in Assets)
            {
                if(asset is DirectoryItem)
                    directoryAssetModel.AssetModels.Add(((DirectoryItem)asset).GetModel());

                if (asset is GeometryItem)
                    directoryAssetModel.AssetModels.Add(((GeometryItem)asset).GetModel());

                if (asset is TextureItem)
                    directoryAssetModel.AssetModels.Add(((TextureItem)asset).GetModel());
            }
            return directoryAssetModel;
        }

        public void SetViewModel(DirectoryAssetModel model)
        {
            Name = model.Name;
            foreach (var assetModel in model.AssetModels)
            {
                if(assetModel is DirectoryAssetModel)
                {
                    DirectoryItem directoryItem = new DirectoryItem();
                    directoryItem.SetViewModel((DirectoryAssetModel)assetModel);
                    Assets.Add(directoryItem);
                }
                else if(assetModel is GeometryAssetModel)
                {
                    GeometryItem geometryAsset = new GeometryItem();
                    geometryAsset.SetViewModel((GeometryAssetModel)assetModel);
                    Assets.Add(geometryAsset);
                }
                else if(assetModel is TextureAssetModel)
                {
                    TextureItem textureItem = new TextureItem();
                    textureItem.SetViewModel((TextureAssetModel)assetModel);
                    Assets.Add(textureItem);
                }
            }
        }
    }
}