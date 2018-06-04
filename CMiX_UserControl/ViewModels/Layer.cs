using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class Layer : ViewModel
    {
        string _Name = "";
        public string Name
        {
            get => _Name;
            set => this.SetAndNotify(ref _Name, value);
        }

        double _Fade = 0.0;
        public double Fade
        {
            get => _Fade;
            set => this.SetAndNotify(ref _Fade, value);
        }

        BlendMode _BlendMode;
        public BlendMode BlendMode
        {
            get => _BlendMode;
            set => this.SetAndNotify(ref _BlendMode, value);
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

        Content _Content = new Content();
        public Content Content
        {
            get => _Content;
            set => this.SetAndNotify(ref _Content, value);
        }

        Mask _Mask = new Mask();
        public Mask Mask
        {
            get => _Mask;
            set => this.SetAndNotify(ref _Mask, value);
        }

        Coloration _Coloration = new Coloration();
        public Coloration Coloration
        {
            get => _Coloration;
            set => this.SetAndNotify(ref _Coloration, value);
        }
    }
}
