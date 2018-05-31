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
            //RestartGameButton.OnRestartGame += StartMusic;
            PlaylistManager.OnChangeSong += ChangeAudio;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            //RestartGameButton.OnRestartGame -= StartMusic;
            PlaylistManager.OnChangeSong -= ChangeAudio;
        }

        private void ChangeAudio(AudioClip _clipToPlay)
        {
            audioSource.clip = _clipToPlay;
            StartMusic();
        }

        /// <summary>
        /// Plays the background music.
        /// </summary>
        private void StartMusic()
        {
            audioSource.Play();
            if (OnAudioStart != null)
            {
                OnAudioStart();
            }
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