using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Commands
{
    public class EditCommand : Command
    {
        private Action _action;
        public EditCommand(Action action) {
            _action = action;
        }
        public override void Execute(object? parameter)
        {
            _action.Invoke();
        }
    }
}
