using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel : Model
    {
        public GeometryModel()
        {
            FileSelector = new FileSelectorModel();

            Counter = new CounterModel();
            GeometryFX = new GeometryFXModel();
            Transform = new TransformModel();
        }

        public GeometryModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            FileSelector = new FileSelectorModel(messageaddress);
        }

        public TransformModel Transform { get; set; }
        public FileSelectorModel FileSelector { get; set; }
        public CounterModel Counter { get; set; }

        public GeometryFXModel GeometryFX { get; set; }
    }
}