namespace CMiX.MVVM.ViewModels
{
    public class GeometryFX : ViewModel
    {
        public GeometryFX()
        {
            //Explode = new Slider(nameof(Explode), this);
        }

        public Slider Explode { get; set; }
    }
}