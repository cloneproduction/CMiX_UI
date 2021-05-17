using System;

namespace CMiX.MVVM.MessageService
{
    public interface IIDAggregator
    {
        IIDIterator CreateIterator();
        void AddID(Guid id);
    }
}