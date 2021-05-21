using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel : Model, IModel
    {
        public GeometryModel()
        {
            this.ID = Guid.NewGuid();
            GeometryFXModel = new GeometryFXModel();
            TransformModel = new TransformModel();
            InstancerModel = new InstancerModel();
            AssetPathSelectorModel = new AssetPathSelectorModel();
        }

        public TransformModel TransformModel { get; set; }
        public InstancerModel InstancerModel { get; set; }
        public GeometryFXModel GeometryFXModel{ get; set; }
        public AssetPathSelectorModel AssetPathSelectorModel { get; set; }
    }
}