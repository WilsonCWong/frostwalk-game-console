# Frostwalk Game Console

<p align="center" > 
<img src="https://i.imgur.com/37KVyNe.png" title="Frostwalk Game Console Logo" width=60%>
</p>

Frostwalk Game Console is a Unity package by Frostwalk Studio for creating in-game and developer consoles. The framework has been developed to allow for flexible and easy command additions, and it is very easy to make extensions to the framework.

<p align="center"> 
<img src="https://media.giphy.com/media/xT0xePUIxhXtO3r7gI/giphy.gif" title="Developer Console Demonstration" style="width: 60%;" />
</p>

The framework comes with the following features:
* Create individual consoles to allow for multiple different consoles in one project.
* Automatically detects commands. Just subclass the Command class and the GameConsole class will add it to the list. You can, of course, override this behavior.
* Flexible commands. Parse the arguments however you want and control the behavior of the commands however you wish.
* Building on that last point, commands can be activated by any number of aliases you desire (e.g. spawn and create do the same thing).
* Utility methods to make creating new commands a cleaner and less painful.
* Built in help syntax (accessed through ```command ?```).

The package also comes with an example developer console, which has the following:
* Basic error checking.
* The ```help```,```create```, ```echo```, and ```spawn/create``` commands.
* Text log display with a scrolling view.
* Keeps previous commands in memory for quick access (with the up arrow on the keyboard).
* Automatic text log snipping when it gets too long.

# How to Use
* Download the package from the github releases.
* You will need a class or multiple classes to manage your console(s). Look at the developer console example for an idea of how to do this.
* You need to extend from the abstract class Command from the Frostwalk.GameConsole namespace to create your own commands. You'll need to override a couple of methods. The developer example project has a couple of commands you can look at.
