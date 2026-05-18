# Tic Tac Toe Console Game

## Description

This project is a console implementation of the classic **Tic Tac Toe** game in C#.

The solution contains two projects:

- `MenuLib` - class library for menu navigation
- `TicTacToe` - console application with the game logic

When the application starts, the user must enter a username. After that, the main menu opens. From the main menu, the user can start a game, change the username, view information about the developer, or quit the application.

---

## Project Structure

```text
Homework2
│
├── README.md
│
├── MenuLib
│   ├── Menu.cs
│   ├── MenuRunner.cs
│   ├── MenuStack.cs
│   ├── NavigationResult.cs
│   └── NavigationResultType.cs
│
└── TicTacToe
    ├── Program.cs
    ├── AppData.cs
    │
    ├── Game
    │   ├── Board.cs
    │   ├── CellSymbol.cs
    │   ├── ComputerPlayer.cs
    │   ├── GameMode.cs
    │   ├── HumanPlayer.cs
    │   ├── Player.cs
    │   └── TicTacToeGame.cs
    │
    └── MenuImplementation
        ├── AboutMenu.cs
        ├── MainMenu.cs
        ├── PlayMenu.cs
        └── SettingsMenu.cs
```

---

## Projects

### MenuLib

`MenuLib` is a class library that contains the menu navigation logic.

It includes:

- `Menu` - base abstract class for all menus
- `MenuRunner` - runs the menu system
- `MenuStack` - custom stack implementation based on an array
- `NavigationResult` - describes the result of a menu action
- `NavigationResultType` - enum with navigation result types

### TicTacToe

`TicTacToe` is the console application.

It contains:

- application start logic
- username input
- main menu implementation
- game mode selection
- settings screen
- about screen
- Tic Tac Toe game logic

---

## Features

- Username input before opening the main menu
- Main menu with:
  - Play
  - Settings
  - About
  - Quit
- Game mode selection:
  - Player vs Player
  - Player vs Computer
- Symbol selection:
  - X
  - O
- Visual 3x3 game board
- Keyboard navigation on the board using:
  - Arrow keys
  - WASD
- Enter key places the symbol on the selected cell
- Occupied cells cannot be overwritten
- Winner detection
- Draw detection
- Return to main menu after the game ends
- Username can be changed in Settings

---

## Controls

### Menu Controls

In the menu, enter the option number and press `Enter`.

Example:

```text
1
```

Additional commands:

```text
back
exit
```

### Symbol Selection

After choosing a game mode, enter:

```text
X
```

or

```text
O
```

You can also enter:

```text
0
```

to select `O`.

To return back, enter:

```text
back
```

### Game Board Controls

Use these keys to move around the board:

```text
Arrow Keys
```

or

```text
W A S D
```

Press:

```text
Enter
```

to place the current player's symbol.

Press:

```text
Esc
```

to return to the main menu.

---

## Technical Requirements

The project follows the homework requirements:

- Uses `MenuLib` for menu navigation
- Uses object-oriented programming principles
- Does not use `System.Collections.Generic` collections such as:
  - `List`
  - `Dictionary`
  - `Stack`
  - `Queue`
  - `HashSet`
- Uses arrays instead of generic collections
- Keeps game logic separated into different classes and files

---

## Main Classes

### Board

Responsible for:

- storing the 3x3 board
- placing symbols
- checking empty cells
- checking winner
- checking draw
- drawing the board

### TicTacToeGame

Responsible for:

- running the game loop
- switching turns
- handling keyboard input
- checking game result
- returning to menu after game ends

### Player

Base class for players.

### HumanPlayer

Represents a human player.

### ComputerPlayer

Represents the computer player.

The computer uses simple logic:

1. Try to win if possible
2. Block the opponent if needed
3. Take the center cell if available
4. Take a corner if available
5. Take any empty cell

---

