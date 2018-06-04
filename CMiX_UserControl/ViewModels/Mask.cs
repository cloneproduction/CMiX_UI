namespace CMiX.ViewModels
{
    public class Mask : ViewModel
    {
        bool _Enable = true;
        public bool Enable
        {
            get => _Enable;
            set => SetAndNotify(ref _Enable, value);
        }

        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get => _BeatMultiplier;
            set => SetAndNotify(ref _BeatMultiplier, value);
        }

        double _BeatChanceToHit = 1.0;
        public double BeatChanceToHit
        {
            get => _BeatChanceToHit;
            set => SetAndNotify(ref _BeatChanceToHit, value);
        }

        Geometry _Geometry = new Geometry();
        public Geometry Geometry
        {
            get => _Geometry;
            set => SetAndNotify(ref _Geometry, value);
        }

        Texture _Texture = new Texture();
        public Texture Texture
        {
            get => _Texture;
            set => SetAndNotify(ref _Texture, value);
        }

        PostFX _PostFX = new PostFX();
        public PostFX PostFX
        {
            get => _PostFX;
            set => SetAndNotify(ref _PostFX, value);
        }
    }
}
