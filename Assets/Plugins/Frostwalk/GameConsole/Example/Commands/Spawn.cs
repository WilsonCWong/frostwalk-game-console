using System;
using UnityEngine;
using Frostwalk.GameConsole;

/// <summary>
/// Spawns a specified GameObject at an optional Vector2/Vector3 position. Currently only spawns
/// Cubes, but you can easily add more.
/// </summary>
internal class Spawn : Command
{
    public override void Run(string argString)
    {
        string[] args = GameConsoleUtility.SplitArgsAndTrim(argString);
        int argLen = args.Length;

        // Give our objects a default Vector3 to spawn at if no position is specified.
        Vector3 position = new Vector3();
        
        if (args == null || args.Length == 0)
        {
            DeveloperConsole.Instance.AddToTextLog("spawn: Invalid GameObject.");
            return;
        }

        // Check for additional clauses, like the "at" keyword
        for (int i = 0; i < argLen; i++)
        {
            switch (args[i])
            {
                case "at":
                    // This checks to see if there are enough arguments for the clause to work left in
                    // the argument array.
                    if (GameConsoleUtility.CheckValidNumOfArgs(i, 1, argLen))
                    {
                        try
                        {
                            position = ParsePosition(args[i + 1], position);
                            i++; // Since we already parse the next argument, we can skip it in the loop.
                        }
                        catch 
                        {
                            DeveloperConsole.Instance.AddToTextLog("spawn: Invalid Position.");
                            return;
                        }
                    }
                    else
                    {
                        DeveloperConsole.Instance.AddToTextLog("spawn: No position specified.");
                        return;
                    }
                    break;
            }
        }

        // See which object to spawn
        switch(args[0])
        {
            case "Cube":
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = position;
                break;
            default:
                DeveloperConsole.Instance.AddToTextLog("GameObject " + args[0] + " was not found.");
                break;
        }
    }

    public override void PrintHelp()
    {
        DeveloperConsole.Instance.AddToTextLog(Help);
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "spawn", "create" };
    }

    protected override void SetHelp()
    {
        Help = "The spawn or create command instantiates a GameObject.\n" +
            "- spawn/create <i>GameObject</i> [at Vector2|Vector3]";
    }

    /// <summary>
    /// Parses the position argument provided to the at clause.
    /// </summary>
    /// <param name="positionString">The position argument string.</param>
    /// <param name="position">A Vector3 of the current position.</param>
    /// <returns>The parsed Vector2/Vector3</returns>
    Vector3 ParsePosition(string positionString, Vector3 position)
    {
        if (positionString.Length > 3 || positionString.Length < 1 )
            throw new Exception();

        string[] coordinateString = positionString.Split(',');
        // Vector2
        if (coordinateString.Length == 2)
        {
            position.x = float.Parse(coordinateString[0]);
            position.y = float.Parse(coordinateString[1]);
        }
        // Vector3
        else if (coordinateString.Length == 3)
        {
            position.x = float.Parse(coordinateString[0]);
            position.y = float.Parse(coordinateString[1]);
            position.z = float.Parse(coordinateString[2]);
        }
        // Incorrect position format
        else
        {
            DeveloperConsole.Instance.AddToTextLog("spawn: Invalid position. Please at least enter" +
                " an x or y position.");
            // Throw an exception back to the Run method to allow it to handle this
            throw new Exception();
        }

        return position;
    }
}