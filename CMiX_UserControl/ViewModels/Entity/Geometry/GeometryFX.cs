using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class GeometryFX : ViewModel
    {
        #region CONSTRUCTORS
        public GeometryFX()
        {
            Explode = new Slider(nameof(Explode));
        }
        #endregion

        #region PROPERTIES
        public Slider Explode { get; }
        #endregion

        #region COPY/PASTE/RESET

        public void Paste(GeometryFXModel geometryFXdto)
        {
            Explode.SetViewModel(geometryFXdto.Explode);
        }

        public void Reset()
        {
            Explode.Reset();
        }
        #endregion
    }
}