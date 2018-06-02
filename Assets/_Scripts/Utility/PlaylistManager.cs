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
        public delegate void ChangeSongAction(AudioClip _clipToPlay);
        public static event ChangeSongAction OnChangeSong;

        [SerializeField] private List<AudioClip> songs;
        private Coroutine loopPlaylistCoroutine;

        private void OnEnable()
        {
            HomeScreenManager.OnRestartGame += PlayPlaylist;
            CollisionHandler.OnDeadlyCollision += StopPlaylist;
        }

        private void OnDisable()
        {
            HomeScreenManager.OnRestartGame -= PlayPlaylist;
            CollisionHandler.OnDeadlyCollision -= StopPlaylist;
        }

        private void ShuffleSongs()
        {
            for (int i = 0; i < songs.Count; i++)
            {
                var temp = songs[i];
                int randomIndex = Random.Range(i, songs.Count);
                songs[i] = songs[randomIndex];
                songs[randomIndex] = temp;
            }
        }

        private void PlayPlaylist()
        {
            ShuffleSongs();
            loopPlaylistCoroutine = StartCoroutine(LoopPlaylist());
        }

        private void StopPlaylist()
        {
            if(loopPlaylistCoroutine != null)
            {
                StopCoroutine(loopPlaylistCoroutine);
            }
        }

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

            if(DifficultyManager.IncreaseDifficulty != null)
            {
                DifficultyManager.IncreaseDifficulty();
            }
            loopPlaylistCoroutine = StartCoroutine(LoopPlaylist());
        }
    }
}