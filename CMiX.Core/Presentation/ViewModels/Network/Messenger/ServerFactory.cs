﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace CMiX.Core.Presentation.ViewModels.Network
{
    public class ServerFactory
    {
        int ID = 0;

        public Server CreateServer()
        {
            var server = new Server(ID);
            ID++;
            return server;
        }
    }
}