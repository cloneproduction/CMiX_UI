﻿using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels.MessageService.Messages
{
    public class MessageUpdateViewModel : IViewModelMessage
    {
        public MessageUpdateViewModel()
        {

        }

        public MessageUpdateViewModel(Guid moduleID, IModel model)
        {
            Model = model;
            ModuleID = moduleID;
        }

        public IModel Model { get; set; }
        public Guid ModuleID { get; set; }
        public Guid ComponentID { get; set; }
    }
}