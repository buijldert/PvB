using System.Collections;
using UI;
using UnityEngine;
using Utility;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        private AudioSource mutedSource;
        [SerializeField]private AudioSource nonMutedSource;

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
            mutedSource = GetComponent<AudioSource>();
        }

        private void StartMusic()
        {
            mutedSource.Play();
            StartCoroutine(PlayMusicDelay());
        }

        private IEnumerator PlayMusicDelay()
        {
            yield return new WaitForSeconds(5f);
            nonMutedSource.Play();
        }

        private void StopMusic()
        {
            mutedSource.Stop();
            nonMutedSource.Stop();
        }
    }
}