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
    public class PostFX : ViewModel, ICopyPasteModel<PostFXModel>, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public PostFX(string messageaddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(PostFX));
            Sender = sender;

            Feedback = new Slider(MessageAddress + nameof(Feedback), sender, mementor);
            Blur = new Slider(MessageAddress + nameof(Blur), sender, mementor);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
            ResetPostFXCommand = new RelayCommand(p => ResetPostFX());
        }
        #endregion

        //#region METHODS
        //public void UpdateMessageAddress(string messageaddress)
        //{
        //    MessageAddress = messageaddress;

        //    Feedback.UpdateMessageAddress($"{messageaddress}{nameof(Feedback)}/");
        //    Blur.UpdateMessageAddress($"{messageaddress}{nameof(Blur)}/");
        //}
        //#endregion

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
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyModel(PostFXModel postFXmodel)
        {
            Feedback.CopyModel(postFXmodel.Feedback);
            Blur.CopyModel(postFXmodel.Blur);
            postFXmodel.Transforms = Transforms;
            postFXmodel.View = View;
        }

        public void PasteModel(PostFXModel postFXmodel)
        {
            Sender.Disable();

            Transforms = postFXmodel.Transforms;
            View = postFXmodel.View;

            Feedback.PasteModel(postFXmodel.Feedback);
            Blur.PasteModel(postFXmodel.Blur);

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            Feedback.Reset();
            Blur.Reset();

            PostFXModel postfxmodel = new PostFXModel();
            this.CopyModel(postfxmodel);
            //this.SendMessages(nameof(PostFXModel), postfxmodel);

            Sender.Enable();
        }

        public void CopyPostFX()
        {
            PostFXModel postfxmodel = new PostFXModel();
            this.CopyModel(postfxmodel);
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
                Sender.Disable();

                var postfxmodel = data.GetData("PostFXModel") as PostFXModel;
                var postfxmessageaddress = MessageAddress;
                this.PasteModel(postfxmodel);
                //UpdateMessageAddress(postfxmessageaddress);
                this.CopyModel(postfxmodel);

                Mementor.EndBatch();
                Sender.Enable();
                //this.SendMessages(nameof(PostFXModel), postfxmodel);
            }
        }

        public void ResetPostFX()
        {
            PostFXModel postfxmodel = new PostFXModel();
            this.Reset();
            this.CopyModel(postfxmodel);
            //this.SendMessages(nameof(PostFXModel), postfxmodel);
        }
        #endregion
    }
}