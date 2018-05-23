using System.Collections;
using UI;
using UnityEngine;
using Utility;

namespace Audio
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private LevelData currentLevelData;

        [SerializeField] private AudioSource mutedSource;
        [SerializeField] private AudioSource nonMutedSource;

        private Coroutine musicDelayCoroutine;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopMusic;
            RestartGameButton.OnRestartGame += StartMusic;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopMusic;
            RestartGameButton.OnRestartGame -= StartMusic;
        }

        void Start()
        {
            mutedSource.clip = currentLevelData.LevelAudio;
            nonMutedSource.clip = currentLevelData.LevelAudio;
        }

        /// <summary>
        /// Starts the music.
        /// </summary>
        private void StartMusic()
        {
            mutedSource.Play();
            musicDelayCoroutine = StartCoroutine(PlayMusicDelay());
        }

        /// <summary>
        /// Plays the actual music with a delay.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayMusicDelay()
        {
            yield return new WaitForSeconds(5f);
            nonMutedSource.Play();
        }

        /// <summary>
        /// Stops all the music from playing.
        /// </summary>
        private void StopMusic()
        {
            if (musicDelayCoroutine != null)
                StopCoroutine(musicDelayCoroutine);
            mutedSource.Stop();
            nonMutedSource.Stop();
        }
    }
}