using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        double _FeedBack = 0.0;
        public double FeedBack
        {
            get => _FeedBack;
            set => this.SetAndNotify(ref _FeedBack, value);
        }

        double _Blur = 0.0;
        public double Blur
        {
            get => _Blur;
            set => this.SetAndNotify(ref _Blur, value);
        }

        PostFXTransforms _Transforms;
        public PostFXTransforms Transforms
        {
            get => _Transforms;
            set => this.SetAndNotify(ref _Transforms, value);
        }

        PostFXView _View;
        public PostFXView View
        {
            get => _View;
            set => this.SetAndNotify(ref _View, value);
        }
    }
}
