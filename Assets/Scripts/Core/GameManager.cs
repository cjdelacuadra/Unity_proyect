using System;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Central game manager that controls game state and lifecycle.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// Represents the possible states of the game.
        /// </summary>
        public enum GameState
        {
            MainMenu,
            Loading,
            Playing,
            Paused,
            GameOver
        }

        /// <summary>
        /// Fired when the game state changes. Passes the new state.
        /// </summary>
        public static event Action<GameState> OnGameStateChanged;

        private GameState _currentState;

        /// <summary>
        /// The current state of the game.
        /// </summary>
        public GameState CurrentState
        {
            get => _currentState;
            private set
            {
                if (_currentState == value) return;
                _currentState = value;
                OnGameStateChanged?.Invoke(_currentState);
                Debug.Log($"[GameManager] State changed to: {_currentState}");
            }
        }

        protected override void OnInitialize()
        {
            CurrentState = GameState.MainMenu;
        }

        /// <summary>
        /// Transitions the game to the specified state.
        /// </summary>
        /// <param name="newState">The target game state.</param>
        public void SetState(GameState newState)
        {
            CurrentState = newState;
        }

        /// <summary>
        /// Pauses or unpauses the game.
        /// </summary>
        /// <param name="pause">True to pause, false to resume.</param>
        public void TogglePause(bool pause)
        {
            if (pause && CurrentState == GameState.Playing)
            {
                Time.timeScale = 0f;
                CurrentState = GameState.Paused;
            }
            else if (!pause && CurrentState == GameState.Paused)
            {
                Time.timeScale = 1f;
                CurrentState = GameState.Playing;
            }
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("[GameManager] Quitting game...");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
