using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel
    {
        public GeometryModel()
        {
            //FileSelector = new FileSelectorModel();
            GeometryFXModel = new GeometryFXModel();
            TransformModel = new TransformModel();
            InstancerModel = new InstancerModel();
            AssetSelectorModel = new AssetSelectorModel();
        }

        //public FileSelectorModel FileSelectorModel { get; set; }
        public TransformModel TransformModel { get; set; }
        public InstancerModel InstancerModel { get; set; }
        public GeometryFXModel GeometryFXModel{ get; set; }
        public AssetSelectorModel AssetSelectorModel { get; set; }
    }
}