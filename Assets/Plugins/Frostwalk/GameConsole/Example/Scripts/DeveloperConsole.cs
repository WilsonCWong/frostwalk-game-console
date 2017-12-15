using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Frostwalk.GameConsole;

public class DeveloperConsole : MonoBehaviour
{
    static DeveloperConsole _instance;

    public static DeveloperConsole Instance { get { return _instance; } }

    [SerializeField] InputField inputField;
    [SerializeField] Text textLog;

    GameConsole console;

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
    }

    public void ClearTextLog()
    {
        textLog.text = "";
    }
}
