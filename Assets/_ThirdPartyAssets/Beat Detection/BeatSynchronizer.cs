using System;
using UnityEngine;
using RR.Controllers;

namespace Audio
{
    /// <summary>
    /// This class should be attached to the audio source for which synchronization should occur, and is 
    /// responsible for synching up the beginning of the audio clip with all active beat counters and pattern counters.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BeatSynchronizer : MonoBehaviour
    {

        public float bpm = 120f;        // Tempo in beats per minute of the audio clip.
        public float startDelay = 0f;   // Number of seconds to delay the start of audio playback.

        public static Action<double> OnAudioStart;

        private void OnEnable()
        {
            MusicController.OnAudioStart += StartBeatCheck;
        }

        private void OnDisable()
        {
            MusicController.OnAudioStart -= StartBeatCheck;
        }

        private void StartBeatCheck()
        {
            double initTime = AudioSettings.dspTime;
            if (OnAudioStart != null)
            {
                OnAudioStart(initTime + startDelay);
            }
        }
    }
}