
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.MessageService
{
    public interface IIDIterator
    {
        void Next();
        bool IsDone { get; }
        Guid CurrentID { get; }

        Message Message { get; }
        List<Guid> IDs { get; }
    }
}