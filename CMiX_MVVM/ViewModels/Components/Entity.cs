using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component
    {
        public Entity(int id, Beat beat) : base(id, beat)
        {
            BeatModifier = new BeatModifier(beat);

            Geometry = new Geometry(beat, this);
            Texture = new Texture(this);
            Coloration = new Coloration(beat, this);

            CopyEntityCommand = new RelayCommand(p => CopyEntity());
            PasteEntityCommand = new RelayCommand(p => PasteEntity());
        }

        public ICommand CopyEntityCommand { get; }
        public ICommand PasteEntityCommand { get; }
        public ICommand ResetEntityCommand { get; }
        
        public BeatModifier BeatModifier { get; }
        public Geometry Geometry { get; }
        public Texture Texture { get; }
        public Coloration Coloration { get; }


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
                var entityModel = data.GetData(nameof(EntityModel)) as EntityModel;
                this.SetViewModel(entityModel);
                //this.Mementor.EndBatch();
                //SendMessages(nameof(ContentModel), contentmodel);
            }
        }

    }
}