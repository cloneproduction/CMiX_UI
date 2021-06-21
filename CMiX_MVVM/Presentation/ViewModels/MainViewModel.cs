﻿using Ceras;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels.Assets;
using CMiX.Core.Presentation.ViewModels.Components;
using Memento;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());
            Mementor = new Mementor();
            DataSender = new DataSender();

            Guid g = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
            var model = new ProjectModel(g);

            CurrentProject = new Project(model);



            var componentCommunicator = new DataSenderCommunicator(DataSender);
            //componentCommunicator.MessageSender = DataSender;
            CurrentProject.SetCommunicator(componentCommunicator);

            //CurrentProject.SetSender(DataSender);
            AssetManager = new AssetManager(CurrentProject);


            Projects = new ObservableCollection<Component>();
            Projects.Add(CurrentProject);

            ComponentManager = new ComponentManager(Projects);
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
            AnimatedDouble = new ObservableCollection<double>();
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



        private ObservableCollection<double> _animatedDouble;
        public ObservableCollection<double> AnimatedDouble
        {
            get => _animatedDouble;
            set => SetAndNotify(ref _animatedDouble, value);
        }


        private DataSender _dataSender;
        public DataSender DataSender
        {
            get => _dataSender;
            set => SetAndNotify(ref _dataSender, value);
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
            //ComponentFactory.CreateComposition(CurrentProject);
        }
        public void Undo()
        {
            //Mementor.Undo();
        }

        public void Redo()
        {
            //Mementor.Redo();
        }


        private void NewProject()
        {
            Project project = new Project(new ProjectModel(Guid.Empty));
            Projects.Clear();
            Projects.Add(project);
            CurrentProject = project;
            AssetManager = new AssetManager(project);
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
                    //CurrentProject.ComponentsInEditing.Clear();
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
    }
}