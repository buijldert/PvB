using Environment;
using System.Collections;
using UI;
using UnityEngine;
using Utility;

namespace Audio
{
    /// <summary>
    /// This class is responsible for setting the current level through song and stopping/starting the background and spawning music.
    /// </summary>
    public class MusicController : MonoBehaviour
    {
        public delegate void AudioStartAction();
        public static event AudioStartAction OnAudioStart;

        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopMusic;
            PlaylistManager.OnChangeSong += ChangeAudio;
            PauseGameManager.OnPauseGame += PauseMusic;
            PauseGameManager.OnResumeGame += PlayMusic;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            PlaylistManager.OnChangeSong -= ChangeAudio;
            PauseGameManager.OnPauseGame -= PauseMusic;
            PauseGameManager.OnResumeGame -= PlayMusic;
        }

        private void ChangeAudio(AudioClip _clipToPlay)
        {
            audioSource.clip = _clipToPlay;
            PlayMusic();
        }

        /// <summary>
        /// Plays the background music.
        /// </summary>
        public void PlayMusic()
        {
            audioSource.Play();
            if (OnAudioStart != null)
            {
                OnAudioStart();
            }
        }

        public void PauseMusic()
        {
            audioSource.Pause();
        }

        /// <summary>
        /// Stops the music and music delay coroutine 
        /// </summary>
        private void StopMusic()
        {
            audioSource.Stop();
        }
    }
}