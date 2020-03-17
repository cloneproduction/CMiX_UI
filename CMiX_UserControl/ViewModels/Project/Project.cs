using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;
using Ceras;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

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

        public CerasSerializer Serializer { get; set; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }
        public Beat Beat { get; set; }
        public MessageService MessageService { get; set; }

        public ServerManager ServerManager { get; set; }

        public ObservableCollection<Server> Servers { get; set; }

        public string FolderPath { get; set; }

        private ObservableCollection<IComponent> _components;
        public ObservableCollection<IComponent> Components
        {
            get => _components;
            set => SetAndNotify(ref _components, value);
        }

        public int ID { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set => SetAndNotify(ref name, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
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
            projectModel.ID = ID;
            projectModel.MessageAddress = MessageAddress;
            projectModel.Name = Name;
            projectModel.IsVisible = IsVisible;
            projectModel.AssetsModel = Assets.GetModel();

            foreach (IGetSet<CompositionModel> item in Components)
            {
                projectModel.ComponentModels.Add(item.GetModel());
            }

            return projectModel;
        }

        public void SetViewModel(ProjectModel projectModel)
        {
            ID = projectModel.ID;
            Name = projectModel.Name;
            IsVisible = projectModel.IsVisible;
            MessageAddress = projectModel.MessageAddress;
            Assets.SetViewModel(projectModel.AssetsModel);

            Components.Clear();
            foreach (CompositionModel componentModel in projectModel.ComponentModels)
            {
                Composition composition = new Composition(0, this.MessageAddress, this.Beat, new MessageService(), this.Assets, this.Mementor);
                composition.SetViewModel(componentModel);
                this.AddComponent(composition);
            }
        }

        public void Rename()
        {
            IsRenaming = true;
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            IsExpanded = true;
        }

        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component);
        }
    }
}