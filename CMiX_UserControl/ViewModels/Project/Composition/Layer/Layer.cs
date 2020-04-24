using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Layer : Component
    {
        #region CONSTRUCTORS
        public Layer(int id, Beat beat, MessageService messageService, Mementor mementor)
            : base(id, beat, messageService, mementor)
        {
            Scene scene = ComponentFactory.CreateScene(this);// new Scene(0, Beat, MessageAddress, MessageService, Mementor);
            Components.Add(scene);

            Mask mask = ComponentFactory.CreateMask(this);// new Mask(0, Beat, MessageAddress, MessageService, Mementor);
            Components.Add(mask);

            PostFX = new PostFX(MessageAddress, messageService, mementor);
            BlendMode = new BlendMode(beat, MessageAddress, messageService, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messageService, mementor);

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
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Out));
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