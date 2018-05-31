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

        [SerializeField] private AudioSource mutedSource;
        [SerializeField] private AudioSource nonMutedSource;

        private Coroutine musicDelayCoroutine;

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
            mutedSource.clip = _clipToPlay;
            nonMutedSource.clip = _clipToPlay;
            StartMusic();
        }

        /// <summary>
        /// Plays the background music.
        /// </summary>
        private void StartMusic()
        {
            musicDelayCoroutine = StartCoroutine(PlayMusicDelay());
        }

        /// <summary>
        /// Plays the real music at a delay.
        /// </summary>
        private IEnumerator PlayMusicDelay()
        {
            mutedSource.Play();
            if (OnAudioStart != null)
            {
                OnAudioStart();
            }
            yield return new WaitForSeconds(6f);
            nonMutedSource.Play();
        }

        /// <summary>
        /// Stops the music and music delay coroutine 
        /// </summary>
        private void StopMusic()
        {
            if (musicDelayCoroutine != null)
            {
                StopCoroutine(musicDelayCoroutine);
            }
            mutedSource.Stop();
            nonMutedSource.Stop();
        }
    }
}