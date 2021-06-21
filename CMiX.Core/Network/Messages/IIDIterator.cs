// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;

namespace CMiX.Core.Network.Messages
{
    public interface IIDIterator
    {
        void Next();
        bool IsDone { get; }
        Guid CurrentID { get; }

        void Process<T>(T receiver);

        List<Guid> IDs { get; }
    }
}