# hunt-the-wumpus
My implementation of the [hunt the wumpus](https://en.wikipedia.org/wiki/Hunt_the_Wumpus) text based game

Compiling and Running
---------

### Linux

```bash
sudo apt install mono-complete
xbuild HuntTheWumpus.sln

```

### Windows
Insure you have [.NET Framework 4.6.1](https://www.microsoft.com/net/download)

Now, navigate to the hunt-the-wumpus folder in Windows Explorer. To open a command prompt at this location go to File > open command prompt.

Assuming the typical path to msbuild, use msbuild to build the solution file.

```shell
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe HuntTheWumpus.sln
```

The executable should be located under: `hunt-the-wumpus\bin\Debug\HuntTheWumpus.exe`

Cheat Mode
---------

Cheat mode will display the locations of all the hazards when you start a game and will display the location of the Wumpus everytime it moves.

To enable cheat mode pass the paramter "cheat" to the executable.
For example, assuming you're in the same directory of the executable:

for Windows:
```shell
HuntTheWumpus.exe cheat
```

for Linux:
```bash
./HuntTheWumpus cheat
```
