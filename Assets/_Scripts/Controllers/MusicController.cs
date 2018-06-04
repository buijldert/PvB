using Environment;
using System.Collections;
using UI;
using UnityEngine;
using Utility;
using UI.Managers;
using System;

namespace Audio
{
    /// <summary>
    /// This class is responsible for setting the current level through song and stopping/starting the background and spawning music.
    /// </summary>
    public class MusicController : MonoBehaviour
    {
        public static Action OnAudioStart;

        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopMusic;
            PlaylistManager.OnChangeSong += ChangeAudio;
            PauseScreenManager.onQuit += StopMusic;

            GameController.OnPauseGame += PauseMusic;
            GameController.OnResumeGame += PlayMusic;
            GameController.OnStopGame += StopMusic;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            PlaylistManager.OnChangeSong -= ChangeAudio;

            GameController.OnPauseGame -= PauseMusic;
            GameController.OnResumeGame -= PlayMusic;
            GameController.OnStopGame -= StopMusic;
        }

        private void ChangeAudio(AudioClip _clipToPlay)
        {
            audioSource.clip = _clipToPlay;
            PlayMusic();
        }

        /// <summary>
        /// Plays the background music.
        /// </summary>
        private void PlayMusic()
        {
            audioSource.Play();
            if (OnAudioStart != null)
            {
                OnAudioStart();
            }
        }

        /// <summary>
        /// Pauses the music.
        /// </summary>
        private void PauseMusic()
        {
            audioSource.Pause();
        }

        /// <summary>
        /// Stops the music. 
        /// </summary>
        private void StopMusic()
        {
            audioSource.Stop();
        }
    }
}