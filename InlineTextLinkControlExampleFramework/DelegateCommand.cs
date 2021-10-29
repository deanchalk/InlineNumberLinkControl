using System;
using System.Windows.Input;

namespace InlineTextLinkControlExampleFramework
{
    public sealed class DelegateCommand<T> : ICommand
    {
        private readonly Func<T,bool> canExecute;
        private readonly Action<T> execute;

        public DelegateCommand(Action<T> execute, Func<T,bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}