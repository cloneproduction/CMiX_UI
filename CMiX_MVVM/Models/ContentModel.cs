﻿using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ContentModel : IModel
    {
        public ContentModel()
        {
            EntityModels = new List<EntityModel>();
            SelectedEntityModel = new EntityModel();
            BeatModifierModel = new BeatModifierModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public ContentModel(string messageAddress) 
            : this()
        {
            MessageAddress = $"{messageAddress}Content/";
        }

        public bool Enable { get; set; }
        public string MessageAddress { get; set; }

        public List<EntityModel> EntityModels { get; set; }
        public EntityModel SelectedEntityModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}