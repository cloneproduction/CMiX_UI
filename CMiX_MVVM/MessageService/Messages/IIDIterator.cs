
using System;

namespace CMiX.MVVM.MessageService
{
    public interface IIDIterator
    {
        void Next();
        bool IsDone { get; }
        Guid CurrentID { get; }
        Message Message { get; set; }
    }
}