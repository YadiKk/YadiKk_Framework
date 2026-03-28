using System;
using System.Collections.Generic;
using UnityEngine;

namespace YadikkFramework
{
    /// <summary>
    /// Manages background music and sound effects separately.
    /// Improved inspector organization for better developer experience.
    /// </summary>
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        #region Serialized Fields
        [Header("Audio Sources")]
        [Tooltip("The source that will handle background music.")]
        [SerializeField] private AudioSource musicSource;
        [Tooltip("The source that will handle short sound effects.")]
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Databases")]
        [SerializeField] private List<MusicSound> musicLibrary = new List<MusicSound>();
        [SerializeField] private List<SFXSound> sfxLibrary = new List<SFXSound>();
        #endregion

        #region Private Variables
        private Dictionary<string, MusicSound> musicDictionary = new Dictionary<string, MusicSound>();
        private Dictionary<string, SFXSound> sfxDictionary = new Dictionary<string, SFXSound>();
        #endregion

        #region Initialization
        private void Start()
        {
            InitializeDatabases();

            if (sfxSource != null)
                sfxSource.loop = false;

        }

        private void InitializeDatabases()
        {
            // Initialize Music
            foreach (var m in musicLibrary)
            {
                if (m == null || string.IsNullOrEmpty(m.SoundName)) continue;
                if (!musicDictionary.ContainsKey(m.SoundName))
                    musicDictionary.Add(m.SoundName, m);
            }

            // Initialize SFX
            foreach (var s in sfxLibrary)
            {
                if (s == null || string.IsNullOrEmpty(s.SoundName)) continue;
                if (!sfxDictionary.ContainsKey(s.SoundName))
                    sfxDictionary.Add(s.SoundName, s);
            }
        }
        #endregion

        #region Public Play Methods
        /// <summary>
        /// Plays background music from the music library.
        /// </summary>
        public void PlayMusic(string musicName, bool loop = true)
        {
            if (musicDictionary.TryGetValue(musicName, out MusicSound m))
            {
                musicSource.clip = m.SoundRef;
                musicSource.volume = m.Volume;
                musicSource.loop = loop;
                musicSource.Play();
            }
            else
            {
                Debug.LogError($"Music: {musicName} not found in Music Library!");
            }
        }

        /// <summary>
        /// Plays a sound effect from the SFX library with pitch variation options.
        /// </summary>
        public void PlaySFX(string sfxName)
        {
            if (sfxDictionary.TryGetValue(sfxName, out SFXSound s))
            {
                sfxSource.pitch = s.UseRandomPitch
                    ? UnityEngine.Random.Range(s.MinPitch, s.MaxPitch)
                    : 1f;

                sfxSource.PlayOneShot(s.SoundRef, s.Volume);
            }
            else
            {
                Debug.LogError($"SFX: {sfxName} not found in SFX Library!");
            }
        }

        /// <summary>
        /// Immediately stops the music source.
        /// </summary>
        public void StopMusic()
        {
            if (musicSource != null && musicSource.isPlaying)
                musicSource.Stop();
        }
        #endregion
    }

    #region Data Structures
    [Serializable]
    public abstract class BaseSound
    {
        public string SoundName;
        public AudioClip SoundRef;
        [Range(0f, 1f)] public float Volume = 1f;
    }

    [Serializable]
    public class MusicSound : BaseSound
    {
        // Music specific data can be added here (e.g., Intro time, Fade duration)
        [Tooltip("Check if this music should bypass listener effects.")]
        public bool BypassEffects = false;
    }

    [Serializable]
    public class SFXSound : BaseSound
    {
        [Header("Pitch Settings")]
        public bool UseRandomPitch = false;
        [Range(0.1f, 3f)] public float MinPitch = 0.85f;
        [Range(0.1f, 3f)] public float MaxPitch = 1.15f;
    }
    #endregion
}