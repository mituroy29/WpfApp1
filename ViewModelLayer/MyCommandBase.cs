using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelLayer
{
    public class MyCommandBase : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canExecuteMethod;

        public MyCommandBase(Action<object> ExecuteMethod, Func<object, bool> CanExecuteMethod)
        {
            this.executeMethod = ExecuteMethod;
            this.canExecuteMethod = CanExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
