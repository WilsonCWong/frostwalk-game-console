using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostwalk.GameConsole
{
    /// <summary>
    /// Holds a snapshot of a command that was previously entered.
    /// </summary>
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
