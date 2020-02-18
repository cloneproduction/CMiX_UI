using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using Ceras;


namespace CMiX.Studio.ViewModels
{
    public class Project : ViewModel, IProject
    {
        public Project()
        {
            MessageAddress = $"{nameof(Project)}/";
            Name = MessageAddress;
            Assets = new Assets();
            Mementor = new Mementor();
            MessageService = new MessageService();
            Beat = new MasterBeat(MessageService);

            Servers = new ObservableCollection<Server>();

            ServerManager = new ServerManager();

            Components = new ObservableCollection<IComponent>();

            FolderPath = string.Empty;
            Serializer = new CerasSerializer();

            RenameCommand = new RelayCommand(p => Rename());
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p as IComponent));
        }

        #region PROPERTIES
        public ICommand RemoveComponentCommand { get; }
        public ICommand RenameCommand { get; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }
        public Beat Beat { get; set; }
        public MessageService MessageService { get; set; }

        public CerasSerializer Serializer { get; set; }
        public ServerManager ServerManager { get; set; }

        public ObservableCollection<Server> Servers { get; set; }
        public string FolderPath { get; set; }

        public ObservableCollection<IComponent> Components { get; set; }

        public int ID { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set => SetAndNotify(ref name, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }
        #endregion



        public ProjectModel GetModel()
        {
            ProjectModel projectModel = new ProjectModel();
            //projectModel.CompositionEditorModel = CompositionEditor.GetModel();
            return projectModel;
        }

        public void SetViewModel(ProjectModel projectModel)
        {
            //CompositionEditor.SetViewModel(projectModel.CompositionEditorModel);
        }

        public void Rename()
        {
            Console.WriteLine("RenameReached");
            IsRenaming = true;
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component as Composition);
            IsExpanded = true;
        }

        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component as Composition);
        }
    }
}