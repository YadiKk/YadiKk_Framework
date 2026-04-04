using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace YadikkFramework
{
    /// <summary>
    /// Add this prefab to your first scene. It persists across all scenes.
    /// Manages loading screens, progress bars, and scene transitions.
    /// </summary>
    public class SceneLoader : SingletonPersistent<SceneLoader>
    {
        [Header("UI Components")]
        [SerializeField] private Image progressBar;
        [SerializeField] private CanvasGroup faderGroup;

        [Header("Settings")]
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private bool useFade = true;
        [SerializeField] private float minimumLoadTime = 0.5f;

        public event Action<string> OnLoadStart;
        public event Action<string> OnLoadComplete;

        private bool isLoading;

   

        private void Start()
        {
            if (faderGroup != null)
            {
                faderGroup.alpha = 0;
                faderGroup.gameObject.SetActive(true);
            }
            if (progressBar != null)
                progressBar.fillAmount = 0;
        }

        /// <summary>
        /// Call this from anywhere: SceneLoader.Instance.LoadScene("Level1");
        /// </summary>
        public void LoadScene(string sceneName)
        {
            if (isLoading) return;

            if (!IsSceneInBuild(sceneName))
            {
                Debug.LogError($"[YadikkFramework] Scene '{sceneName}' is missing in Build Settings!");
                return;
            }

            StartCoroutine(LoadAsyncRoutine(sceneName, LoadSceneMode.Single));
        }

        public void LoadSceneAdditive(string sceneName)
        {
            if (isLoading) return;

            if (!IsSceneInBuild(sceneName))
            {
                Debug.LogError($"[YadikkFramework] Scene '{sceneName}' is missing in Build Settings!");
                return;
            }

            StartCoroutine(LoadAsyncRoutine(sceneName, LoadSceneMode.Additive));
        }

        private IEnumerator LoadAsyncRoutine(string sceneName, LoadSceneMode mode)
        {
            isLoading = true;

            OnLoadStart?.Invoke(sceneName);

            float startTime = Time.unscaledTime;

            if (useFade)
                yield return FadeRoutine(1f);

            if (progressBar != null)
                progressBar.fillAmount = 0;

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, mode);
            op.allowSceneActivation = false;

            while (op.progress < 0.9f)
            {
                float progress = op.progress / 0.9f;

                if (progressBar != null)
                    progressBar.fillAmount = progress;

                yield return null;
            }

            if (progressBar != null)
                progressBar.fillAmount = 1f;

            float elapsed = Time.unscaledTime - startTime;
            if (elapsed < minimumLoadTime)
                yield return new WaitForSecondsRealtime(minimumLoadTime - elapsed);

            op.allowSceneActivation = true;

            while (!op.isDone)
                yield return null;

            if (useFade)
                yield return FadeRoutine(0f);

            OnLoadComplete?.Invoke(sceneName);

            isLoading = false;
        }

        private IEnumerator FadeRoutine(float target)
        {
            if (faderGroup == null)
                yield break;

            float startAlpha = faderGroup.alpha;
            float timer = 0;

            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                faderGroup.alpha = Mathf.Lerp(startAlpha, target, timer / fadeDuration);
                yield return null;
            }

            faderGroup.alpha = target;
        }

        private IEnumerator Fade(float targetAlpha)
        {
            if (faderGroup == null) yield break;

            float startAlpha = faderGroup.alpha;
            float timer = 0;

            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                faderGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
                yield return null;
            }
            faderGroup.alpha = targetAlpha;
        }

        private IEnumerator Fade(bool _active)
        {
            if (faderGroup == null) yield break;

            float startAlpha = faderGroup.alpha;
            float timer = 0;

            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                faderGroup.alpha = Mathf.Lerp(startAlpha, _active ? 1 : 0, timer / fadeDuration);
                yield return null;
            }
            faderGroup.alpha = _active ? 1 : 0;
        }

        private bool IsSceneInBuild(string name)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

                if (sceneName.Equals(name, StringComparison.Ordinal))
                    return true;
            }
            return false;
        }
    }
}