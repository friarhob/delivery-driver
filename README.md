# Delivery Driver

You can play it [here](https://friarhob.github.io/delivery-driver).

Simple prototype developed (and changed to include rules, a game mechanic and refactoring) based on [GameDev.tv Unity2D Course on Udemy](https://www.udemy.com/course/unitycourse/).

---

## Rules

* WASD/Arrows to move (also supports joysticks, but I didn't test this throughouly).

* You need to find and get all packages (red squares) in the map and deliver to the customer (purple circle).

* Whenever you crash your car, you lose a life. You have 5 lives to start.

* Powerups can:
  - Increase your speed.
  - Decrease your speed.
  - Add more time.
  - Add more packages.
  - Remove packages.

---

## Basic Architecture Decisions

### Managers

* **GameManager** is a static singleton that hosts game states, including:
  - lives
  - level
  - remaining time
  - remaining packages
  - score

* **EventManager** is also a static singleton, responsible for calling all internal events in the game, as stated by the following table:

| Event Name         | Function to invoke it | Notes                                                                           |
| ------------------ | --------------------- | ------------------------------------------------------------------------------- |
| onCarCrash         | carCrash()            | Event just called if there wasn't a previous crash less than 0.5 seconds before |
| onFinishLevel      | finishLevel()         | -                                                                               |
| onGameOver         | gameOver()            | Game over should just run in case of defeat                                     |
| onGameWon          | gameWon()             | -                                                                               |
| onPackageDelivered | packageDelivered()    | Package removed by powerup also triggers this option                            |
| onStartNewGame     | startNewGame()        | Event called even in the first run                                              |
| onStartNewLevel    | startNewLevel()       | -                                                                               |

* **UIManager** controls visibility of all panes, texts (using TMP), and also the generation/destruction of prefabs (packages and powerups). Packages and powerups are generated randomly, with the following rules: 1 package per level, half powerup per level (rounded up). Powerups include:

| PowerUp Colour | Description                            |
| -------------- | -------------------------------------- |
| Blue           | Reduces the car speed                  |
| Green          | Adds 10 more seconds to the level      |
| Purple         | Adds a new random package to the level |
| Red            | Remove a random package from the level |
| Yellow         | Increases the car speed                |

### Canvas

#### Text fields

* **LivesTMPText** shows the number of lives.
* **PackagesTMPText** shows the number of remaining packages.
* **TimerTMPText** shows the remaining game time in seconds.
* **ScoreTMPText** shows the total score for the game.

#### Panels

* **GameOverPanel** shows a game over message, when a player loses (either by using all their lives or time running out), with buttons for starting a new game and opening the instructions panel.
* **InstructionsPanel** opens when the game start, showing the rules, with a button for starting a new game.
* **LevelWonPanel** shows a congratulations message, when a player wins the level (delivering all packages), with button for starting next level.

---

## To-do List

* Compose music + add sounds
* Local high-score