# MicroColony 🛠️🌱
One-screen Mars dome survival built with **Godot 4 (.NET/C#)**. Keep O₂ / H₂O / Power above zero while 3 colonists consume resources. Survive 10 minutes. Lose if any meter hits 0.

## 🎮 Core Loop
- Place/toggle buildings: **Solar**, **Water Recycler**, **Hydroponics**
- Each second: colonists consume; buildings produce
- Random events (dust storms, leaks) [planned]

## 🧩 Controls
- Mouse: toggle buildings / UI buttons
- Esc: pause / menu

## 🗂 Project Layout

- `assets/` sprites, audio, fonts
- `scenes/` main_menu.tscn, game.tscn, ui_hud.tscn
- `scripts/` C# scripts (Game, Colonist, Building, UIHUD)
- `ui/` HUD scenes and styles
- `data/` balance.csv + balance.json
- `docs/` project docs and changelog

