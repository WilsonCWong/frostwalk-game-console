using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Frostwalk.GameConsole
{
    public class GameConsole
    {
        List<Command> commandList;
        List<CommandSnapshot> commandHistory;

        public List<CommandSnapshot> CommandHistory { get { return commandHistory; } }

        
        /// <summary>
        /// Create a game console with all the commands in Command's assembly.
        /// </summary>
        public GameConsole()
        {
            InitializeCommandList();
            commandHistory = new List<CommandSnapshot>();
        }

        /// <summary>
        /// Create a console with your own list of commands. This is useful for creating
        /// multiple different consoles for games like those hacker games.
        /// </summary>
        /// <param name="commandList">Your list of commands.</param>
        public GameConsole(List<Command> commandList)
        {
            this.commandList = commandList;
            commandHistory = new List<CommandSnapshot>();
        }

        /// <summary>
        /// Parses the input and parses the command if it is in proper command format.
        /// </summary>
        /// <param name="input">The command input.</param>
        /// <returns>A boolean that reports whether the command was parsed or not.</returns>
        public virtual bool ParseInput(string input)
        {
            Regex cmdRegex = new Regex(@"^([A-Za-z]+)\s?(.+)*");
            Match match = cmdRegex.Match(input);

            GroupCollection data = match.Groups;
            String cmd = data[1].Value;
            String args = "";

            if (data[2].Success)
            {
                 args = data[2].Captures[0].Value;
            }       

            if (match.Success)
            {
                return ParseCommand(cmd, args);
            }
            else
            {
                commandHistory.Add(new CommandSnapshot(cmd, args));
                return false;
            }            
        }

        /// <summary>
        /// Parses the command and runs it if it matches a command in the list.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="args">The command arguments.</param>
        /// <returns>A boolean reporting on the success of the parse.</returns>
        private bool ParseCommand(string cmd, string args)
        {
            commandHistory.Add(new CommandSnapshot(cmd, args));
            foreach (Command c in commandList)
            {
                foreach (string cmdString in c.Keywords)
                {
                    if (cmdString == cmd)
                    {
                        c.Run(args);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Initializes a list of commands from all the classes that subclass Command in its
        /// assembly.
        /// </summary>
        protected void InitializeCommandList()
        {
            IEnumerable<Command> commands = typeof(Command).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)))
                .Select(t => (Command)Activator.CreateInstance(t));

            commandList = commands.ToList();
        }
    }
}

