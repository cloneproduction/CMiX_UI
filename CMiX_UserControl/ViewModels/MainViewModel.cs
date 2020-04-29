using Ceras;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.Services;
using CMiX.ViewModels;
using Memento;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Mementor = new Mementor();
            Serializer = new CerasSerializer();

            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            MessengerService = new MessengerService();

            CurrentProject = ComponentFactory.CreateProject(MessengerService, Mementor);

            Projects = new ObservableCollection<Component>();
            Projects.Add(CurrentProject);

            AssetManager = new AssetManager(CurrentProject);
            MessengerManager = new MessengerManager(CurrentProject);
            ComponentEditor = new ComponentEditor(CurrentProject);
            ComponentManager = new ComponentManager(Projects);
            ComponentManager.ComponentDeletedEvent += ComponentEditor.ComponentDeletedEvent;

            Outliner = new Outliner(Projects);

            CloseWindowCommand = new RelayCommand(p => CloseWindow(p));
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(p));
            MaximizeWindowCommand = new RelayCommand(p => MaximizeWindow(p));

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());

            UndoCommand = new RelayCommand(p => Undo());
            RedoCommand = new RelayCommand(p => Redo());

            AddCompositionCommand = new RelayCommand(p => AddComposition());
        }

        #region PROPERTIES
        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AddCompositionCommand { get; }

        private readonly IDialogService DialogService;

        
        public Mementor Mementor { get; set; }
        public CerasSerializer Serializer { get; set; }
        
        public Outliner Outliner { get; set; }
        public string FolderPath { get; set; }
        public ObservableCollection<Component> Projects { get; set; }


        public MessengerService MessengerService { get; set; }

        private MessengerManager _messengerManager;
        public MessengerManager MessengerManager
        {
            get => _messengerManager;
            set => SetAndNotify(ref _messengerManager, value);
        }

        private AssetManager assetManager;
        public AssetManager AssetManager
        {
            get => assetManager;
            set => SetAndNotify(ref assetManager, value);
        }

        private ComponentManager _componentManager;
        public ComponentManager ComponentManager
        {
            get => _componentManager;
            set => SetAndNotify(ref _componentManager, value);
        }

        private ComponentEditor _componentEditor;
        public ComponentEditor ComponentEditor
        {
            get => _componentEditor;
            set => SetAndNotify(ref _componentEditor, value);
        }

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set => SetAndNotify(ref _currentProject, value);
        }


        public void MaximizeWindow(object obj)
        {
            if (obj is Window)
            {
                var window = obj as Window;
                if (window.WindowState == WindowState.Normal)
                    window.WindowState = WindowState.Maximized;
                else
                    window.WindowState = WindowState.Normal;
            }
        }

        public void MinimizeWindow(object obj)
        {
            if (obj is Window)
                ((Window)obj).WindowState = WindowState.Minimized;
        }

        public void CloseWindow(object obj)
        {
            if (obj is Window)
            {
                var window = obj as Window;

                var modalDialog = new ModalDialog();
                bool? success = DialogService.ShowDialog(this, modalDialog);
                if (success == true)
                {
                    if (modalDialog.SaveProject)
                    {
                        var projectSaved = SaveAsProject();
                        if (projectSaved)
                            window.Close();
                    }
                    else
                        window.Close();
                }
            }
        }
        #endregion

        public void AddComposition()
        {
            var composition = ComponentFactory.CreateComposition(CurrentProject);
            CurrentProject.AddComponent(composition);
        }
        public void Undo()
        {
            System.Console.WriteLine("Mementor Undo");
            //Mementor.Undo();
        }

        public void Redo()
        {
            System.Console.WriteLine("Mementor Redo");
            //Mementor.Redo();
        }

        #region MENU METHODS
        private void NewProject()
        {
            Project project = ComponentFactory.CreateProject(MessengerService, Mementor);
            Projects.Clear();
            Projects.Add(project);
            CurrentProject = project;
            AssetManager = new AssetManager(project);
            MessengerManager = new MessengerManager(project);
            ComponentEditor = new ComponentEditor(project);
        }

        private void OpenProject()
        {
            OpenFileDialogSettings settings = new OpenFileDialogSettings();
            settings.Filter = "Project (*.cmix)|*.cmix";

            bool? success = DialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                string folderPath = settings.FileName;
                if (settings.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    CurrentProject.ComponentsInEditing.Clear();
                    Projects.Clear();

                    byte[] data = File.ReadAllBytes(folderPath);
                    NewProject();
                    CurrentProject.SetViewModel(Serializer.Deserialize<ProjectModel>(data));
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(FolderPath))
            {
                var data = Serializer.Serialize(CurrentProject.GetModel());
                File.WriteAllBytes(FolderPath, data);
            }
            else
            {
                SaveAsProject();
            }
        }

        private bool SaveAsProject()
        {
            SaveFileDialogSettings settings = new SaveFileDialogSettings();
            settings.Filter = "Project (*.cmix)|*.cmix";
            settings.DefaultExt = "cmix";
            settings.AddExtension = true;

            bool? success = DialogService.ShowSaveFileDialog(this, settings);
            if (success == true && CurrentProject != null)
            {
                var data = Serializer.Serialize(CurrentProject.GetModel());
                string folderPath = settings.FileName;
                File.WriteAllBytes(folderPath, data);
                FolderPath = folderPath;
                return true;
            }
            else
                return false;
        }

        private void Quit(object p)
        {
            var window = p as Window;
            window.Close();
        }
        #endregion
    }
}
