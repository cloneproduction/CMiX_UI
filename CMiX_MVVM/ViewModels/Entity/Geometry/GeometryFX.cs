namespace CMiX.MVVM.ViewModels
{
    public class GeometryFX : Sendable
    {
        public GeometryFX()
        {
            Explode = new Slider(nameof(Explode));
        }

        public Slider Explode { get; }
    }
}