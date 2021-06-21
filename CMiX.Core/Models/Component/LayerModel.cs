// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Models.Component;
using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Models
{
    [Serializable]
    public class LayerModel : IComponentModel
    {
        public LayerModel()
        {

        }

        public LayerModel(Guid id)
        {
            ID = id;
            BlendMode = new BlendModeModel();
            PostFXModel = new PostFXModel();
            Fade = new SliderModel();
            MaskModel = new MaskModel();
            VisibilityModel = new VisibilityModel();
            ComponentModels = new ObservableCollection<IComponentModel>();
        }

        public Guid ID { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public bool Out { get; set; }
        public string Address { get; set; }
        public bool IsVisible { get; set; }


        public VisibilityModel VisibilityModel { get; set; }
        public BlendModeModel BlendMode { get; set; }
        public SliderModel Fade { get; set; }
        public MaskModel MaskModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}