using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Content : ViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) 
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Content));

            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, oscmessengers, masterbeat, mementor);
            Geometry = new Geometry(MessageAddress, oscmessengers, mementor);
            Texture = new Texture(MessageAddress, oscmessengers, mementor);
            PostFX = new PostFX(MessageAddress, oscmessengers, mementor);

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
            Copy(contentmodel);
            IDataObject data = new DataObject();
            data.SetData(nameof(Content), contentmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(Content)))
            {
                var contentmodel = (ContentModel)data.GetData(nameof(Content)) as ContentModel;
                Paste(contentmodel);
                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            ContentModel contentmodel = new ContentModel();
            Paste(contentmodel);
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