using System;

namespace CMiX.Core.Presentation.ViewModels.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify(int count);
    }
}
