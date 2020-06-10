namespace CMiX.MVVM.ViewModels
{
    public class GeometryFX : ViewModel
    {
        public GeometryFX()
        {
            Explode = new Slider(nameof(Explode));
        }

        public Slider Explode { get; }
    }
}