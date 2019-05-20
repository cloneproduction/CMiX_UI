using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(Content));
            Enable = true;
            BeatModifier = new BeatModifier(String.Format("{0}/{1}", layername, nameof(Content)), oscmessengers, masterbeat, mementor);
            Geometry = new Geometry(String.Format("{0}/{1}", layername, nameof(Content)), oscmessengers, mementor);
            Texture = new Texture(String.Format("{0}/{1}", layername, nameof(Content)), oscmessengers, mementor);
            PostFX = new PostFX(String.Format("{0}/{1}", layername, nameof(Content)), oscmessengers, mementor);
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set => SetAndNotify(ref _enable, value);
        }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public PostFX PostFX { get; }
        #endregion

        #region COPY/PASTE
        public void CopySelf()
        {
            ContentModel contentdto = new ContentModel();
            this.Copy(contentdto);
            IDataObject data = new DataObject();
            data.SetData("Content", contentdto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Content"))
            {
                var contentdto = (ContentModel)data.GetData("Content") as ContentModel;
                this.Paste(contentdto);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ContentModel contentdto = new ContentModel();
            this.Paste(contentdto);
        }


        public void Copy(ContentModel contentdto)
        {
            contentdto.Enable = Enable;
            BeatModifier.Copy(contentdto.BeatModifierModel);
            Texture.Copy(contentdto.TextureDTO);
            Geometry.Copy(contentdto.GeometryModel);
            PostFX.Copy(contentdto.PostFXModel);
        }

        public void Paste(ContentModel contentdto)
        {
            DisabledMessages();
            Enable = contentdto.Enable;
            BeatModifier.Paste(contentdto.BeatModifierModel);
            Texture.Paste(contentdto.TextureDTO);
            Geometry.Paste(contentdto.GeometryModel);
            PostFX.Paste(contentdto.PostFXModel);
            EnabledMessages();
        }
        #endregion
    }
}