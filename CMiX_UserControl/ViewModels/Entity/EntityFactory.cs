﻿using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class EntityFactory
    {
        public EntityFactory()
        {

        }

        private int EntityID { get; set; } = 0;

        public Entity CreateEntity(BeatModifier beatModifier, string parentMessageAddress, Sender sender, Mementor memento)
        {
            BeatModifier beatModifier = new BeatModifier()
            Entity entity = new Entity(beatModifier, EntityID, parentMessageAddress, sender, memento);
            EntityID++;
            return entity;
        }
    }
}