using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostwalk.GameConsole
{
    public static class GameConsoleUtility
    {
        /// <summary>
        /// Checks if there is a valid number of arguments left in the arguments array
        /// at its current index. 
        /// </summary>
        /// <param name="curInd">The current index of the array.</param>
        /// <param name="nArgs">The minimum number of arguments for the command.</param>
        /// <param name="length">The length of the argument array.</param>
        /// <returns>Validity.</returns>
        public static bool CheckValidNumOfArgs(int curInd, int nArgs, int length)
        {
            if (nArgs + curInd < length)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Splits an argument string by spaces and returns the string array.
        /// </summary>
        /// <param name="args">The argument string with all the arguments.</param>
        /// <returns>The argument string split into a string array.</returns>
        public static string[] SplitArgs(string args)
        {
            return args.Split(' ');
        }

        /// <summary>
        /// Trims the end of an argument string and then splits it by spaces and 
        /// returns the string array.
        /// </summary>
        /// <param name="args">The argument string.</param>
        /// <returns>The argument string with a trimmed end split into a string array.</returns>
        public static string[] SplitArgsAndTrim(string args)
        {
            return args.TrimEnd(' ').Split(' ');
        }
    }
}
