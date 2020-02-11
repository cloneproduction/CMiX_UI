using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class DirectoryItem : ViewModel, IAssets
    {
        public DirectoryItem(string name, string path, IAssets parentAsset)
        {
            Assets = new ObservableCollection<IAssets>();
            ParentAsset = parentAsset;
            IsExpanded = false;
            Name = name;
            Path = path;
            Ponderation = ItemPonderation.DirectoryPonderation;
            if(parentAsset is DirectoryItem)
                ((DirectoryItem)parentAsset).ChildRenamed += this.DirectoryRenamed;


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
            set
            {
                
                
                SetAndNotify(ref _isRenaming, value);
                if (!IsRenaming && ParentAsset != null)
                {
                    Console.WriteLine("RenameChanged false");
                    OnChildRenamed(new EventArgs());
                    //ParentAsset.ReorderAssets(ParentAsset.Assets);
                    //DirectoryRenamed;
                    //ParentAsset.Assets = ReorderAssets(ParentAsset.Assets);
                }
                    
            }
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

        public ObservableCollection<IAssets> ReorderAssets(ObservableCollection<IAssets> assets)
        {
            Console.WriteLine("ReorderAssets");
            ObservableCollection<IAssets> temp;
            temp = new ObservableCollection<IAssets>(assets.OrderBy($"{nameof(IAssets.Ponderation)}, {nameof(IAssets.Name)}"));
            assets.Clear();
            foreach (IAssets j in temp) assets.Add(j);
            return new ObservableCollection<IAssets>(assets.OrderBy($"{nameof(IAssets.Ponderation)}, {nameof(IAssets.Name)}")); ;
        }

        public void AddAsset()
        {
            var directoryItem = new DirectoryItem("NewFolder", null, this);
            directoryItem.ChildRenamed += this.DirectoryRenamed;
            Assets.Add(directoryItem);
            ///ReorderAssets(ParentAsset.Assets);
        }

        public void RemoveAsset()
        {
            throw new System.NotImplementedException();
        }

        public void Rename()
        {
            this.IsRenaming = true;
        }

        //directoryItem.ChildRenamed += this.DirectoryRenamed;
        //this.ChildRenamed += directoryItem.DirectoryRenamed;

        public event EventHandler ChildRenamed;
        protected virtual void OnChildRenamed(EventArgs e)
        {
            EventHandler handler = ChildRenamed;
            handler?.Invoke(this, e);
        }

        public void DirectoryRenamed(object sender, EventArgs e)
        {
            Console.WriteLine("Renamed " + this.Name);
        }
    }
}
