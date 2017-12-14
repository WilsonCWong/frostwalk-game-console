using Frostwalk.GameConsole;

internal class Clear : Command
{
    public override void Run(string argString)
    {
        DeveloperConsole.Instance.ClearTextLog();
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "clear" };
    }
}