using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// Manages all audio playback including music and sound effects.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("Settings")]
        [SerializeField] [Range(0f, 1f)] private float _musicVolume = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float _sfxVolume = 1f;

        /// <summary>
        /// Current music volume (0 to 1).
        /// </summary>
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = Mathf.Clamp01(value);
                if (_musicSource != null)
                    _musicSource.volume = _musicVolume;
            }
        }

        /// <summary>
        /// Current SFX volume (0 to 1).
        /// </summary>
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = Mathf.Clamp01(value);
                if (_sfxSource != null)
                    _sfxSource.volume = _sfxVolume;
            }
        }

        protected override void OnInitialize()
        {
            EnsureAudioSources();
        }

        /// <summary>
        /// Plays a music clip, optionally looping.
        /// </summary>
        /// <param name="clip">The music clip to play.</param>
        /// <param name="loop">Whether to loop the clip.</param>
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip == null) return;

            EnsureAudioSources();
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.volume = _musicVolume;
            _musicSource.Play();
        }

        /// <summary>
        /// Stops the currently playing music.
        /// </summary>
        public void StopMusic()
        {
            if (_musicSource != null && _musicSource.isPlaying)
                _musicSource.Stop();
        }

        /// <summary>
        /// Plays a one-shot sound effect.
        /// </summary>
        /// <param name="clip">The sound effect clip to play.</param>
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;

            EnsureAudioSources();
            _sfxSource.PlayOneShot(clip, _sfxVolume);
        }

        /// <summary>
        /// Plays a sound effect at a specific world position.
        /// </summary>
        /// <param name="clip">The sound effect clip to play.</param>
        /// <param name="position">The world position to play the sound at.</param>
        public void PlaySFXAtPosition(AudioClip clip, Vector3 position)
        {
            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, position, _sfxVolume);
        }

        private void EnsureAudioSources()
        {
            if (_musicSource == null)
            {
                var musicObj = new GameObject("MusicSource");
                musicObj.transform.SetParent(transform);
                _musicSource = musicObj.AddComponent<AudioSource>();
                _musicSource.playOnAwake = false;
            }

            if (_sfxSource == null)
            {
                var sfxObj = new GameObject("SFXSource");
                sfxObj.transform.SetParent(transform);
                _sfxSource = sfxObj.AddComponent<AudioSource>();
                _sfxSource.playOnAwake = false;
            }
        }
    }
}
