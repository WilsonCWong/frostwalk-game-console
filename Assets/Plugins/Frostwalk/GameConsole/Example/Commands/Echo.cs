using Frostwalk.GameConsole;

internal class Echo : Command
{
    public override void Run(string argString)
    {
        if (argString == null)
        {
            DeveloperConsole.Instance.AddToTextLog("echo: Invalid message.");
        }

        DeveloperConsole.Instance.AddToTextLog(argString);
    }

    public override void PrintHelp()
    {
        DeveloperConsole.Instance.AddToTextLog(Help);
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "echo" };
    }

    protected override void SetHelp()
    {
        Help = "The echo command prints whatever you put as its argument to the screen.";
    }
}