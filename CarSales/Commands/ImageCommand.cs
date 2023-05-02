using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Commands
{
    public class ImageCommand: Command
    {
        private Action _execute;
        public ImageCommand(Action execute)
        {
            _execute = execute;
        }

        public override void Execute(object? parameter)
        {
            _execute.Invoke();
        }
    }
}
