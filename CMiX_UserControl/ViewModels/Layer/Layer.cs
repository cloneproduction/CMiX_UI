using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Layer : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Layer(MasterBeat masterBeat, string messageAddress, int id,  Sender sender, Assets assets, Mementor mementor) 
        {
            Enabled = false;
            MessageAddress =  $"{messageAddress}{nameof(Layer)}/{id}/";
            Sender = sender;
            Mementor = mementor;
            Assets = assets;

            ID = id;
            Name = "Layer " + id;

            BlendMode = new BlendMode(masterBeat, MessageAddress, sender, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), sender, mementor);
            Content = new Content(masterBeat, MessageAddress, sender, mementor);
            Mask = new Mask(masterBeat, MessageAddress, sender, mementor);
            PostFX = new PostFX(MessageAddress, sender, mementor);
        }
        #endregion

        #region PROPERTIES
        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

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

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        public Slider Fade { get; }
        public Content Content { get; }
        public Mask Mask { get; }
        public PostFX PostFX { get; }
        public BlendMode BlendMode { get; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyModel(LayerModel layerModel)
        {
            layerModel.Name = Name;
            layerModel.ID = ID;
            layerModel.Out = Out;

            Fade.CopyModel(layerModel.Fade);
            BlendMode.CopyModel(layerModel.BlendMode);
            Content.CopyModel(layerModel.ContentModel);
            Mask.CopyModel(layerModel.MaskModel);
            PostFX.CopyModel(layerModel.PostFXModel);
        }

        public void PasteModel(LayerModel layerModel)
        {
            Sender.Disable();

            Name = layerModel.Name;
            Out = layerModel.Out;
            ID = layerModel.ID;

            Fade.PasteModel(layerModel.Fade);
            BlendMode.PasteModel(layerModel.BlendMode);
            Content.PasteModel(layerModel.ContentModel);
            Mask.PasteModel(layerModel.MaskModel);
            PostFX.PasteModel(layerModel.PostFXModel);

            Sender.Enable();
        }

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            Content.Reset();
            Mask.Reset();
            PostFX.Reset();
        }
        #endregion
    }
}