
namespace Frostwalk.GameConsole
{
    public abstract class Command
    {
        
        string[] keywords;

        /// <summary>
        /// Creates a new command object. This also sets the keywords of the command.
        /// </summary>
        public Command()
        {
            SetKeywords();
        }

        /// <summary>
        /// The keyword(s) of the command that indicate this command is being called.
        /// </summary>
        public string[] Keywords { get { return keywords; } protected set { keywords = value; } }

        /// <summary>
        /// Sets the keywords of this command.
        /// </summary>
        protected abstract void SetKeywords();

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="argString">Command's arguments in one contiguous string.</param>
        public abstract void Run(string argString);
    }
}
