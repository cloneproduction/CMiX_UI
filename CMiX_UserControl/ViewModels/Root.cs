using Ceras;
using Memento;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;

namespace CMiX.Studio.ViewModels
{
    public class Root : ViewModel
    {
        public Root()
        {
            var frameworkDialogFactory = new CustomFrameworkDialogFactory();
            var customTypeLocator = new CustomTypeLocator();
            DialogService = new DialogService(frameworkDialogFactory, customTypeLocator);

            Mementor = new Mementor();
            Serializer = new CerasSerializer();

            CurrentProject = new Project(DialogService);
            CurrentProject.PropertyChanged += CurrentProject_PropertyChanged;
            Projects = new ObservableCollection<Component> { CurrentProject };
            ComponentEditor = new ComponentEditor();

            ComponentManager = new ComponentManager(Projects);
            ComponentManager.ComponentDeleted += ComponentEditor.ComponentManager_ComponentDeleted;

            Outliner = new Outliner(Projects);

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));

            CloseWindowCommand = new RelayCommand(p => CloseWindow(p));
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(p));
            MaximizeWindowCommand = new RelayCommand(p => MaximizeWindow(p));
        }

        public void OpenSplashScreen()
        {

        }

        public void MaximizeWindow(object obj)
        {
            if(obj is Window)
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
            if(obj is Window)
            {
                var window = obj as Window;

                bool? success = DialogService.ShowDialog(this, new ModalDialog());
                if (success == true)
                {
                    window.Close();
                }
            }
        }

        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }

        private readonly IDialogService DialogService;

        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand QuitCommand { get; }

        public Mementor Mementor { get; set; }
        public CerasSerializer Serializer { get; set; }

        public Outliner Outliner { get; set; }

        public ComponentEditor ComponentEditor { get; set; }
        public ComponentManager ComponentManager { get; set; }
        public AssetManager AssetManager { get; set; }

        public string FolderPath { get; set; }
        public string MessageAddress { get; set; }

        private ObservableCollection<Component> _projects;
        public ObservableCollection<Component> Projects
        {
            get => _projects;
            set => SetAndNotify(ref _projects, value);
        }

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set => SetAndNotify(ref _currentProject, value);
        }

        private bool _projectChanged;
        public bool ProjectChanged
        {
            get { return _projectChanged; }
            set { _projectChanged = value; }
        }

        private void CurrentProject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            System.Console.WriteLine("CURRENT PROJECT CHANGED");
        }

        #region MENU METHODS
        private void NewProject()
        {
            CurrentProject = new Project(this.DialogService);
            CurrentProject.PropertyChanged += CurrentProject_PropertyChanged;
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
                    byte[] data = File.ReadAllBytes(folderPath);
                    ProjectModel projectmodel = Serializer.Deserialize<ProjectModel>(data);
                    CurrentProject.SetViewModel(projectmodel);
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(FolderPath))
            {
                var projectModel = CurrentProject.GetModel();
                var data = Serializer.Serialize(projectModel);
                File.WriteAllBytes(FolderPath, data);
            }
            else
            {
                SaveAsProject();
            }
        }

        private void SaveAsProject()
        {
            SaveFileDialogSettings settings = new SaveFileDialogSettings();
            settings.Filter = "Project (*.cmix)|*.cmix";
            settings.DefaultExt = "cmix";
            settings.AddExtension = true;

            bool? success = DialogService.ShowSaveFileDialog(this, settings);
            if (success == true)
            {
                IComponentModel projectModel = CurrentProject.GetModel();
                string folderPath = settings.FileName;
                var data = Serializer.Serialize(projectModel);
                File.WriteAllBytes(folderPath, data);
                FolderPath = folderPath;
            }
        }

        private void Quit(object p)
        {
            var window = p as Window;
            window.Close();
        }
        #endregion
    }
}