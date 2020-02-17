using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Specialized;

namespace CMiX.Studio.ViewModels
{
    public class AssetSelector<T> : ViewModel, ISendable, IUndoable
    {
        public AssetSelector(string messageAddress, Assets assets, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(AssetSelector<T>)}/"; ;
            Assets = assets;
            MessageService = messageService;
            Mementor = mementor;

            CollectionViewSource = new CollectionViewSource();
            CollectionViewSource.Source = Assets.AvailableResources;

            AssetsView = CollectionViewSource.View;
            AssetsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            AssetsView.Filter = FilterAssets;
        }

        public bool FilterAssets(object item)
        {
            if (item is T)
                return true;
            else
                return false;
        }

        private IAssets _selectedAsset;
        public IAssets SelectedAsset
        {
            get => _selectedAsset;
            set => SetAndNotify(ref _selectedAsset, value);
        }

        public CollectionViewSource CollectionViewSource { get; set; }


        private ICollectionView _assetsView;
        public ICollectionView AssetsView
        {
            get => _assetsView;
            set => SetAndNotify(ref _assetsView, value);
        }

        public Assets Assets { get; set; }
        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }


        public AssetSelectorModel GetModel()
        {
            AssetSelectorModel model = new AssetSelectorModel();
            //model.Name = SelectedAsset.Name;
            //model.Path = SelectedAsset.Path;
            return model;
        }

        public void SetViewModel(AssetSelectorModel model)
        {
            MessageService.Disable();
            //SelectedAsset.Name = model.Name;
            //SelectedAsset.Path = model.Path;
            MessageService.Enable();
        }
    }
}
