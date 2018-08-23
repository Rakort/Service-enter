using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ServiceСenter.Commands
{
    public class SimpleCommand : ICommand
    {
        public Boolean CommandSucceeded { get; set; }

        public Predicate<object> CanExecuteDelegate { get; set; }

        public Action<object> ExecuteDelegate { get; set; }

        public bool IsExecute { get; set; }

        public SimpleCommand(Action p_action)
        {
            ExecuteDelegate = o => p_action();
            IsExecute = true;
        }
        public SimpleCommand(Action<object> p_action)
        {
            ExecuteDelegate = p_action;
            IsExecute = true;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (!IsExecute) return;
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
                CommandSucceeded = true;
            }
        }
        
    }
}
