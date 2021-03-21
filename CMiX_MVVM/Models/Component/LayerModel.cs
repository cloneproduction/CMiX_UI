﻿using CMiX.MVVM.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class LayerModel : IComponentModel
    {
        public LayerModel()
        {

        }

        public LayerModel(int id)
        {
            ID = id;
            BlendMode = new BlendModeModel();
            PostFXModel = new PostFXModel();
            Fade = new SliderModel();
            MaskModel = new MaskModel();
            ComponentModels = new ObservableCollection<IComponentModel>();
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Out { get; set; }
        public string Address { get; set; }
        public bool IsVisible { get; set; }

        public BlendModeModel BlendMode { get; set; }
        public SliderModel Fade { get; set; }
        public MaskModel MaskModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}