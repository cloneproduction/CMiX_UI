using System;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<object> CommandFunc { get; }
        private Predicate<object> CanExecuteFunc { get; }

        //public event EventHandler CanExecuteChanged;
        //{
        //    add => CommandManager.RequerySuggested += value;
        //    remove => CommandManager.RequerySuggested -= value;
        //}

        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


        public RelayCommand(Action<object> command)
            : this(command, p => true)
        { }

        public RelayCommand(Action<object> command, Predicate<object> canExecute)
        {
            CommandFunc = command ?? throw new ArgumentNullException(nameof(command));
            CanExecuteFunc = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object parameter) => CanExecuteFunc.Invoke(parameter);

        public void Execute(object parameter) => CommandFunc.Invoke(parameter);
    }
}
