using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Entity : Component, IBeat
    {
        public Entity(int id, MessageTerminal MessageTerminal, MasterBeat beat) : base (id, MessageTerminal)
        {
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, beat);
            Geometry = new Geometry(nameof(Geometry), this, beat);
            Texture = new Texture(nameof(Texture), this);
            Coloration = new Coloration(nameof(Coloration), this, beat);
        }

        public BeatModifier BeatModifier { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public MasterBeat MasterBeat { get; set; }

        public override void Dispose()
        {
            BeatModifier.Dispose();
            Geometry.Dispose();
            Texture.Dispose();
            Coloration.Dispose();
            base.Dispose();
        }

        public override IModel GetModel()
        {
            EntityModel model = new EntityModel();

            model.Enabled = this.Enabled;
            model.Name = this.Name;
            model.ID = this.ID;

            model.BeatModifierModel = (BeatModifierModel)this.BeatModifier.GetModel();
            model.TextureModel = (TextureModel)this.Texture.GetModel();
            model.GeometryModel = (GeometryModel)this.Geometry.GetModel();
            model.ColorationModel = (ColorationModel)this.Coloration.GetModel();

            return model;
        }

        public override void SetViewModel(IModel model)
        {
            EntityModel entityModel = model as EntityModel;
            this.BeatModifier.SetViewModel(entityModel.BeatModifierModel);
            this.Texture.SetViewModel(entityModel.TextureModel);
            this.Geometry.SetViewModel(entityModel.GeometryModel);
            this.Coloration.SetViewModel(entityModel.ColorationModel);
        }
    }
}