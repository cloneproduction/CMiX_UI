﻿using CMiX.Core.Interfaces;
using CMiX.Core.Models.Component;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    public class EntityModel : IComponentModel
    {
        public EntityModel()
        {
            BeatModifierModel = new BeatModifierModel();
            GeometryModel = new GeometryModel();
            TextureModel = new TextureModel();
            ColorationModel = new ColorationModel();
            VisibilityModel = new VisibilityModel();
        }

        public EntityModel(Guid id) : this ()
        {
            ID = id;
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public Guid ID { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public ColorationModel ColorationModel { get; set; }
        public VisibilityModel VisibilityModel { get; set; }

        public string Address { get; set; }
        public bool IsVisible { get; set; }

        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}