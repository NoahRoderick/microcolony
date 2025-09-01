# MicroColony Documentation

This directory contains design notes and changelog for the MicroColony project.

- `README.md`: This file.
- `CHANGELOG.md`: Version history.

## Scene Setup

1. **Main Menu**
   - Create a `Control` root and attach `scripts/MainMenu.cs`.
   - Add a `Button` named `StartButton` to load `scenes/game.tscn`.
2. **HUD**
   - Root `Control` with `scripts/UIHUD.cs`.
   - Add ProgressBars `O2Bar`, `H2OBar`, `PowerBar`.
   - Add Buttons `SolarButton`, `RecyclerButton`, `HydroponicsButton` (toggle mode on).
   - Add `TimerLabel` for countdown and `RestartButton` (hidden by default).
3. **Game**
   - Root `Node2D` with `scripts/Game.cs`.
   - Add `Timer` named `TickTimer` (wait time 1s, autostart).
   - Instance the HUD scene as `HUD` and link exported paths.
   - Add building nodes `Solar`, `Recycler`, `Hydroponics` with `scripts/Building.cs`.

Hook up exported node paths in the Inspector so the game script can find its HUD and buildings.
