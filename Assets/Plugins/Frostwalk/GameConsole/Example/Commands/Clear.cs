using Frostwalk.GameConsole;

internal class Clear : Command
{
    public override void Run(string argString)
    {
        DeveloperConsole.Instance.ClearTextLog();
    }

    public override void PrintHelp()
    {
        DeveloperConsole.Instance.AddToTextLog(Help);
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "clear" };
    }

    protected override void SetHelp()
    {
        Help = "The clear command clears the log of all text.";
    }
}