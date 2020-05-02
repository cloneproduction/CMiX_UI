using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Scene : Component
    {
        #region CONSTRUCTORS
        public Scene(int id, Beat beat)
            : base(id, beat)
        {
            Name = "Scene";
            BeatModifier = new BeatModifier(beat);
            PostFX = new PostFX();

            //CopyContentCommand = new RelayCommand(p => CopyContent());
            //PasteContentCommand = new RelayCommand(p => PasteContent());
            //ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }
        public ICommand ResetContentCommand { get; }


        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }
        #endregion

        #region COPY/PASTE

        public void Reset()
        {
            this.Enabled = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();
        }

        #region COPYPASTE CONTENT
        //public void CopyContent()
        //{
        //    SceneModel contentmodel = GetModel();
        //    IDataObject data = new DataObject();
        //    data.SetData("ContentModel", contentmodel, false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PasteContent()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("ContentModel"))
        //    {
        //        this.Mementor.BeginBatch();
        //        MessengerService.Disable();

        //        var contentModel = data.GetData("ContentModel") as SceneModel;
        //        var contentmessageaddress = MessageAddress;
        //        this.SetViewModel(contentModel);

        //        MessengerService.Enable();
        //        this.Mementor.EndBatch();

        //        MessengerService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        //    }
        //}

        //public void ResetContent()
        //{
        //    this.Reset();
        //    MessengerService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, GetModel());
        //}
        #endregion

        #endregion
    }
}