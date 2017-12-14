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

    protected override void SetKeywords()
    {
        Keywords = new string[] { "echo" };
    }
}