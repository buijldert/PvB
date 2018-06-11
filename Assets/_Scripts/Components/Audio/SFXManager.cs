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

        /// <summary>
        /// Awake() is called before Start() and OnEnable().
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }

            instance = this;
        }

        /// <summary>
        /// Changes the volume in decibels of the given exposed parameter.
        /// </summary>
        /// <param name="_groupVolumeName">The name of the exposed volume parameter.</param>
        /// <param name="_decibelValue">The value in decibels that the given exposed parameter will be changed to.</param>
        public void ChangeGroupVolume(string _groupVolumeName, float _decibelValue)
        {
            masterMixer.SetFloat(_groupVolumeName, _decibelValue);
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
        private IEnumerator SimultaneousSound(AudioClip _clipToPlay, float _volume, float _pitch)
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