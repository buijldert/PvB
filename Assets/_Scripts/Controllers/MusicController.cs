using System;
using UnityEngine;
using RR.Handlers;
using RR.Managers;

namespace RR.Controllers
{
    /// <summary>
    /// This class is responsible for setting the current level through song and stopping/starting the background and spawning music.
    /// </summary>
    public class MusicController : MonoBehaviour
    {
        public static Action OnAudioStart;

        [SerializeField] private AudioSource audioSource;

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopMusic;
            PlaylistManager.OnChangeSong += ChangeAudio;

            GameController.OnPauseGame += PauseMusic;
            GameController.OnResumeGame += PlayMusic;
            GameController.OnStopGame += StopMusic;
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            PlaylistManager.OnChangeSong -= ChangeAudio;

            GameController.OnPauseGame -= PauseMusic;
            GameController.OnResumeGame -= PlayMusic;
            GameController.OnStopGame -= StopMusic;
        }

        /// <summary>
        /// Changes the song playing.
        /// </summary>
        /// <param name="_clipToPlay">The song that the will be changed to.</param>
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