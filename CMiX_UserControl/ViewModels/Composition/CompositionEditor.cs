using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Ceras;
using Memento;
using CMiX.MVVM.Commands;

namespace CMiX.Studio.ViewModels
{
    public class CompositionEditor : ViewModel, ICompositionEditor
    {
        public CompositionEditor(ObservableCollection<Composition> compositions, ObservableCollection<Server> servers, string messageAddress, Assets assets, Mementor mementor)
        {
            Mementor = mementor;
            CompositionManager = new CompositionManager(servers);

            Compositions = compositions;
            MessageAddress = messageAddress;
            MessageService = new MessageService();

            NewCompositionCommand = new RelayCommand(p => NewComposition());
            DeleteSelectedCompositionCommand = new RelayCommand(p => DeleteSelectedComposition());
            DuplicateSelectedCompositionCommand = new RelayCommand(p => DuplicateSelectedComposition());
            RenameSelectedCompositionCommand = new RelayCommand(p => RenameSelectedComposition());
            ImportCompoCommand = new RelayCommand(p => ImportCompo());
            ExportCompoCommand = new RelayCommand(p => ExportCompo());
            ReloadCompositionCommand = new RelayCommand(p => ReloadComposition(p));
        }

        #region PROPERTIES
        public ICommand NewCompositionCommand { get; }
        public ICommand DeleteSelectedCompositionCommand { get; }
        public ICommand DuplicateSelectedCompositionCommand { get; }
        public ICommand RenameSelectedCompositionCommand { get; }
        public ICommand ImportCompoCommand { get; }
        public ICommand ExportCompoCommand { get; }
        public ICommand SaveCompositionCommand { get; }
        public ICommand OpenCompositionCommand { get; }
        public ICommand ReloadCompositionCommand { get; }

        public MessageService MessageService { get; set; }
        public Assets Assets { get; set; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public CompositionManager CompositionManager { get; set; }
        public MessageValidationManager MessageValidationManager { get; set; }

        public CerasSerializer Serializer { get; set; }

        public ObservableCollection<Composition> Compositions { get; set; }

        private Composition _selectedComposition;
        public Composition SelectedComposition
        {
            get => _selectedComposition;
            set => SetAndNotify(ref _selectedComposition, value);
        }

        private bool _isRenamingSelected;
        public bool IsRenamingSelected
        {
            get => _isRenamingSelected;
            set => SetAndNotify(ref _isRenamingSelected, value);
        }
        #endregion

        #region NEW/DELETE/DUPLICATE COMPOSITION
        private void NewComposition()
        {
            CompositionManager.CreateSelectedComposition(this);
            CompositionModel compositionModel = SelectedComposition.GetModel();
            MessageService.SendMessages(SelectedComposition.MessageAddress, MessageCommand.COMPOSITION_ADD, null, GetModel());
        }

        private void DeleteSelectedComposition()
        {
            int deleteIndex = Compositions.IndexOf(SelectedComposition);
            CompositionManager.DeleteComposition(this);
            MessageService.SendMessages(MessageAddress, MessageCommand.COMPOSITION_DELETE, null, deleteIndex);
        }

        private void DuplicateSelectedComposition()
        {
            CompositionManager.DuplicateComposition(this);
            MessageService.SendMessages(SelectedComposition.MessageAddress, MessageCommand.COMPOSITION_DUPLICATE, null, SelectedComposition.GetModel());
        }

        private void RenameSelectedComposition()
        {
            if (SelectedComposition != null)
            {
                SelectedComposition.IsEditingName = true;
                IsRenamingSelected = true;
            }
        }
        #endregion

        private void ReloadComposition(object messageValidation)
        {
            MessageValidation mv = messageValidation as MessageValidation;
            mv.SendMessage(SelectedComposition.MessageAddress, MessageCommand.VIEWMODEL_UPDATE, null, GetModel());
        }

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
                    CompositionManager.CreateSelectedComposition(this);
                    SelectedComposition.SetViewModel(compositionmodel);
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
                CompositionModel compositionmodel = SelectedComposition.GetModel();
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(compositionmodel);
                File.WriteAllBytes(folderPath, data);
            }
        }
        #endregion

        #region COPY/PASTE MODEL
        public CompositionEditorModel GetModel()
        {
            CompositionEditorModel compositionEditorModel = new CompositionEditorModel();
            compositionEditorModel.SelectedCompositionModel = SelectedComposition.GetModel();

            foreach (var composition in Compositions)
            {
                var compositionModel = composition.GetModel();
                compositionEditorModel.CompositionModels.Add(compositionModel);
            }
            return compositionEditorModel;
        }

        public void SetViewModel(CompositionEditorModel compoEditorModel)
        {
            if (SelectedComposition != null)
                SelectedComposition.SetViewModel(compoEditorModel.SelectedCompositionModel);

            Compositions.Clear();
            foreach (var compositionModel in compoEditorModel.CompositionModels)
            {
                var comp = CompositionManager.CreateComposition(this);
                comp.SetViewModel(compositionModel);
            }
        }
        #endregion
    }
}