﻿using System.IO;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using Ceras;

namespace CMiX.Studio.ViewModels
{
    public class Project : ViewModel, IProject
    {
        public Project(MessageService messageService, Mementor mementor)
        {
            MessageService = messageService;
            MessageAddress = $"{nameof(Project)}/";
            Assets = new Assets();
            Mementor = mementor;

            CompositionEditor = new CompositionEditor(messageService, MessageAddress, Assets, Mementor);

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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }
        public CerasSerializer Serializer { get; set; }
        public CompositionEditor CompositionEditor { get; set; }

        public string FolderPath { get; set; }
        #endregion

        #region MENU METHODS
        private void NewProject()
        {
            ProjectModel projectModel = new ProjectModel();
            this.CopyModel(projectModel);
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
                    PasteModel(projectmodel);
                    FolderPath = folderPath;
                }
            }
        }

        private void SaveProject()
        {
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            if(!string.IsNullOrEmpty(FolderPath))
            {
                ProjectModel projectdto = new ProjectModel();
                this.CopyModel(projectdto);
                var serializer = new CerasSerializer();
                var data = serializer.Serialize(projectdto);
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
                ProjectModel projectdto = new ProjectModel();
                this.CopyModel(projectdto);
                string folderPath = savedialog.FileName;
                var data = Serializer.Serialize(projectdto);
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
        public void CopyModel(IModel model)
        {
            ProjectModel projectModel = model as ProjectModel;
            CompositionEditor.CopyModel(projectModel.CompositionEditorModel);
        }

        public void PasteModel(IModel model)
        {
            var projectModel = model as ProjectModel;
            CompositionEditor.PasteModel(projectModel.CompositionEditorModel);
        }
        #endregion
    }
}