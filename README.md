# Unity Project

A Unity game project with a clean, scalable architecture designed for team collaboration.

## Prerequisites

- **Unity Hub** (latest version): [Download Unity Hub](https://unity.com/download)
- **Unity Editor** (2022.3 LTS or later recommended)
- **Git** (for version control)

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/cjdelacuadra/Unity_proyect.git
   ```

2. **Open with Unity Hub:**
   - Launch Unity Hub
   - Click **Open** > **Add project from disk**
   - Select the cloned `Unity_proyect` folder
   - Unity will import all assets and generate the `Library/` folder (this may take a few minutes on first open)

3. **Open the main scene:**
   - In the Project window, navigate to `Assets/Scenes/`
   - Double-click `MainMenu.unity` or `GameScene.unity` to load a scene
   - Press the **Play** button to run the game in the Editor

## Project Structure

```
Assets/
├── Animations/          # Animation clips and controllers
├── Audio/
│   ├── Music/           # Background music tracks
│   └── SFX/             # Sound effects
├── Editor/              # Custom editor scripts and tools
├── Fonts/               # Font files (.ttf, .otf)
├── Materials/           # Material assets
├── Plugins/             # Third-party plugins and SDKs
├── Prefabs/
│   ├── Characters/      # Character prefabs
│   ├── Environment/     # Environment/level prefabs
│   └── UI/              # UI element prefabs
├── Resources/           # Assets loaded at runtime via Resources.Load()
├── Scenes/              # Unity scene files (.unity)
├── Scripts/
│   ├── Core/            # Singletons, managers, and core systems
│   ├── Gameplay/        # Game mechanics and player logic
│   ├── UI/              # UI controllers and views
│   └── Utilities/       # Helper classes and extensions
├── Shaders/             # Custom shader files
├── StreamingAssets/     # Assets accessible via file path at runtime
├── Textures/
│   ├── Characters/      # Character sprites and textures
│   ├── Environment/     # Environment textures
│   └── UI/              # UI sprites and icons
└── ThirdParty/          # Third-party assets (keep separate for clarity)
```

## Architecture Overview

The project uses a **Manager-based singleton architecture** for core systems:

| Manager | Responsibility |
|---------|---------------|
| `GameManager` | Game state, initialization, and lifecycle |
| `AudioManager` | Music and SFX playback |
| `UIManager` | UI panel management and transitions |
| `SceneLoader` | Async scene loading with transition support |

All managers inherit from a generic `Singleton<T>` base class that ensures a single instance persists across scenes.

### Key Design Principles

- **Single Responsibility:** Each script handles one concern
- **Loose Coupling:** Systems communicate through events, not direct references
- **Scalability:** New features are added as new scripts/managers without modifying existing ones
- **Prefab-driven:** Game objects are built as prefabs for easy reuse and iteration

## Build Instructions

1. Open the project in Unity
2. Go to **File** > **Build Settings**
3. Select your target platform (PC, Mac, Android, iOS, WebGL, etc.)
4. Click **Switch Platform** if needed
5. Click **Build** and choose an output directory

## Contributing

Please read [GUIDELINES.md](GUIDELINES.md) for coding standards, naming conventions, and asset guidelines before contributing.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
