
namespace Frostwalk.GameConsole
{
    public abstract class Command
    {
        
        string[] keywords;
        string help;

        /// <summary>
        /// Creates a new command object. This also sets the keywords of the command.
        /// </summary>
        public Command()
        {
            SetKeywords();
            SetHelp();
        }

        /// <summary>
        /// The keyword(s) of the command that indicate this command is being called.
        /// </summary>
        public string[] Keywords { get { return keywords; } protected set { keywords = value; } }

        public string Help { get { return help; } protected set { help = value; } }

        /// <summary>
        /// Sets the keywords of this command.
        /// </summary>
        protected abstract void SetKeywords();

        /// <summary>
        /// Sets the help documentation of this command
        /// </summary>
        protected abstract void SetHelp();

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="argString">Command's arguments in one contiguous string.</param>
        public abstract void Run(string argString);

        public abstract void PrintHelp();
    }
}
