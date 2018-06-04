using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.Managers;
using UnityEngine;


namespace Utility
{ 
    /// <summary>
    /// This class is responsible for managing the playlist that will dictate the level.
    /// </summary>
    public class PlaylistManager : MonoBehaviour
    {
        public static Action<AudioClip> OnChangeSong;

        [SerializeField] private List<AudioClip> songs;
        private Coroutine loopPlaylistCoroutine;

        private void OnEnable()
        {
            CollisionHandler.OnDeadlyCollision += StopPlaylist;
            GameController.OnStartGame += PlayPlaylist;
            GameController.OnStopGame += StopPlaylist;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDeadlyCollision -= StopPlaylist;
            GameController.OnStartGame -= PlayPlaylist;
            GameController.OnStopGame -= StopPlaylist;
        }

        /// <summary>
        /// Shuffles the list of the songs.
        /// </summary>
        private void ShuffleSongs()
        {
            for (int i = 0; i < songs.Count; i++)
            {
                var temp = songs[i];
                int randomIndex = UnityEngine.Random.Range(i, songs.Count);
                songs[i] = songs[randomIndex];
                songs[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Starts the playlist.
        /// </summary>
        private void PlayPlaylist()
        {
            ShuffleSongs();
            loopPlaylistCoroutine = StartCoroutine(LoopPlaylist());
        }

        /// <summary>
        /// Loops through the playlist and reshuffles and restarts it after going through all the songs.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoopPlaylist()
        {
            for (int i = 0; i < songs.Count; i++)
            {
                if (OnChangeSong != null)
                {
                    OnChangeSong(songs[i]);
                }
                yield return new WaitForSeconds(songs[i].length + 5f);
            }
            ShuffleSongs();

            if (DifficultyManager.IncreaseDifficulty != null)
            {
                DifficultyManager.IncreaseDifficulty();
            }
            loopPlaylistCoroutine = StartCoroutine(LoopPlaylist());
        }

        /// <summary>
        /// Stops the playlist.
        /// </summary>
        private void StopPlaylist()
        {
            if(loopPlaylistCoroutine != null)
            {
                StopCoroutine(loopPlaylistCoroutine);
            }
        }

        
    }
}