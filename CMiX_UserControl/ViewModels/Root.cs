﻿using Ceras;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using Memento;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Studio.ViewModels
{
    public class Root : ViewModel
    {
        public Root()
        {
            Mementor = new Mementor();
            ComponentManager = new ComponentManager();
            CurrentProject = new Project();
            Projects = new ObservableCollection<Project>();
            Projects.Add(CurrentProject);
            ComponentEditor = new ComponentEditor();

            NewProjectCommand = new RelayCommand(p => NewProject());
            OpenProjectCommand = new RelayCommand(p => OpenProject());
            SaveProjectCommand = new RelayCommand(p => SaveProject());
            SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());
            QuitCommand = new RelayCommand(p => Quit(p));
        }

        public ICommand NewProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand SaveAsProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand QuitCommand { get; }

        public Mementor Mementor { get; set; }
        public CerasSerializer Serializer { get; set; }
        
        public ComponentEditor ComponentEditor { get; set; }

        public Assets Assets { get; set; }
        public string FolderPath { get; set; }
        public string MessageAddress { get; set; }

        public ObservableCollection<Project> Projects { get; set; }

        private Project _currentProject;
        public Project CurrentProject
        {
            get => _currentProject;
            set => SetAndNotify(ref _currentProject, value);
        }

        public ComponentManager ComponentManager { get; set; }

        #region MENU METHODS
        private void NewProject()
        {
            CurrentProject = new Project();
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
                    byte[] data = File.ReadAllBytes(folderPath);
                    ProjectModel projectmodel = serializer.Deserialize<ProjectModel>(data);
                    CurrentProject = new Project();
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
            System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
            savedialog.Filter = "Project (*.cmix)|*.cmix";
            savedialog.DefaultExt = "cmix";
            savedialog.AddExtension = true;

            if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectModel projectModel = CurrentProject.GetModel();
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
    }
}