using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyTicketWPFClient.RouteCalc
{
    public class DelegateCommand : ICommand
    {
        private Action _execute;
        public DelegateCommand(Action execute)
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
            _execute.Invoke();
        }
    }
}
