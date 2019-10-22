using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel : Model
    {
        public GeometryModel()
        {
            FileSelector = new FileSelectorModel();
            GeometryFX = new GeometryFXModel();
            Transform = new TransformModel();
            Instancer = new InstancerModel();
        }

        public GeometryModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            FileSelector = new FileSelectorModel(messageaddress);
            Instancer = new InstancerModel(messageaddress);
        }

        public TransformModel Transform { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public InstancerModel Instancer { get; set; }
        public GeometryFXModel GeometryFX { get; set; }
    }
}