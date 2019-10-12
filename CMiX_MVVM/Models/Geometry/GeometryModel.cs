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
            Modifier = new ModifierModel();
        }

        public GeometryModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            FileSelector = new FileSelectorModel(messageaddress);
            Modifier = new ModifierModel(messageaddress);
        }

        public TransformModel Transform { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public ModifierModel Modifier { get; set; }
        public GeometryFXModel GeometryFX { get; set; }
    }
}