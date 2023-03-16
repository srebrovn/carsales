using System;
using System.Windows.Input;

namespace CarSales.Commands
{
    public abstract class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? paramete);

        protected void OnCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
