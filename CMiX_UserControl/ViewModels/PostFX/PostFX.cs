using System;
using System.Windows;
using System.Windows.Input;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class PostFX : ViewModel,  ISendable, IUndoable  //, ICopyPasteModel<PostFXModel>,
    {
        #region CONSTRUCTORS
        public PostFX(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(PostFX));
            MessageService = messageService;

            Feedback = new Slider(MessageAddress + nameof(Feedback), messageService, mementor);
            Blur = new Slider(MessageAddress + nameof(Blur), messageService, mementor);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
            ResetPostFXCommand = new RelayCommand(p => ResetPostFX());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyPostFXCommand { get; }
        public ICommand PastePostFXCommand { get; }
        public ICommand ResetPostFXCommand { get; }

        public Slider Feedback { get; }
        public Slider Blur { get; }

        private string _transforms;
        public string Transforms
        {
            get => _transforms;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "Transforms");
                SetAndNotify(ref _transforms, value);
                //SendMessages(MessageAddress + nameof(Transforms), Transforms);
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "View");
                SetAndNotify(ref _view, value);
                //SendMessages(MessageAddress + nameof(View), View);
            }
        }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public PostFXModel GetModel()
        {
            PostFXModel postFXModel = new PostFXModel();
            postFXModel.Feedback = Feedback.GetModel();
            postFXModel.Blur = Blur.GetModel();
            postFXModel.Transforms = Transforms;
            postFXModel.View = View;
            return postFXModel;
        }

        //public void CopyModel(PostFXModel postFXmodel)
        //{
        //    Feedback.CopyModel(postFXmodel.Feedback);
        //    Blur.CopyModel(postFXmodel.Blur);
        //    postFXmodel.Transforms = Transforms;
        //    postFXmodel.View = View;
        //}

        public void PasteModel(PostFXModel postFXmodel)
        {
            MessageService.Disable();

            Transforms = postFXmodel.Transforms;
            View = postFXmodel.View;

            Feedback.PasteModel(postFXmodel.Feedback);
            Blur.PasteModel(postFXmodel.Blur);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            Feedback.Reset();
            Blur.Reset();

            PostFXModel postfxmodel = GetModel();
            //this.SendMessages(nameof(PostFXModel), postfxmodel);

            MessageService.Enable();
        }

        public void CopyPostFX()
        {
            PostFXModel postfxmodel = GetModel();
            IDataObject data = new DataObject();
            data.SetData("PostFXModel", postfxmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PastePostFX()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFXModel"))
            {
                Mementor.BeginBatch();
                MessageService.Disable();

                var postFXModel = data.GetData("PostFXModel") as PostFXModel;
                this.PasteModel(postFXModel);

                Mementor.EndBatch();
                MessageService.Enable();
                //this.SendMessages(nameof(PostFXModel), postFXModel);
            }
        }

        public void ResetPostFX()
        {
            PostFXModel postFXModel = GetModel();
            this.Reset();
            //this.SendMessages(nameof(PostFXModel), postFXModel);
        }
        #endregion
    }
}