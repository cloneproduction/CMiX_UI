using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.Resources;
using System.Collections.Specialized;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public class Layer : ViewModel, ISendable, IUndoable, IComponent
    {
        #region CONSTRUCTORS
        public Layer(int id, Beat beat, string messageAddress, MessageService messageService, Assets assets, Mementor mementor) 
        {
            ID = id;
            Name = "Layer " + id;

            Enabled = false;
            MessageAddress =  $"{messageAddress}{nameof(Layer)}/{id}/";
            MessageService = messageService;
            Mementor = mementor;
            Assets = assets;
            Beat = beat;
            ID = id;

            Components = new ObservableCollection<IComponent>();
            MaskComponents = new ObservableCollection<IComponent>();

            //Components.CollectionChanged += Components_CollectionChanged;
            PostFX = new PostFX(MessageAddress, messageService, mementor);

            Mask = new Mask(beat, MessageAddress, messageService, mementor);
            BlendMode = new BlendMode(beat, MessageAddress, messageService, mementor);
            Fade = new Slider(MessageAddress + nameof(Fade), messageService, mementor);

            RenameCommand = new RelayCommand(p => Rename());
            RemoveComponentCommand = new RelayCommand(p => RemoveComponent(p as IComponent));
            AddLayerMaskCommand = new RelayCommand(p => AddLayerMask());
        }

        //private void Components_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action == NotifyCollectionChangedAction.Remove)
        //    {
        //        foreach (Entity entity in e.OldItems)
        //            entity.PropertyChanged -= EntityViewModelPropertyChanged;
        //    }
        //    else if (e.Action == NotifyCollectionChangedAction.Add)
        //    {
        //        foreach (Entity entity in e.NewItems)
        //            entity.PropertyChanged += EntityViewModelPropertyChanged;
        //    }
        //}

        //public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //if(Components.Any(c => ((Entity)c).IsMask == true))
        //    //    IsMask = true;
        //    //else
        //    //    IsMask = false;
        //    //DisplayEntity();
        //}

        public void AddLayerMask()
        {
            IsMask = true;
            System.Console.WriteLine("AddLayerMask");
        }

        public void DeleteLayerMask()
        {
            IsMask = false;
            MaskComponents.Clear();
            System.Console.WriteLine("DeleteLayerMask");
        }
        #endregion

        public ICommand AddLayerMaskCommand { get; set; }
        public ICommand RenameCommand { get;  }
        public ICommand RemoveComponentCommand { get; }

        public ObservableCollection<IComponent> Components { get; set; }

        public ObservableCollection<IComponent> ContentComponents { get; set; }
        public ObservableCollection<IComponent> MaskComponents { get; set; }

        private Entity _selectedEntity;
        public Entity SelectedEntity
        {
            get => _selectedEntity;
            set => SetAndNotify(ref _selectedEntity, value);
        }

        #region PROPERTIES
        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
        }

        private Visibility visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get => visibility;
            set => SetAndNotify(ref visibility, value);
        }

        private bool _maskChecked = false;
        public bool MaskChecked
        {
            get => _maskChecked;
            set
            {
                SetAndNotify(ref _maskChecked, value);
                if (value == true)
                    Components = MaskComponents;
            }
        }

        private bool _contentChecked = true;
        public bool ContentChecked
        {
            get => _contentChecked;
            set
            {
                SetAndNotify(ref _contentChecked, value);
                if (value == true)
                    Components = ContentComponents;
            }
        }

        private bool _isMask;
        public bool IsMask
        {
            get => _isMask;
            set => SetAndNotify(ref _isMask, value);
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

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            set => SetAndNotify(ref _isRenaming, value);
        }

        private int _id;
        public int ID
        {
            get => _id;
            set => SetAndNotify(ref _id, value);
        }

        private Beat _beat;
        public Beat Beat
        {
            get => _beat;
            set => SetAndNotify(ref _beat, value);
        }

        private bool _out;
        public bool Out
        {
            get => _out;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Out));
                SetAndNotify(ref _out, value);
                //if (Out)
                    //Sender.SendMessages(MessageAddress + nameof(Out), Out);
            }
        }

        private IComponent _selectedComponent;
        public IComponent SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public MessageService MessageService { get; set; }
        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public Assets Assets { get; set; }

        public Slider Fade { get; set; }
        public Mask Mask { get; set; }
        public PostFX PostFX { get; set; }
        public BlendMode BlendMode { get; set; }
        #endregion

        public void DisplayEntity()
        {
            foreach (Entity component in Components)
            {
                if (component.IsMask && MaskChecked)
                    component.Visibility = Visibility.Visible;
                else if (!component.IsMask && ContentChecked)
                    component.Visibility = Visibility.Visible;
                else
                    component.Visibility = Visibility.Collapsed;
            }
        }

        #region COPY/PASTE/RESET
        public LayerModel GetModel()
        {
            LayerModel layerModel = new LayerModel();
            layerModel.Name = Name;
            layerModel.ID = ID;
            layerModel.Out = Out;
            layerModel.Fade = Fade.GetModel();
            layerModel.BlendMode = BlendMode.GetModel();
            layerModel.MaskModel = Mask.GetModel();
            layerModel.PostFXModel = PostFX.GetModel();
            return layerModel;
        }

        public void SetViewModel(LayerModel layerModel)
        {
            MessageService.Disable();

            Name = layerModel.Name;
            Out = layerModel.Out;
            ID = layerModel.ID;
            Fade.SetViewModel(layerModel.Fade);
            BlendMode.SetViewModel(layerModel.BlendMode);
            Mask.SetViewModel(layerModel.MaskModel);
            PostFX.SetViewModel(layerModel.PostFXModel);

            MessageService.Enable();
        }

        public void Reset()
        {
            Enabled = true;

            BlendMode.Reset();
            Fade.Reset();
            Mask.Reset();
            PostFX.Reset();
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

        public void Rename()
        {
            IsRenaming = true;
        }
        #endregion
    }
}