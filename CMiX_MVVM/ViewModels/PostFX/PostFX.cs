using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class PostFX : Sender
    {
        public PostFX() 
        {
            Feedback = new Slider(nameof(Feedback));
            Feedback.SendChangeEvent += this.OnChildPropertyToSendChange;

            Blur = new Slider(nameof(Blur));
            Blur.SendChangeEvent += this.OnChildPropertyToSendChange;

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
            ResetPostFXCommand = new RelayCommand(p => Reset());
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (this.GetMessageAddress() == e.MessageAddress)
            {
                this.SetViewModel(e.Model as PostFXModel);
                Console.WriteLine("PostFX Updated");
            }
        }

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
                SetAndNotify(ref _transforms, value);
                OnSendChange(this.GetModel(), GetMessageAddress());
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                OnSendChange(this.GetModel(), GetMessageAddress());
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            Feedback.Reset();
            Blur.Reset();

            PostFXModel postfxmodel = this.GetModel();
        }

        public void CopyPostFX()
        {
            IDataObject data = new DataObject();
            data.SetData(this.GetType().Name, this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PastePostFX()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFXModel"))
            {
                var postFXModel = data.GetData("PostFXModel") as PostFXModel;
                this.SetViewModel(postFXModel);
            }
        }
        #endregion
    }
}