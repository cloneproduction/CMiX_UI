using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;

namespace CMiX.MVVM.ViewModels.Components
{
    public class Entity : Component
    {
        public Entity(Scene scene, EntityModel entityModel) 
            : base (entityModel)
        {
            MasterBeat = scene.MasterBeat;

            BeatModifier = new BeatModifier(this.MessageDispatcher, scene.MasterBeat, entityModel.BeatModifierModel);
            Geometry = new Geometry(this.MessageDispatcher, scene.MasterBeat, entityModel.GeometryModel);
            Texture = new Texture(this.MessageDispatcher, scene.MasterBeat, entityModel.TextureModel);
            Coloration = new Coloration(this.MessageDispatcher, scene.MasterBeat, entityModel.ColorationModel);
            Visibility = new Visibility(this.MessageDispatcher, scene.Visibility, entityModel.VisibilityModel);
        }


        public BeatModifier BeatModifier { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override IModel GetModel()
        {
            EntityModel model = new EntityModel(this.ID);

            model.Enabled = this.Enabled;
            model.Name = this.Name;

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