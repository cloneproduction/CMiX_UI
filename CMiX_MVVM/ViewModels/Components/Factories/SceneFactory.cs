﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(Layer parentLayer)
        {
            ParentLayer = parentLayer;
        }


        private static int ID = 0;
        private Layer ParentLayer { get; set; }


        public Component CreateComponent()
        {
            var messageDispatcher = new MessageDispatcher();
            var component = new Scene(ParentLayer, new SceneModel(Guid.NewGuid()), messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var messageDispatcher = new MessageDispatcher();
            return new Scene(ParentLayer, model as SceneModel, messageDispatcher);
        }
    }
}