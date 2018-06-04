using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        bool _Enable = true;
        public bool Enable
        {
            get => _Enable;
            set => this.SetAndNotify(ref _Enable, value);
        }

        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get => _BeatMultiplier;
            set => this.SetAndNotify(ref _BeatMultiplier, value);
        }

        double _BeatChanceToHit = 1.0;
        public double BeatChanceToHit
        {
            get => _BeatChanceToHit;
            set => this.SetAndNotify(ref _BeatChanceToHit, value);
        }

        Geometry _Geometry = new Geometry();
        public Geometry Geometry
        {
            get => _Geometry;
            set => this.SetAndNotify(ref _Geometry, value);
        }

        Texture _Texture = new Texture();
        public Texture Texture
        {
            get => _Texture;
            set => this.SetAndNotify(ref _Texture, value);
        }

        PostFX _PostFX = new PostFX();
        public PostFX PostFX
        {
            get => _PostFX;
            set => this.SetAndNotify(ref _PostFX, value);
        }
    }
}
