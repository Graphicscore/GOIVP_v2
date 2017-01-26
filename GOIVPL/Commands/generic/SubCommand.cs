using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIVPL.Commands.generic
{
    public abstract class SubCommand : InterfaceCommand
    {
        public abstract string getName();
    }
}
