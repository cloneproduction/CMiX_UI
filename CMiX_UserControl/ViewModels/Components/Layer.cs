

namespace CMiX.Studio.ViewModels
{
    public class Layer : Component
    {
        #region CONSTRUCTORS
        public Layer(int id, Beat beat)
            : base(id, beat)
        {
            Scene scene = ComponentFactory.CreateScene(this);
            Components.Add(scene);

            Mask mask = ComponentFactory.CreateMask(this);
            Components.Add(mask);

            PostFX = new PostFX();
            PostFX.SendChangeEvent += this.OnChildPropertyToSendChange;

            BlendMode = new BlendMode(beat);
            BlendMode.SendChangeEvent += this.OnChildPropertyToSendChange;

            Fade = new Slider(nameof(Fade));
            Fade.SendChangeEvent += this.OnChildPropertyToSendChange;

            IsExpanded = true;
        }
        #endregion

        #region PROPERTIES
        private bool _out;
        public bool Out
        {
            get => _out;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, nameof(Out));
                SetAndNotify(ref _out, value);
                //if (Out)
                    //Sender.SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
        #endregion

        #region COPY/PASTE/RESET

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            //Mask.Reset();
            PostFX.Reset();
        }
        #endregion
    }
}