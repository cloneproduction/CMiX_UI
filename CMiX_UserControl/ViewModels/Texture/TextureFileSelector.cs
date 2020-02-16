using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.Studio.ViewModels
{
    public class TextureFileSelector : ViewModel, ISendable, IUndoable
    {
        public TextureFileSelector(string messageAddress, Assets assets, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(TextureFileSelector)}/"; ;
            Assets = assets;
            MessageService = messageService;
            Mementor = mementor;
        }

        private TextureItem _selectedTextureItem;
        public TextureItem SelectedTextureItem
        {
            get => _selectedTextureItem;
            set => SetAndNotify(ref _selectedTextureItem, value);
        }

        public Assets Assets { get; set; }
        public Mementor Mementor { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }


        public TextureFileModel GetModel()
        {
            TextureFileModel model = new TextureFileModel();
            model.Name = SelectedTextureItem.Name;
            model.Path = SelectedTextureItem.Path;
            return model;
        }

        public void SetViewModel(TextureFileModel model)
        {
            MessageService.Disable();
            SelectedTextureItem.Name = model.Name;
            SelectedTextureItem.Path = model.Path;
            MessageService.Enable();
        }
    }
}
