﻿using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SceneModel : IComponentModel
    {
        public SceneModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();

            TransformModel = new TransformModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
            MaskModel = new MaskModel();
            BeatModifierModel = new BeatModifierModel();
        }

        public bool Enabled { get; set; }
        public bool IsVisible { get; set; }

        public TransformModel TransformModel { get; set; }
        public MaskModel MaskModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}