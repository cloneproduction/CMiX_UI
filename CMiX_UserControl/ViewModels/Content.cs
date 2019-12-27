using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Commands;

namespace CMiX.ViewModels
{
    public class Content : SendableViewModel
    {
        #region CONSTRUCTORS
        public Content(Beat masterbeat, string messageAddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor) 
            : base (serverValidations, mementor)
        {
            MessageAddress = String.Format($"{messageAddress}{nameof(Content)}/");
            Enable = true;

            BeatModifier = new BeatModifier(MessageAddress, masterbeat, serverValidations, mementor);
            PostFX = new PostFX(MessageAddress, serverValidations, mementor);

            Objects = new ObservableCollection<Object>();
            CreateObject();

            AddObjectCommand = new RelayCommand(p => AddObject());
            DeleteObjectCommand = new RelayCommand(p => DeleteObject());
            CopyContentCommand = new RelayCommand(p => CopyContent());
            PasteContentCommand = new RelayCommand(p => PasteContent());
            ResetContentCommand = new RelayCommand(p => ResetContent());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageAddress)
        {
            MessageAddress = messageAddress;
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(BeatModifier)));
            PostFX.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(PostFX)));

            foreach (var obj in Objects)
            {
                obj.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Object)));
            }
        }

        public Object CreateObject()
        {
            ObjectID++;
            string messageAddress = $"{this.MessageAddress}Object{ObjectID.ToString()}/";
            Object obj = new Object(this.BeatModifier, messageAddress, ServerValidation, Mementor) { ID = ObjectID, Name = "Object" + ObjectID };
            Objects.Add(obj);
            SelectedObject = obj;

            return obj;
        }

        public void AddObject()
        {
            Mementor.BeginBatch();
            DisabledMessages();

            var obj = CreateObject();

            Mementor.ElementAdd(Objects, obj);
            //UpdateLayerContentFolder(layer);

            ObjectModel objectModel = new ObjectModel();
            obj.Copy(objectModel);

            EnabledMessages();
            Mementor.EndBatch();

            SendMessages(MessageAddress, MessageCommand.OBJECT_ADD, null, objectModel);
            Console.WriteLine("Object added with MessageAddress : " + MessageAddress);
        }

        public void DeleteObject()
        {
            if (SelectedObject != null)
            {
                Mementor.BeginBatch();
                DisabledMessages();

                Object removedObject = SelectedObject as Object;
                int removedObjectIndex = Objects.IndexOf(removedObject);
                //UpdateLayersIDOnDelete(removedlayer);
                Mementor.ElementRemove(Objects, removedObject);
                Objects.Remove(removedObject);

                if (Objects.Count == 0)
                {
                    ObjectID = -1;
                }

                if (Objects.Count > 0)
                {
                    if (removedObjectIndex > 0)
                        SelectedObject = Objects[removedObjectIndex - 1];
                    else
                        SelectedObject = Objects[0];
                }

                EnabledMessages();
                Mementor.EndBatch();

                SendMessages(MessageAddress, MessageCommand.OBJECT_DELETE, null, removedObjectIndex);
                Console.WriteLine("Deleted Object with index : " + removedObjectIndex.ToString());
            }
        }
        #endregion

        #region PROPERTIES
        public ICommand AddObjectCommand { get; }
        public ICommand DeleteObjectCommand { get; }
        public ICommand CopyContentCommand { get; }
        public ICommand PasteContentCommand { get; }
        public ICommand ResetContentCommand { get; }

        public ObservableCollection<Object> Objects { get; set; }

        private Object _selectedObject;
        public Object SelectedObject
        {
            get => _selectedObject;
            set
            {
                if(value == null)
                    Console.WriteLine("SelectedObject is NULL");
                else
                    Console.WriteLine("SelectedObject is not NULL");
                SetAndNotify(ref _selectedObject, value);
            }
        }

        private int _objectID = -1;
        public int ObjectID
        {
            get { return _objectID; }
            set { _objectID = value; }
        }

        public BeatModifier BeatModifier { get; }
        public PostFX PostFX { get; }

        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            this.DisabledMessages();

            this.Enable = true;
            this.BeatModifier.Reset();
            this.PostFX.Reset();

            this.EnabledMessages();
        }


        public void Copy(ContentModel contentModel)
        {
            contentModel.MessageAddress = MessageAddress;
            contentModel.Enable = Enable;

            this.SelectedObject.Copy(contentModel.SelectedObjectModel);
            this.BeatModifier.Copy(contentModel.BeatModifierModel);
            this.PostFX.Copy(contentModel.PostFXModel);
            

            foreach (Object obj in Objects)
            {
                ObjectModel objectModel = new ObjectModel();
                obj.Copy(objectModel);
                contentModel.ObjectModels.Add(objectModel);
            }
            Console.WriteLine("Copy Content");
        }

        public void Paste(ContentModel contentModel)
        {
            this.DisabledMessages();

            this.MessageAddress = contentModel.MessageAddress;
            this.Enable = contentModel.Enable;
            this.BeatModifier.Paste(contentModel.BeatModifierModel);
            this.PostFX.Paste(contentModel.PostFXModel);
            this.SelectedObject.Paste(contentModel.SelectedObjectModel);

            Objects.Clear();
            foreach (ObjectModel objectModel in contentModel.ObjectModels)
            {
                // may have some address problem here
                Object obj = new Object(this.BeatModifier, MessageAddress, ServerValidation, Mementor);
                obj.Paste(objectModel);
                this.Objects.Add(obj);
            }
            
            this.EnabledMessages();
        }

        #region COPYPASTE CONTENT
        public void CopyContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Copy(contentmodel);
            IDataObject data = new DataObject();
            data.SetData("ContentModel", contentmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteContent()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ContentModel"))
            {
                this.Mementor.BeginBatch();
                this.DisabledMessages();

                var contentmodel = data.GetData("ContentModel") as ContentModel;
                var contentmessageaddress = MessageAddress;
                this.Paste(contentmodel);
                this.UpdateMessageAddress(contentmessageaddress);

                //this.Copy(contentmodel);
                this.EnabledMessages();
                this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        public void ResetContent()
        {
            ContentModel contentmodel = new ContentModel();
            this.Reset();
            this.Copy(contentmodel);
            //SendMessages(nameof(ContentModel), contentmodel);
        }
        #endregion

        #endregion
    }
}