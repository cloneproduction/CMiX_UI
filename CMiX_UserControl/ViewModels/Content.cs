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
            ContentModel contentmodel = new ContentModel();
            this.Copy(contentmodel);
            IDataObject data = new DataObject();
            data.SetData("Content", contentmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Content"))
            {
                var contentmodel = (ContentModel)data.GetData("Content") as ContentModel;
                this.Paste(contentmodel);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ContentModel contentmodel = new ContentModel();
            this.Paste(contentmodel);
        }


        public void Copy(ContentModel contentmodel)
        {
            contentmodel.MessageAddress = MessageAddress;
            contentmodel.Enable = Enable;
            BeatModifier.Copy(contentmodel.BeatModifierModel);
            Texture.Copy(contentmodel.texturemodel);
            Geometry.Copy(contentmodel.GeometryModel);
            PostFX.Copy(contentmodel.PostFXModel);
        }

        public void Paste(ContentModel contentmodel)
        {
            DisabledMessages();
            MessageAddress = contentmodel.MessageAddress;
            Enable = contentmodel.Enable;
            BeatModifier.Paste(contentmodel.BeatModifierModel);
            Texture.Paste(contentmodel.texturemodel);
            Geometry.Paste(contentmodel.GeometryModel);
            PostFX.Paste(contentmodel.PostFXModel);
            EnabledMessages();
        }
        #endregion
    }
}