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
using CMiX.Studio.Views;
using MvvmDialogs.FrameworkDialogs.MessageBox;
using MvvmDialogs.FrameworkDialogs;
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
            Projects = new ObservableCollection<IComponent> { CurrentProject };
            ComponentManager = new ComponentManager(Projects);

            ComponentEditor = new ComponentEditor();
            ComponentManager.ComponentDeleted += ComponentEditor.ComponentManager_ComponentDeleted;

            Outliner = new Outliner(Projects);

            ShowDialogCommand = new RelayCommand(p => ShowDialog());

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));
        }

        public ICommand ShowDialogCommand { get; }

        private readonly IDialogService DialogService;

        //public Root(IDialogService dialogService)
        //{
        //    this.dialogService = dialogService;
        //}


        private void ShowDialog()
        {
            System.Console.WriteLine("ShowDialog");

            //dialogService.Show(this, new CustomMessageBox());

            OpenFileDialogSettings fileDialogSettings = new OpenFileDialogSettings();
         
            DialogService.ShowOpenFileDialog(this, fileDialogSettings);
        }


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

        public Assets Assets { get; set; }
        public string FolderPath { get; set; }
        public string MessageAddress { get; set; }

        private ObservableCollection<IComponent> _projects;
        public ObservableCollection<IComponent> Projects
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

        #region MENU METHODS
        private void NewProject()
        {
            CurrentProject = new Project(this.DialogService);
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