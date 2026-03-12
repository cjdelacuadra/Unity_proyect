using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    /// <summary>
    /// Handles asynchronous scene loading with optional transition support.
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        /// <summary>
        /// Fired when a scene starts loading. Passes the scene name.
        /// </summary>
        public static event Action<string> OnSceneLoadStarted;

        /// <summary>
        /// Fired during scene loading with the progress value (0 to 1).
        /// </summary>
        public static event Action<float> OnSceneLoadProgress;

        /// <summary>
        /// Fired when a scene finishes loading. Passes the scene name.
        /// </summary>
        public static event Action<string> OnSceneLoadCompleted;

        private bool _isLoading = false;

        /// <summary>
        /// Whether a scene is currently being loaded.
        /// </summary>
        public bool IsLoading => _isLoading;

        /// <summary>
        /// Loads a scene asynchronously by name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="mode">The load scene mode (Single or Additive).</param>
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (_isLoading)
            {
                Debug.LogWarning("[SceneLoader] A scene is already being loaded.");
                return;
            }

            StartCoroutine(LoadSceneAsync(sceneName, mode));
        }

        /// <summary>
        /// Loads a scene by its build index.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene.</param>
        /// <param name="mode">The load scene mode.</param>
        public void LoadScene(int buildIndex, LoadSceneMode mode = LoadSceneMode.Single)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogError($"[SceneLoader] No scene found at build index {buildIndex}.");
                return;
            }

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            LoadScene(sceneName, mode);
        }

        /// <summary>
        /// Reloads the currently active scene.
        /// </summary>
        public void ReloadCurrentScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            LoadScene(currentScene);
        }

        private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
            _isLoading = true;
            OnSceneLoadStarted?.Invoke(sceneName);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                OnSceneLoadProgress?.Invoke(progress);

                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            _isLoading = false;
            OnSceneLoadCompleted?.Invoke(sceneName);
        }
    }
}
