using System;
using UnityEngine;
using Frostwalk.GameConsole;


internal class Spawn : Command
{
    public override void Run(string argString)
    {
        string[] args = GameConsoleUtility.SplitArgsAndTrim(argString);
        int argLen = args.Length;

        Vector3 position = new Vector3();
        
        if (args == null || args.Length == 0)
        {
            DeveloperConsole.Instance.AddToTextLog("spawn: Invalid GameObject.");
            return;
        }

        for (int i = 0; i < argLen; i++)
        {
            switch (args[i])
            {
                case "at":
                    if (GameConsoleUtility.CheckValidNumOfArgs(i, 1, argLen))
                    {
                        try
                        {
                            position = ParsePosition(args[i + 1], position);
                            i++;
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

        switch(args[0])
        {
            case "Cube":
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = position;
                break;
            default:
                DeveloperConsole.Instance.AddToTextLog("GameObject " + args[0] + "was not found.");
                break;
        }
    }

    protected override void SetKeywords()
    {
        Keywords = new string[] { "spawn", "create" };
    }

    Vector3 ParsePosition(string positionString, Vector3 position)
    {
        if (positionString.Length > 3 || positionString.Length < 1 )
            throw new Exception();

        string[] coordinateString = positionString.Split(',');
        if (coordinateString.Length == 2)
        {
            position.x = float.Parse(coordinateString[0]);
            position.y = float.Parse(coordinateString[1]);
        }
        else if (coordinateString.Length == 3)
        {
            position.x = float.Parse(coordinateString[0]);
            position.y = float.Parse(coordinateString[1]);
            position.z = float.Parse(coordinateString[2]);
        }
        else
        {
            DeveloperConsole.Instance.AddToTextLog("spawn: Invalid position. Please at least enter" +
                " an x or y position.");
            throw new Exception();
        }

        return position;
    }
}