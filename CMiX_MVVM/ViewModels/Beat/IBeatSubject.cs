namespace CMiX.MVVM.ViewModels
{
    public interface IBeatSubject
    {
        void Attach(IBeatObserver observer);

        void Detach(IBeatObserver observer);

        void NotifyBeatChange(double period);
    }
}
