using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class Mask : ViewModel, ISendable, IUndoable, IComponent, IGetSet<MaskModel>
    {
        #region CONSTRUCTORS
        public Mask(Beat beat, string messageAddress, MessageService messageService, Assets assets, Mementor mementor) 
        {
            Beat = beat;
            Assets = assets;
            Mementor = mementor;
            MessageAddress = $"{messageAddress}{nameof(Mask)}/";
            MessageService = messageService;
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;
            Name = "Mask";
            Components = new ObservableCollection<IComponent>();
            BeatModifier = new BeatModifier(MessageAddress, beat, messageService, mementor);

            Geometry = new Geometry(MessageAddress, messageService, mementor, assets, beat);
            Texture = new Texture(MessageAddress, messageService, assets, mementor);
            PostFX = new PostFX(MessageAddress, messageService, mementor);

            CopyMaskCommand = new RelayCommand(p => CopyMask());
            PasteMaskCommand = new RelayCommand(p => PasteMask());
            ResetMaskCommand = new RelayCommand(p => ResetMask());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyMaskCommand { get; }
        public ICommand PasteMaskCommand { get; }
        public ICommand ResetMaskCommand { get; }

        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public PostFX PostFX { get; }

        public string MessageAddress { get; set; }
        public Mementor Mementor { get; set; }
        public MasterBeat MasterBeat { get; set; }
        public Assets Assets { get; set; }
        public Beat Beat { get; set; }
        public MessageService MessageService { get; set; }
        public bool IsRenaming { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }


        private string _masktype;
        public string MaskType
        {
            get => _masktype;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(MaskType));
                SetAndNotify(ref _masktype, value);
                //SendMessages(MessageAddress + nameof(MaskType), MaskType);
            }
        }

        private string _maskcontroltype;
        public string MaskControlType
        {
            get => _maskcontroltype;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(MaskControlType));
                SetAndNotify(ref _maskcontroltype, value);
                //SendMessages(MessageAddress + nameof(MaskControlType), MaskControlType);
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetAndNotify(ref _isVisible, value);
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

        public ICommand RenameCommand { get; }

        public ObservableCollection<IComponent> Components { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public MaskModel GetModel()
        {
            MaskModel maskModel = new MaskModel();

            maskModel.Enable = Enabled;
            maskModel.MaskType = MaskType;
            maskModel.MaskControlType = MaskControlType;

            maskModel.BeatModifierModel = BeatModifier.GetModel();
            maskModel.TextureModel = Texture.GetModel();
            maskModel.GeometryModel = Geometry.GetModel();
            maskModel.PostFXModel = PostFX.GetModel();

            foreach (IGetSet<EntityModel> item in Components)
                maskModel.ComponentModels.Add(item.GetModel());

            return maskModel;
        }

        public void SetViewModel(MaskModel maskModel)
        {
            MessageService.Disable();

            Enabled = maskModel.Enable;
            MaskType = maskModel.MaskType;
            MaskControlType = maskModel.MaskControlType;

            BeatModifier.SetViewModel(maskModel.BeatModifierModel);
            Texture.SetViewModel(maskModel.TextureModel);
            Geometry.Paste(maskModel.GeometryModel);
            PostFX.SetViewModel(maskModel.PostFXModel);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            Enabled = false;
            BeatModifier.Reset();
            Geometry.Reset();
            Texture.Reset();
            PostFX.Reset();

            MessageService.Enable();
        }

        public void CopyMask()
        {
            IDataObject data = new DataObject();
            data.SetData("MaskModel", GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteMask()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("MaskModel"))
            {
                Mementor.BeginBatch();
                MessageService.Disable();;

                var maskmodel = data.GetData("MaskModel") as MaskModel;
                this.SetViewModel(maskmodel);
                MessageService.Enable();
                Mementor.EndBatch();
                //this.SendMessages(nameof(MaskModel), GetModel());
            }
        }

        public void ResetMask()
        {
            this.Reset();
            //this.SendMessages(nameof(MaskModel), GetModel());
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
        #endregion
    }
}