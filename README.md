# 🌲 Little Red's Forest Escape

> A third-person fairy tale survival adventure built in Unity 6.

Little Red's Forest Escape is a student game project inspired by Little Red Riding Hood. The player guides Little Red through an increasingly dangerous magical forest — collecting crystals, avoiding hazards, and escaping the wolf to reach grandmother's house.

---

## 🎮 Gameplay

- Collect the required number of crystals in each level
- Avoid water, explosion barrels, and the wolf
- Reach the exit gate before your health runs out
- Three levels with escalating danger and atmosphere

| Level | Setting | New Threat |
|-------|---------|------------|
| Level 1 | Bright magical forest | Basic navigation and crystal collection |
| Level 2 | Dark swamp with broken paths | Water hazards and explosion barrels |
| Level 3 | Night forest | Wolf chase mechanic |

---

## 🕹️ Controls

| Action | Input |
|--------|-------|
| Move | W / A / S / D |
| Run | Left Shift (hold while moving) |
| Jump | Space |
| Pause | Pause button (on-screen) |

---

## 🛠️ Built With

- **Unity 6** (URP)
- **C#**
- **Unity Input System**
- **TextMesh Pro**

---

## 📁 Project Structure

```
Assets/
├── Scripts/          # All gameplay scripts
├── Scenes/           # Level scenes
├── Fantasy Forest Environment Free Sample/
└── Audio files       # .mp3 and .wav assets
```

---

## 📜 Scripts Overview

| Script | Responsibility |
|--------|---------------|
| `GameManager.cs` | Score, crystal count, level completion, game over |
| `AudioManager.cs` | Music and SFX playback across scenes |
| `PlayerMovement.cs` | Character movement and running |
| `PlayerHealth.cs` | Damage, hearts UI, game over trigger |
| `ExitZone.cs` | Crystal gate — blocks exit until requirement met |
| `WolfChase.cs` | Wolf proximity detection and player damage |
| `ExplodingBomb.cs` | Explosion trigger, damage, fire VFX |
| `LevelTimer.cs` | Timer count-up and best-time tracking |
| `PauseMenuController.cs` | Pause/resume flow |
| `SettingsManager.cs` | Volume sliders and mute, saved via PlayerPrefs |
| `StoryManager.cs` | Skippable story panel at level start |

---

## 🎵 Audio Credits

| File | Source |
|------|--------|
| `mainmenu.mp3`, `levels.wav` | [Mixkit](https://mixkit.co) — Free license |
| `mixkit-fairytale-game-over-1945.wav` | [Mixkit](https://mixkit.co) — Free license |
| `mixkit-wolves-at-scary-forest-2485.wav` | [Mixkit](https://mixkit.co) — Free license |
| `collect.wav`, `jump.mp3`, `landing.mp3` | [Pixabay](https://pixabay.com) — Royalty-free |
| `bomb.mp3`, `watersplash.mp3`, `damage.wav` | [Freesound](https://freesound.org) — CC license (see per-file) |
| `wolfhowl.mp3`, `attack.mp3` | [Freesound](https://freesound.org) — CC license (see per-file) |

---

## 🧊 3D Asset Credits

3D models and environment assets were sourced from [Sketchfab](https://sketchfab.com).  
Each model carries its own Creative Commons license — see individual model pages for attribution requirements.

---

## ⚙️ How to Run

1. Clone the repository
2. Open in **Unity 6** (URP project)
3. Open `Assets/Scenes/MainMenu` scene
4. Press Play or build for Windows

**Minimum requirements:** Windows 10, 4 GB RAM, DirectX 11 GPU

---

## 👩‍💻 Developer

**Hilal Aslan** — Unity & C# student project  
Document Version: 1.0
