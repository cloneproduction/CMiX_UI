using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Data;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : ViewModel, IAssets
    {
        public DirectoryItem(string name, string path, IAssets parentAsset)
        {
            Assets = new ObservableCollection<IAssets>();
            Assets.CollectionChanged += CollectionChanged;
            InitializeCollectionView();

            ParentAsset = parentAsset;
            IsExpanded = false;
            Name = name;
            Path = path;
            Ponderation = ItemPonderation.DirectoryPonderation;

            AddAssetCommand = new RelayCommand(p => AddAsset());
            RenameCommand = new RelayCommand(p => Rename());
        }

        public ICommand AddAssetCommand { get; set; }
        public ICommand RenameCommand { get; set; }
        public ICommand RemoveAssetCommand { get; set; }

        private ObservableCollection<IAssets> _assets;
        public ObservableCollection<IAssets> Assets
        {
            get { return _assets; }
            set { _assets = value; }
        }

        public ListCollectionView AssetsCollectionView { get; set; }

        private void InitializeCollectionView()
        {
            AssetsCollectionView = new ListCollectionView(Assets);
            SortDescription ponderation = new SortDescription("Ponderation", ListSortDirection.Ascending);
            SortDescription sort = new SortDescription("Name", ListSortDirection.Ascending);
            AssetsCollectionView.SortDescriptions.Add(ponderation);
            AssetsCollectionView.SortDescriptions.Add(sort);
        }


        public IAssets ParentAsset { get; set; }

        private string _path;
        public string Path
        {
            get => _path;
            set => SetAndNotify(ref _path, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public Enum Ponderation { get; set; }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
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
            set
            {
                SetAndNotify(ref _isSelected, value);
                //Console.WriteLine(Name + " IsSelected " + IsSelected);
            }
        }

        public void AddAsset()
        {
            var directoryItem = new DirectoryItem("NewFolder", null, this);
            Assets.Add(directoryItem);
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            IsRenaming = true;
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
            if(e.PropertyName == nameof(Name))
                this.AssetsCollectionView.Refresh();
        }
    }
}
