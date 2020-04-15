using Ceras;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using Memento;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var frameworkDialogFactory = new CustomFrameworkDialogFactory();
            var customTypeLocator = new CustomTypeLocator();
            DialogService = new DialogService(frameworkDialogFactory, customTypeLocator);
            

            RootComponent = new Root(0, string.Empty, null, null, Mementor);
            RootDirectory = new AssetDirectory();

            ComponentsInEditing = new ObservableCollection<Component>();
            AssetManager = new AssetManager(DialogService, RootDirectory.Assets);

            ComponentManager = new ComponentManager(RootComponent.Components, ComponentsInEditing);
            ComponentEditor = new ComponentEditor(ComponentsInEditing);

            Outliner = new Outliner(RootComponent.Components);

            NewProjectCommand = new RelayCommand(p => NewProject(p as Component));
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));

            CloseWindowCommand = new RelayCommand(p => CloseWindow(p));
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(p));
            MaximizeWindowCommand = new RelayCommand(p => MaximizeWindow(p));
        }

        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand QuitCommand { get; }

        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }

        private readonly IDialogService DialogService;

        public Mementor Mementor { get; set; }
        public CerasSerializer Serializer { get; set; }
        public ComponentEditor ComponentEditor { get; set; }
        public ComponentManager ComponentManager { get; set; }
        public AssetManager AssetManager { get; set; }
        public Root RootComponent { get; set; }
        public AssetDirectory RootDirectory { get; set; }
        public Outliner Outliner { get; set; }

        public string FolderPath { get; set; }

        private ObservableCollection<Component> _componentsInEditing;
        public ObservableCollection<Component> ComponentsInEditing
        {
            get => _componentsInEditing;
            set => SetAndNotify(ref _componentsInEditing, value);
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

                bool? success = DialogService.ShowDialog(this, new ModalDialog());
                if (success == true)
                {
                    window.Close();
                }
            }
        }


        #region MENU METHODS
        private void NewProject(Component component)
        {
            RootComponent.Components.Clear();
            ComponentsInEditing.Clear();
            RootComponent.Components.Add(ComponentManager.CreateProject(component));
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
                    RootComponent.Components.Clear();
                    ComponentsInEditing.Clear();
                    var newProject = ComponentManager.CreateProject(RootComponent);

                    byte[] data = File.ReadAllBytes(folderPath);
                    newProject.SetViewModel(Serializer.Deserialize<ProjectModel>(data));
                    FolderPath = folderPath;
                }
            }
        }


        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(FolderPath) && RootComponent.Components.Count > 0)
            {
                var data = Serializer.Serialize(RootComponent.Components[0].GetModel());
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
                if (RootComponent.Components.Count > 0)
                {
                    var data = Serializer.Serialize(RootComponent.Components[0].GetModel());
                    string folderPath = settings.FileName;
                    File.WriteAllBytes(folderPath, data);
                    FolderPath = folderPath;
                }
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
