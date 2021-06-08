using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    public class InverterModel : Model
    {
        public InverterModel()
        {
            ID = Guid.NewGuid();
            Invert = new SliderModel();
            InvertMode = new ComboBoxModel<TextureInvertMode>();
        }

        public SliderModel Invert { get; set; }
        public ComboBoxModel<TextureInvertMode> InvertMode { get; set; }
    }
}
