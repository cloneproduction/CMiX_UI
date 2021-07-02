// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Assets;
using CMiX.Core.Presentation.ViewModels.Components;
using CMiX.Core.Presentation.ViewModels.Network;
using CMiX.Core.Presentation.ViewModels.Scheduling;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel(IDataSender dataSender, IMediator mediator)
        {
            DialogService = new DialogService(new CustomFrameworkDialogFactory(), new CustomTypeLocator());

            DataSender = dataSender as DataSender;

            var model = new ProjectModel();
            CurrentProject = new Project(model, mediator);

            var componentCommunicator = new DataSenderCommunicator(DataSender as DataSender);
            CurrentProject.SetCommunicator(componentCommunicator);

            AssetManager = new AssetManager(CurrentProject);

            SchedulerManager = new SchedulerManager(CurrentProject);
            //SchedulerManager.SetCommunicator(componentCommunicator);
            //PlaylistEditor = new PlaylistEditor(CurrentProject);



            Projects = new ObservableCollection<Component>();
            Projects.Add(CurrentProject);

            ComponentManager = new ComponentManager(Projects);
            Outliner = new Outliner(Projects);

            AnimatedDouble = new ObservableCollection<double>();

            CloseWindowCommand = new RelayCommand(p => CloseWindow(p));
            //MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(p));
            //MaximizeWindowCommand = new RelayCommand(p => MaximizeWindow(p));

            //NewProjectCommand = new RelayCommand(p => NewProject());
            //OpenProjectCommand = new RelayCommand(p => OpenProject());
            //SaveProjectCommand = new RelayCommand(p => SaveProject());
            //SaveAsProjectCommand = new RelayCommand(p => SaveAsProject());

            //UndoCommand = new RelayCommand(p => Undo());
            //RedoCommand = new RelayCommand(p => Redo());

            //AddCompositionCommand = new RelayCommand(p => AddComposition());

        }



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

        //public Mementor Mementor { get; set; }
        //public CerasSerializer Serializer { get; set; }



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

        private SchedulerManager _schedulerManager;
        public SchedulerManager SchedulerManager
        {
            get => _schedulerManager;
            set => SetAndNotify(ref _schedulerManager, value);
        }

        public PlaylistEditor PlaylistEditor { get; set; }


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
                        //var projectSaved = SaveAsProject();
                        //if (projectSaved)
                        //    window.Close();
                    }
                    else
                        window.Close();
                }
            }
        }

        private void Quit(object p)
        {
            var window = p as Window;
            window.Close();
        }

        //public void AddComposition()
        //{
        //    //ComponentFactory.CreateComposition(CurrentProject);
        //}
        //public void Undo()
        //{
        //    //Mementor.Undo();
        //}

        //public void Redo()
        //{
        //    //Mementor.Redo();
        //}


        //private void NewProject()
        //{
        //    Project project = new Project(new ProjectModel(), Mediator);
        //    Projects.Clear();
        //    Projects.Add(project);
        //    CurrentProject = project;
        //    AssetManager = new AssetManager(project);
        //}

        //private void OpenProject()
        //{
        //    OpenFileDialogSettings settings = new OpenFileDialogSettings();
        //    settings.Filter = "Project (*.cmix)|*.cmix";

        //    bool? success = DialogService.ShowOpenFileDialog(this, settings);
        //    if (success == true)
        //    {
        //        string folderPath = settings.FileName;
        //        if (settings.FileName.Trim() != string.Empty) // Check if you really have a file name 
        //        {
        //            //CurrentProject.ComponentsInEditing.Clear();
        //            Projects.Clear();

        //            byte[] data = File.ReadAllBytes(folderPath);
        //            NewProject();
        //            CurrentProject.SetViewModel(Serializer.Deserialize<ProjectModel>(data));
        //            FolderPath = folderPath;
        //        }
        //    }
        //}

        //private void SaveProject()
        //{
        //    //System.Windows.Forms.SaveFileDialog savedialog = new System.Windows.Forms.SaveFileDialog();
        //    //if (!string.IsNullOrEmpty(FolderPath))
        //    //{
        //    //    var data = Serializer.Serialize(CurrentProject.GetModel());
        //    //    File.WriteAllBytes(FolderPath, data);
        //    //}
        //    //else
        //    //{
        //    //    SaveAsProject();
        //    //}
        //}

        //private bool SaveAsProject()
        //{
        //    SaveFileDialogSettings settings = new SaveFileDialogSettings();
        //    settings.Filter = "Project (*.cmix)|*.cmix";
        //    settings.DefaultExt = "cmix";
        //    settings.AddExtension = true;

        //    bool? success = DialogService.ShowSaveFileDialog(this, settings);
        //    if (success == true && CurrentProject != null)
        //    {
        //        var data = Serializer.Serialize(CurrentProject.GetModel());
        //        string folderPath = settings.FileName;
        //        File.WriteAllBytes(folderPath, data);
        //        FolderPath = folderPath;
        //        return true;
        //    }
        //    else
        //        return false;
        //}


    }
}
