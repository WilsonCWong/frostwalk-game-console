using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Frostwalk.GameConsole;
using System;
using System.Linq;

public class DeveloperConsole : MonoBehaviour
{
    static DeveloperConsole _instance;
    GameConsole console;

    public static DeveloperConsole Instance { get { return _instance; } }
    public GameConsole Console { get { return console; } }

    [SerializeField] InputField inputField;
    [SerializeField] Text textLog;
    [SerializeField] int lineLimit = 150;

	void Awake () 
	{
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            inputField.onEndEdit.AddListener(ParseTextInput);
            console = new GameConsole();
        }
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && inputField.isFocused)
        {
            inputField.MoveTextStart(false);
            CommandSnapshot snapshot = console.GetPreviousCommand();
            inputField.text = PrintSnapshot(snapshot);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && inputField.isFocused)
        {
            inputField.MoveTextStart(false);
            CommandSnapshot snapshot = console.GetNextCommand();
            inputField.text = PrintSnapshot(snapshot);
        }
    }

    string PrintSnapshot (CommandSnapshot snap)
    {
        if (snap.Args == "")
            return snap.Command;
        else
            return snap.Command + " " + snap.Args;
    }

    int CheckOverLineLimit()
    {
        int numLines = textLog.text.Length - textLog.text.Replace("\n", string.Empty).Length;
        Debug.Log(numLines);
        if (numLines > lineLimit)
            return numLines - lineLimit;
        else
            return 0;
    }

    public void ParseTextInput(string input)
    {
        if (inputField.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            AddToTextLog("> " + input);
            if (!console.ParseInput(input))
            {
                string command = console.CommandHistory[console.CommandHistory.Count - 1].Command;
                if (command != "")
                    AddToTextLog("The command " + command + " was not found.");
                else
                    AddToTextLog("Invalid input.");

            }

            inputField.text = "";
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            inputField.Select();
        }
    }
	
    public void AddToTextLog(string output)
    {
        textLog.text += "\n" + output;
        int nLines = CheckOverLineLimit();
        if (nLines != 0)
        {
            string[] lines = textLog.text
                .Split('\n')
                .Skip(nLines)
                .ToArray();

            textLog.text = string.Join("\n", lines);
        } 
    }

    public void ClearTextLog()
    {
        textLog.text = "";
    }
}
