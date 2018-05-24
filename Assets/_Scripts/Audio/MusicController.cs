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

        public void SetCurrentLevelData(LevelData levelData)
        {
            currentLevelData = levelData;
        }

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
        /// Plays the background music.
        /// </summary>
        private void StartMusic()
        {
            mutedSource.Play();
            musicDelayCoroutine = StartCoroutine(PlayMusicDelay());
        }

        /// <summary>
        /// Plays the real music at a delay.
        /// </summary>
        private IEnumerator PlayMusicDelay()
        {
            yield return new WaitForSeconds(6f);
            nonMutedSource.Play();
        }

        /// <summary>
        /// Stops the music and music delay coroutine 
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