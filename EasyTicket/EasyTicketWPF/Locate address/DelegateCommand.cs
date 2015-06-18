using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyTicketWPFClient
{
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> _execute;

        public DelegateCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute.Invoke((T)parameter);
        }
    }
}
