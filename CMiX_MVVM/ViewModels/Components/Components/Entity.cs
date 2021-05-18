﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
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

            BeatModifier = new BeatModifier(scene.MasterBeat, entityModel.BeatModifierModel);
            Geometry = new Geometry(scene.MasterBeat, entityModel.GeometryModel);
            Texture = new Texture(scene.MasterBeat, entityModel.TextureModel);
            Coloration = new Coloration(scene.MasterBeat, entityModel.ColorationModel);
            Visibility = new Visibility(scene.Visibility, entityModel.VisibilityModel);
        }


        public BeatModifier BeatModifier { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        public MasterBeat MasterBeat { get; set; }


        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            base.SetReceiver(messageReceiver);

            BeatModifier.SetReceiver(MessageReceiver);
            Geometry.SetReceiver(MessageReceiver);
            Texture.SetReceiver(MessageReceiver);
            Coloration.SetReceiver(MessageReceiver);
            Visibility.SetReceiver(MessageReceiver);
        }

        public override IMessageSender SetSender(IMessageSender messageSender)
        {
            var sender = base.SetSender(messageSender);

            BeatModifier.SetSender(sender);
            Geometry.SetSender(sender);
            Texture.SetSender(sender);
            Coloration.SetSender(sender);
            Visibility.SetSender(sender);

            return sender;
        }


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