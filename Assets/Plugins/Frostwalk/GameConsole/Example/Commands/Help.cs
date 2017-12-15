using Frostwalk.GameConsole;
using System.Collections.Generic;

internal class Help : Command
{
    public override void Run(string argString)
    {
        string[] args = GameConsoleUtility.SplitArgsAndTrim(argString);
        if (args.Length == 0)
            DeveloperConsole.Instance.AddToTextLog("help: Please provide a command.");
        else if (args.Length > 1)
            DeveloperConsole.Instance.AddToTextLog("help: Please only enter one command argument.");
        else
        {
            List<Command> commandList = DeveloperConsole.Instance.Console.CommandList;
            foreach (Command command in commandList)
            {
                foreach(string keyword in command.Keywords)
                {
                    if (args[0] == keyword)
                    {
                        DeveloperConsole.Instance.AddToTextLog(command.Help);
                        return;
                    }
                }
            }
            DeveloperConsole.Instance.AddToTextLog("help: The specified command was not found.");
        }
    }

    public override void PrintHelp()
    {
        DeveloperConsole.Instance.AddToTextLog(Help);
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "help" };
    }

    protected override void SetHelp()
    {
        Help = "The help command prints the help documentation of the specified command.";
    }
}