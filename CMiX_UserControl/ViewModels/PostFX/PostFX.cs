using System;
using System.Windows;
using System.Windows.Input;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class PostFX : ViewModel
    {
        #region CONSTRUCTORS
        public PostFX(string messageaddress) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(PostFX));
            //MessengerService = messengerService;

            Feedback = new Slider(MessageAddress + nameof(Feedback));
            Blur = new Slider(MessageAddress + nameof(Blur));

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
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "Transforms");
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
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "View");
                SetAndNotify(ref _view, value);
                //SendMessages(MessageAddress + nameof(View), View);
            }
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessengerService.Disable();

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            Feedback.Reset();
            Blur.Reset();

            PostFXModel postfxmodel = this.GetModel();
            //this.SendMessages(nameof(PostFXModel), postfxmodel);

            MessengerService.Enable();
        }

        public void CopyPostFX()
        {
            PostFXModel postfxmodel = this.GetModel();
            IDataObject data = new DataObject();
            data.SetData("PostFXModel", postfxmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PastePostFX()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFXModel"))
            {
                //Mementor.BeginBatch();

                var postFXModel = data.GetData("PostFXModel") as PostFXModel;
                this.SetViewModel(postFXModel);

                //Mementor.EndBatch();

                //this.SendMessages(nameof(PostFXModel), postFXModel);
            }
        }

        public void ResetPostFX()
        {
            PostFXModel postFXModel = this.GetModel();
            this.Reset();
            //this.SendMessages(nameof(PostFXModel), postFXModel);
        }
        #endregion
    }
}