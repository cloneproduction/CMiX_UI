

using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Layer : Component
    {
        public Layer(int id, Beat beat) : base(id, beat)
        {
            //Scene scene = ComponentFactory.CreateScene(this);
            //AddComponent(scene);
            //System.Console.WriteLine("Layer Constructor CreateScene");
            //Mask mask = ComponentFactory.CreateMask(this);
            //AddComponent(mask);

            PostFX = new PostFX();
            BlendMode = new BlendMode(this);
            Fade = new Slider(nameof(Fade), this);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as LayerModel);
                Console.WriteLine("Layer SetViewModel");
            }
                
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set => SetAndNotify(ref _out, value);
        }

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
    }
}