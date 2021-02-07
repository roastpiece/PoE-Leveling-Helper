## Features

- Set reminders for when you reach a certain level
- Set wether or not an audio alert should be played
- Set wether or not the reminder should be read by the System TTS engine
- Set your character name to only receive reminders for your chararcter (useful in party play)

## How it works

The program listens to changes to the Path of Exile log file.
It looks for `Character (Class) is now level XX` messages.

## Development

Download .NET Core SDK 5.0

Go into `PoE-Leveling-Helper` and run `dotnet run` to run the project.

## VSCode

To edit and run the project you need to open the `PoE-Leveling-Helper` folder with VSCode.
