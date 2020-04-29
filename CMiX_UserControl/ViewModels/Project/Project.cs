using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Services;
using MvvmDialogs;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Specialized;

namespace CMiX.Studio.ViewModels
{
    public class Project : Component, IUndoable
    {
        public Project(int id, string messageAddress, Beat beat, MessengerService messengerService, Mementor mementor, IDialogService dialogService)
            : base(id, beat, messengerService, mementor)
        {
            DialogService = dialogService;

            Assets = new ObservableCollection<IAssets>();
            AssetsFlatten = new ObservableCollection<IAssets>();
            ComponentsInEditing = new ObservableCollection<Component>();

            InitCollectionView();
        }

        #region PROPERTIES
        public IDialogService DialogService { get; set; }

        private ObservableCollection<Component> _componentsInEditing;
        public ObservableCollection<Component> ComponentsInEditing
        {
            get => _componentsInEditing;
            set => SetAndNotify(ref _componentsInEditing, value);
        }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get => _assets;
            set => SetAndNotify(ref _assets, value);
        }

        private ObservableCollection<IAssets> _assetsFlatten;
        public ObservableCollection<IAssets> AssetsFlatten
        {
            get => _assetsFlatten;
            set => SetAndNotify(ref _assetsFlatten, value);
        }

        public CollectionViewSource GeometryViewSource { get; set; }
        private ICollectionView _geometryCollectionView;
        public ICollectionView GeometryCollectionView
        {
            get => _geometryCollectionView;
            set => SetAndNotify(ref _geometryCollectionView, value);
        }

        public CollectionViewSource ImageViewSource { get; set; }
        private ICollectionView _imageCollectionView;
        public ICollectionView ImageCollectionView
        {
            get => _imageCollectionView;
            set => SetAndNotify(ref _imageCollectionView, value);
        }

        public void InitCollectionView()
        {
            this.AssetsFlatten.CollectionChanged += FlattenAssets_CollectionChanged;

            GeometryViewSource = new CollectionViewSource();
            GeometryViewSource.Source = this.AssetsFlatten;
            GeometryCollectionView = GeometryViewSource.View;
            GeometryCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            GeometryCollectionView.Filter = FilterGeometry;

            ImageViewSource = new CollectionViewSource();
            ImageViewSource.Source = this.AssetsFlatten;
            ImageCollectionView = ImageViewSource.View;
            ImageCollectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ImageCollectionView.Filter = FilterImage;
        }

        private void FlattenAssets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GeometryCollectionView.Refresh();
            ImageCollectionView.Refresh();
        }

        public void BuildAssetFlattenCollection(ObservableCollection<IAssets> assets)
        {
            foreach (IAssets asset in assets)
            {
                if (asset is AssetTexture || asset is AssetGeometry)
                    AssetsFlatten.Add(asset);
                else if (asset is AssetDirectory)
                    BuildAssetFlattenCollection(((AssetDirectory)asset).Assets);
            }
        }

        public bool FilterGeometry(object item)
        {
            if (item is AssetGeometry)
                return true;
            else
                return false;
        }

        public bool FilterImage(object item)
        {
            if (item is AssetTexture)
                return true;
            else
                return false;
        }
        #endregion
    }
}