# 🍄 Super Gyula — A Fan-Made meme game

> "Star — 10 seconds of invincibility with shimmering colors; enemies can be wiped out on contact."

The classic we all know, built from scratch in Unity. Jump, stomp Goombas, kick shells, and grab mushrooms — just like childhood, except now you can peek under the hood.

---

## 🎮 What's Inside

**Power-Ups & Invincibility**
- ⭐ **Star** — 10 seconds of invincibility with shimmering colors. Enemies can be wiped out on contact.
- 🍄 **1UP Mushroom** — an extra life. Don't miss it.

**Enemies**
- 🟤 **Goomba** — a single stomp from above is enough.
- 🐢 **Koopa** — a stomp from above tucks it into its shell; touching the shell launches it, and it takes out other enemies.
- ⚠️ Side contact deals damage; falling into a pit means instant loss of a life.

**Secrets**
- 👀 The level has invisible triggers that play a sound and reveal hidden objects when touched. Explore!

---

## 🗺️ Game Structure

Scenes in order (Build Settings):

1. **`loadcomp`** — studio logo splash screen (fade in and out).
2. **`Main Menu`** — main menu with "Play" and "Quit" buttons.
3. **`1-1`** — the game level. The one and only.

---

## 📁 Project Structure
Assets/
├── Scenes/ scenes: loadcomp, Main Menu, 1-1
├── Scripts/ game logic (C#)
├── Prefabs/ Mario, Goomba, Koopa, blocks, pipes, mushrooms, star, castle, etc.
├── Sprites/ graphics
├── sound/ music and sound effects
└── Materials/ physics materials (NoFriction)

**Key Scripts:**

| Script | What It Does |
|---|---|
| `GameManager` | Singleton: lives, coins, level loading and restarting |
| `PlayerMovement` | Hero movement, jumping, and gravity; button controls |
| `Player` | Grow/shrink, death, star invincibility, sounds |
| `PowerUp` | Coin, magic mushroom, star, 1UP |
| `BlockHit`, `BlockItem`, `BlockCoin` | Blocks with coins and items |
| `EntityMovement`, `Enemy`, `iphone`, `applew` | Enemy movement and behavior (shell, squashing) |
| `FlagPole` | Level finish: flag, walking animation, then fade to black |
| `SideScrollingCamera` | Camera that follows the player |
| `DeathBarrier`, `barierscript`, `FallDetection` | Death on falling |
| `MainMenu`, `SceneTransition`, `comploader` | Menu, scene transitions, splash screen |
| `coincounter` | On-screen counter (TextMeshPro) |

> And yes — among the enemy scripts you'll find `iphone` and `applew`. Don't ask. 😄

---

## 🛠️ Building from Source

1. Install **Unity Hub** and **Unity 2021.3.40f1**.
2. Open the project folder through Unity Hub.
3. Open the scene `Assets/Scenes/loadcomp.unity` and press **Play** — or build the game via `File → Build Settings` (Windows platform).
Either you may download zip file from folder archive

---

## ⚖️ License & Credits

The project is based on an open Super Mario tutorial for Unity. The original Super Mario Bros. characters and music belong to **Nintendo**; the project was created for educational and non-commercial purposes.
