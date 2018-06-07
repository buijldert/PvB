using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace RR.Audio
{
    /// <summary>
    /// This class is responsible for playing sound FX when needed and changing the volume of different audiomixer channels.
    /// </summary>
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager instance;

        [SerializeField] private AudioMixer masterMixer;

        [SerializeField] private AudioMixerGroup outputMixerGroup;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
<<<<<<< HEAD
            instance = this;

            masterAudioMixer = Resources.Load<AudioMixer>("Mixer");
=======

            s_Instance = this;
            DontDestroyOnLoad(gameObject);
>>>>>>> Development_branch
        }


        public void ChangeGroupVolume(string _groupVolumeName, float _volumeValue)
        {
            masterMixer.SetFloat(_groupVolumeName, _volumeValue);
        }
        
        /// <summary>
        /// Plays the given at default volume and pitch if no volume and pitch are given.
        /// </summary>
        /// <param name="_clipToPlay">The clip that will be played.</param>
        /// <param name="_volume">The volume at which the clip will be played.</param>
        /// <param name="_pitch">The pitch at which the clip will be played.</param>
        public void PlaySound(AudioClip _clipToPlay, float _volume = 1f, float _pitch = 1f)
        {
            StartCoroutine(SimultaneousSound(_clipToPlay, _volume, _pitch));
        }

        /// <summary>
        /// Plays the given at default volume and pitch if no volume and pitch are given.
        /// Plays simultaneously with other sounds.
        /// </summary>
        /// <param name="_clipToPlay">The clip that will be played.</param>
        /// <param name="_volume">The volume at which the clip will be played.</param>
        /// <param name="_pitch">The pitch at which the clip will be played.</param>
        private IEnumerator SimultaneousSound(AudioClip _clipToPlay, float _volume = 1f, float _pitch = 1f)
        {
            AudioSource tempAS = gameObject.AddComponent<AudioSource>();
            tempAS.clip = _clipToPlay;
            tempAS.volume = _volume;
            tempAS.pitch = _pitch;
            tempAS.outputAudioMixerGroup = outputMixerGroup;
            tempAS.Play();

            yield return new WaitForSeconds(_clipToPlay.length);
            Destroy(tempAS);
        }
    }
}