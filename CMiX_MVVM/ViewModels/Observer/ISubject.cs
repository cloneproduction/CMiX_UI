using System;

namespace CMiX.MVVM.ViewModels.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify(int count);
    }
}
