using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using Ceras;
using System.Collections.ObjectModel;
using System;

namespace CMiX.Studio.ViewModels
{
    public class Project : ViewModel, IProject
    {
        public Project()
        {
            MessageAddress = $"{nameof(Project)}/";
            Assets = new Assets();
            Mementor = new Mementor();
            Servers = new ObservableCollection<Server>();

            CompositionEditor = new CompositionEditor(Servers, MessageAddress, Assets, Mementor);
            ServerManager = new ServerManager();

            FolderPath = string.Empty;
            Serializer = new CerasSerializer();

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));
        }

        #region PROPERTIES
        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand QuitCommand { get; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }
        public CerasSerializer Serializer { get; set; }
        public CompositionEditor CompositionEditor { get; set; }
        public ServerManager ServerManager { get; set; }

        public ObservableCollection<Server> Servers { get; set; }

        public string FolderPath { get; set; }
        #endregion

        #region MENU METHODS
        private void NewProject()
        {
            throw new NotImplementedException();
        }

        private void OpenProject()
        {
            System.Windows.Forms.OpenFileDialog opendialog = new System.Windows.Forms.OpenFileDialog();
            opendialog.Filter = "Project (*.cmix)|*.cmix";

            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = opendialog.FileName;
                if (opendialog.FileName.Trim() != string.Empty) // Check if you really have a file name 
                {
                    CerasSerializer serializer = new CerasSerializer();
                    byte[] data = File.ReadAllBytes(folderPath) ;
                    ProjectModel projectmodel = serializer.Deserialize<ProjectModel>(data);
                    SetViewModel(projectmodel);
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if(!string.IsNullOrEmpty(FolderPath))
            {
                var projectModel = GetModel();
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
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Project (*.cmix)|*.cmix";
            savedialog.DefaultExt = "cmix";
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectModel projectModel = GetModel();
                string folderPath = savedialog.FileName;
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

        #region COPY/PASTE
        public ProjectModel GetModel()
        {
            ProjectModel projectModel = new ProjectModel();
            projectModel.CompositionEditorModel = CompositionEditor.GetModel();
            return projectModel;
        }

        public void SetViewModel(ProjectModel projectModel)
        {
            CompositionEditor.SetViewModel(projectModel.CompositionEditorModel);
        }
        #endregion
    }
}