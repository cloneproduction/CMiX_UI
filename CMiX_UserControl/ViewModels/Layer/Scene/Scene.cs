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
        public Scene(int id, Beat beat, string messageAddress, MessageService messageService, Mementor mementor)
            : base(id, beat, messageAddress, messageService, mementor)
        {
            Name = "Scene";
            BeatModifier = new BeatModifier(MessageAddress, Beat, messageService, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);

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
        public override IComponentModel GetModel()
        {
            SceneModel sceneModel = new SceneModel();
            sceneModel.Enabled = Enabled;
            sceneModel.BeatModifierModel = BeatModifier.GetModel();
            sceneModel.PostFXModel = PostFX.GetModel();

            foreach (Component item in Components)
                sceneModel.ComponentModels.Add(item.GetModel());

            return sceneModel;
        }

        public override void SetViewModel(IComponentModel componentModel)
        {
            var sceneModel = componentModel as SceneModel;
            MessageService.Disable();

            Enabled = sceneModel.Enabled;
            BeatModifier.SetViewModel(sceneModel.BeatModifierModel);
            PostFX.SetViewModel(sceneModel.PostFXModel);

            Components.Clear();
            foreach (EntityModel entityModel in sceneModel.ComponentModels)
            {
                Entity entity = new Entity(0, this.Beat, this.MessageAddress, this.MessageService, this.Mementor);
                entity.SetViewModel(entityModel);
                this.AddComponent(entity);
            }

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            MessageService.Enable();
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
        //        MessageService.Disable();

        //        var contentModel = data.GetData("ContentModel") as SceneModel;
        //        var contentmessageaddress = MessageAddress;
        //        this.SetViewModel(contentModel);

        //        MessageService.Enable();
        //        this.Mementor.EndBatch();

        //        MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, contentModel);
        //    }
        //}

        //public void ResetContent()
        //{
        //    this.Reset();
        //    MessageService.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, GetModel());
        //}
        #endregion

        #endregion
    }
}