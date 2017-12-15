using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Frostwalk.GameConsole;
using System.Linq;

/// <summary>
/// A singleton manager for the developer console.
/// </summary>
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
        // Make sure there is only one instance of this singleton
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
        // Checks for the input required for log history. I would recommend using a
        // input manager instead to make the Update call unnecessary.
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

    /// <summary>
    /// Prints a command snapshot.
    /// </summary>
    /// <param name="snap">The snapshot to print.</param>
    /// <returns>The string output.</returns>
    string PrintSnapshot (CommandSnapshot snap)
    {
        if (snap.Args == "")
            return snap.Command;
        else
            return snap.Command + " " + snap.Args;
    }

    /// <summary>
    /// Checks if the number of lines in the text log is over the limit, and returns the difference. 
    /// </summary>
    /// <returns>The line difference.</returns>
    int CheckOverLineLimit()
    {
        int numLines = textLog.text.Length - textLog.text.Replace("\n", string.Empty).Length;
        if (numLines > lineLimit)
            return numLines - lineLimit;
        else
            return 0;
    }

    /// <summary>
    /// Parses the input that was given through the input field.
    /// </summary>
    /// <param name="input">The input string.</param>
    public void ParseTextInput(string input)
    {
        // Make sure the input field has content.
        if (inputField.text != "" && (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            AddToTextLog("> " + input);
            if (!console.ParseInput(input))
            {
                // Input has failed here, so we need to determine why.
                string command = console.CommandHistory[console.CommandHistory.Count - 1].Command;
                if (command != "")
                    AddToTextLog("The command " + command + " was not found.");
                else
                    AddToTextLog("Invalid input.");

            }

            inputField.text = "";
            // Refocuses the input field.
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            inputField.Select();
        }
    }
	
    /// <summary>
    /// Adds a line of text to the text log.
    /// </summary>
    /// <param name="output">The output you want to print into the text log.</param>
    public void AddToTextLog(string output)
    {
        textLog.text += "\n" + output;

        // Automatically clears the first n lines of the log when it's over the limit
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

    /// <summary>
    /// Clears the text log completely.
    /// </summary>
    public void ClearTextLog()
    {
        textLog.text = "";
    }
}
