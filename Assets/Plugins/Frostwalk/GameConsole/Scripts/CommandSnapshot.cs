using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostwalk.GameConsole
{
    public struct CommandSnapshot
    {
        string command;
        string args;

        public string Command { get { return command; } }
        public string Args { get { return args; } }

        public CommandSnapshot(string command, string args)
        {
            this.command = command;
            this.args = args;
        }
    }
}
