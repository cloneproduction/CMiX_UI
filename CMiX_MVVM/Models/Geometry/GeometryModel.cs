using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel
    {
        public GeometryModel()
        {
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