﻿using CMiX.Services;
using System;
namespace CMiX.Models
{
    [Serializable]
    public class ContentModel
    {
        public ContentModel()
        {
            BeatModifierModel = new BeatModifierModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public ContentModel(string messageaddress) 
            : this()
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, "Content");
            BeatModifierModel = new BeatModifierModel(messageaddress);
            TextureModel = new TextureModel(messageaddress);
            GeometryModel = new GeometryModel(messageaddress);
        }

        public string MessageAddress { get; set; }

        [OSC]
        public bool Enable { get; set; }
        
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}