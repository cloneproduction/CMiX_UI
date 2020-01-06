using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Ceras;
using CMiX.MVVM.Commands;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class CompositionEditor : ViewModel, ICompositionEditor
    {
        public CompositionEditor(MessageService messageService, string messageAddress, Assets assets, Mementor mementor)
        {
            CompositionFactory = new CompositionFactory(messageService);
            Compositions = new ObservableCollection<Composition>();
            MessageAddress = messageAddress;
            MessageService = messageService;
            Sender = messageService.CreateSender();

            NewCompositionCommand = new RelayCommand(p => NewComposition());
            DeleteSelectedCompositionCommand = new RelayCommand(p => DeleteSelectedComposition());
            DuplicateSelectedCompositionCommand = new RelayCommand(p => DuplicateSelectedComposition());
            ImportCompoCommand = new RelayCommand(p => ImportCompo());
            ExportCompoCommand = new RelayCommand(p => ExportCompo());
            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
        }

        #region PROPERTIES
        public ICommand NewCompositionCommand { get; }
        public ICommand DeleteSelectedCompositionCommand { get; }
        public ICommand DuplicateSelectedCompositionCommand { get; }
        public ICommand ImportCompoCommand { get; }
        public ICommand ExportCompoCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand ReloadCompositionCommand { get; }

        public MessageService MessageService { get; set; }
        public Assets Assets { get; set; }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        public CompositionFactory CompositionFactory { get; set; }
        
        public CerasSerializer Serializer { get; set; }
        public ObservableCollection<Composition> Compositions { get; set; }

        private Composition _selectedComposition;
        public Composition SelectedComposition
        {
            get => _selectedComposition;
            set => SetAndNotify(ref _selectedComposition, value);
        }

        #endregion

        private void ReloadComposition(object sender)
        {
            CompositionModel compositionModel = new CompositionModel();
            this.CopyModel(compositionModel);
            Sender.SendMessages(MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, compositionModel);
        }

        #region NEW/DELETE/DUPLICATE COMPOSITION
        private void NewComposition()
        {
            CompositionFactory.CreateSelectedComposition(this);
            CompositionModel compositionModel = new CompositionModel();
            SelectedComposition.CopyModel(compositionModel);
            Sender.SendMessages(MessageAddress, MessageCommand.COMPOSITION_ADD, null, compositionModel);
        }

        private void DeleteSelectedComposition()
        {
            int deleteIndex = Compositions.IndexOf(SelectedComposition);
            Sender.SendMessages(MessageAddress, MessageCommand.COMPOSITION_DELETE, null, deleteIndex);
            CompositionFactory.DeleteComposition(this);
        }

        private void DuplicateSelectedComposition()
        {
            CompositionFactory.DuplicateComposition(this);
            CompositionModel compositionModel = new CompositionModel();
            SelectedComposition.CopyModel(compositionModel);
            Sender.SendMessages(MessageAddress, MessageCommand.COMPOSITION_DUPLICATE, null, compositionModel);
        }
        #endregion

        #region IMPORT/EXPORT
        private void ImportCompo()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Compo (*.compmix)|*.compmix";
            opendialog.DefaultExt = "compmix";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    byte[] data = File.ReadAllBytes(folderPath);
                    CompositionModel compositionmodel = Serializer.Deserialize<CompositionModel>(data);
                    CompositionFactory.CreateSelectedComposition(this);
                    SelectedComposition.PasteModel(compositionmodel);
                }
            }
        }

        private void ExportCompo()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Compo (*.compmix)|*.compmix";
            savedialog.DefaultExt = "compmix";
            savedialog.FileName = SelectedComposition.Name;
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CompositionModel compositionmodel = new CompositionModel();
                SelectedComposition.CopyModel(compositionmodel);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(compositionmodel);
                File.WriteAllBytes(folderPath, data);
            }
        }
        #endregion

        #region COPY/PASTE MODEL
        public void CopyModel(IModel model)
        {
            CompositionEditorModel compoEditorModel = model as CompositionEditorModel;
            foreach (var comp in Compositions)
            {
                CompositionModel compositionModel = new CompositionModel();
                comp.CopyModel(compositionModel);
                compoEditorModel.CompositionModels.Add(compositionModel);
            }
        }

        public void PasteModel(IModel model)
        {
            CompositionEditorModel compoEditorModel = model as CompositionEditorModel;
            Compositions.Clear();
            foreach (var compositionModel in compoEditorModel.CompositionModels)
            {
                var comp = CompositionFactory.CreateComposition(this);
                comp.PasteModel(compositionModel);
            }
        }
        #endregion
    }
}