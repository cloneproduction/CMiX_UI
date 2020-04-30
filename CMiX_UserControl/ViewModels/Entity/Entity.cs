using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class Entity : Component
    {
        #region CONSTRUCTORS
        public Entity(int id, Beat beat)
            : base(id, beat)
        {
            BeatModifier = new BeatModifier(MessageAddress, beat);
            Geometry = new Geometry(MessageAddress, beat);
            Texture = new Texture(MessageAddress);
            Coloration = new Coloration(MessageAddress, beat);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
            //ResetEntityCommand = new RelayCommand(p => ResetEntity());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        
        public MessengerService MessengerService { get; set; }
        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }
        #endregion

        #region COPY/PASTE
        public void Reset()
        {
            MessengerService.Disable();

            this.Enabled = true;
            this.BeatModifier.Reset();
            this.Geometry.Reset();
            this.Texture.Reset();
            this.Coloration.Reset();

            MessengerService.Enable();
        }


        public void CopyEntity()
        {
            IDataObject data = new DataObject();
            data.SetData(nameof(EntityModel), this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteEntity()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(EntityModel)))
            {
                //this.Mementor.BeginBatch();
                this.MessengerService.Disable();

                var entityModel = data.GetData(nameof(EntityModel)) as EntityModel;
                this.SetViewModel(entityModel);
                this.MessengerService.Enable();
                //this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

        //public void ResetEntity()
        //{
        //    EntityModel entityModel = GetModel();
        //    this.Reset();
        //    //SendMessages(nameof(ContentModel), entityModel);
        //}
        #endregion
    }
}