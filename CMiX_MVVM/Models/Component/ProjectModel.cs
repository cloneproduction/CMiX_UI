using CMiX.Core.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public class ProjectModel : IComponentModel
    {
        public ProjectModel(Guid id)
        {
            ID = id;
            ComponentModels = new ObservableCollection<IComponentModel>();
            AssetModels = new ObservableCollection<IAssetModel>();
            AssetModelsFlatten = new ObservableCollection<IAssetModel>();
        }

        public bool Enabled { get; set; }
        public string Address { get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }

        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
        public ObservableCollection<IAssetModel> AssetModels { get; set; }
        public ObservableCollection<IAssetModel> AssetModelsFlatten { get; set; }
    }
}