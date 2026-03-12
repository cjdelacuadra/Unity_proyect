# Project Guidelines

Standards and conventions for scripts, assets, and collaboration.

---

## Table of Contents

1. [Coding Standards](#coding-standards)
2. [Naming Conventions](#naming-conventions)
3. [Folder and Asset Guidelines](#folder-and-asset-guidelines)
4. [Scene Guidelines](#scene-guidelines)
5. [Prefab Guidelines](#prefab-guidelines)
6. [Git Workflow](#git-workflow)

---

## Coding Standards

### General

- Use **C#** for all scripts
- Follow **C# Microsoft conventions** for formatting and style
- One class per file; filename must match class name exactly
- Use `#region` blocks sparingly and only for large files
- Keep methods short (ideally < 30 lines)
- Prefer composition over inheritance

### Namespaces

Organize scripts under project namespaces:

```csharp
namespace Game.Core       // Core systems, managers, singletons
namespace Game.Gameplay   // Player, enemies, game mechanics
namespace Game.UI         // UI controllers, views, HUD
namespace Game.Utilities  // Helpers, extensions, data classes
```

### MonoBehaviour Lifecycle

Follow this order for Unity callback methods:

```csharp
Awake()
OnEnable()
Start()
Update()
FixedUpdate()
LateUpdate()
OnDisable()
OnDestroy()
```

### Access Modifiers

- Use `private` by default; expose only what is needed
- Use `[SerializeField]` for fields that need Inspector access but should remain private
- Avoid `public` fields; use properties with `{ get; private set; }` instead

```csharp
// Preferred
[SerializeField] private float moveSpeed = 5f;

// Avoid
public float moveSpeed = 5f;
```

### Events and Communication

- Use C# `event` and `Action` delegates for inter-system communication
- Avoid `FindObjectOfType()` and `GameObject.Find()` at runtime
- Reference dependencies via the Inspector or a service locator pattern

```csharp
public static event Action<int> OnScoreChanged;

// Raise event
OnScoreChanged?.Invoke(newScore);
```

### Comments

- Use XML comments (`///`) for public methods and properties
- Use inline comments only when logic is non-obvious
- Do not comment out dead code; remove it (git preserves history)

---

## Naming Conventions

### Scripts (C#)

| Element | Convention | Example |
|---------|-----------|---------|
| Classes | PascalCase | `PlayerController` |
| Interfaces | IPascalCase | `IDamageable` |
| Public methods | PascalCase | `TakeDamage()` |
| Private methods | PascalCase | `CalculateHealth()` |
| Public properties | PascalCase | `Health { get; set; }` |
| Private fields | camelCase with underscore prefix | `_currentHealth` |
| Parameters | camelCase | `damageAmount` |
| Constants | UPPER_SNAKE_CASE | `MAX_HEALTH` |
| Enums | PascalCase (type and values) | `GameState.Playing` |
| Events | On + PascalCase | `OnPlayerDied` |

### Assets

| Asset Type | Convention | Example |
|-----------|-----------|---------|
| Scenes | PascalCase | `MainMenu.unity` |
| Prefabs | PascalCase | `EnemyGoblin.prefab` |
| Materials | PascalCase with suffix | `WoodFloor_Mat.mat` |
| Textures | PascalCase with type suffix | `WoodFloor_Diffuse.png` |
| Animations | PascalCase with action | `PlayerRun.anim` |
| Audio clips | PascalCase with type prefix | `SFX_Explosion.wav` |
| Shaders | PascalCase | `ToonShading.shader` |
| ScriptableObjects | PascalCase with SO suffix | `WeaponData_SO.asset` |

### Texture Suffixes

| Suffix | Map Type |
|--------|----------|
| `_Diffuse` | Diffuse / Albedo |
| `_Normal` | Normal map |
| `_Roughness` | Roughness map |
| `_Metallic` | Metallic map |
| `_AO` | Ambient Occlusion |
| `_Emission` | Emission map |
| `_Height` | Height / Displacement |

---

## Folder and Asset Guidelines

### Folder Rules

- **Never** put assets in the root `Assets/` folder; always use a subfolder
- Keep third-party assets in `Assets/ThirdParty/` to separate them from project code
- Use `Assets/Resources/` only for assets that must be loaded at runtime via `Resources.Load()`; prefer Addressables for larger projects
- Place editor-only scripts in `Assets/Editor/` so they are excluded from builds

### Import Settings

- **Textures:** Set appropriate max size (512, 1024, 2048) based on usage. Use compression (ASTC for mobile, DXT/BC for desktop)
- **Audio:** Use `.ogg` or `.mp3` for music, `.wav` for short SFX. Set compression based on clip length
- **Models:** Ensure scale factor is set correctly (usually 1). Strip unused blend shapes and animations on import

### Asset Bundles / Addressables

- For larger projects, use the **Addressable Asset System** instead of `Resources/`
- Group addressables by scene or feature for efficient loading

---

## Scene Guidelines

- Each scene should have a clear purpose (e.g., `MainMenu`, `GameLevel01`, `Loading`)
- Use a consistent hierarchy structure in every scene:

```
Scene Root
├── --- MANAGERS ---
│   ├── GameManager
│   ├── AudioManager
│   └── UIManager
├── --- ENVIRONMENT ---
│   ├── Terrain
│   ├── Props
│   └── Lighting
├── --- CHARACTERS ---
│   ├── Player
│   └── Enemies
├── --- UI ---
│   └── Canvas
└── --- CAMERAS ---
    └── Main Camera
```

- Use **separator GameObjects** (named `--- SECTION ---`) to visually organize the hierarchy
- Keep scenes lightweight; use prefabs for reusable elements

---

## Prefab Guidelines

- Build gameplay objects as prefabs so they can be reused and iterated on independently
- Use **Prefab Variants** when you need a modified version of an existing prefab
- Avoid nested prefab overrides more than 2 levels deep
- Prefabs should be self-contained: they should work when dragged into any scene
- Store prefabs in appropriate subfolders (`Prefabs/Characters/`, `Prefabs/UI/`, etc.)

---

## Git Workflow

### Branches

- `main` — stable, release-ready code
- `develop` — integration branch for features
- `feature/<name>` — individual feature branches
- `bugfix/<name>` — bug fix branches
- `hotfix/<name>` — urgent production fixes

### Commits

- Write clear, descriptive commit messages
- Use present tense: `Add player movement script` (not `Added...`)
- Keep commits focused: one logical change per commit

### Unity-Specific Git Tips

- **Force Text Serialization:** Ensure `Edit > Project Settings > Editor > Asset Serialization` is set to **Force Text**
- **Visible Meta Files:** Ensure `Edit > Project Settings > Editor > Version Control Mode` is set to **Visible Meta Files**
- **Scene Merging:** Use Unity's YAML merge tool (`UnityYAMLMerge`) for resolving scene and prefab conflicts
- **Never commit** `Library/`, `Temp/`, `Build/`, `Logs/`, or `UserSettings/` folders

### Pull Requests

- Reference the related issue or feature in the PR description
- Include screenshots or GIFs for visual changes
- Request review from at least one team member before merging
