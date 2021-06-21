﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;

namespace CMiX.Core.Presentation.ViewModels
{
    public interface IControl : IIDObject
    {
        void SetCommunicator(Communicator communicator);
        void UnsetCommunicator(Communicator communicator);

        void SetViewModel(IModel model);
        IModel GetModel();
    }
}