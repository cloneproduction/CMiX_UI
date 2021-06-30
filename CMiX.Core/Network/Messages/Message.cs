// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;


namespace CMiX.Core.Network.Messages
{
    public abstract class Message
    {
        public List<Guid> IDs { get; set; }

        public IIDIterator CreateIterator()
        {
            var iterator = new MessageIDIterator(this);
            return iterator;
        }

        public int Count
        {
            get { return IDs.Count; }
        }

        public Guid GetID(int index)
        {
            return IDs[index];
        }

        public void AddID(Guid id)
        {
            if (IDs == null)
                IDs = new List<Guid>();

            IDs.Insert(0, id);
        }

        public abstract void Process<T>(T receiver);
    }
}
