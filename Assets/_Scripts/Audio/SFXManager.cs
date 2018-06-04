using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

namespace Audio
{
    /// <summary>
    /// This class is responsible for playing sound FX when needed and changing the volume of different audiomixer channels.
    /// </summary>
    public class SFXManager : MonoBehaviour
    {
        private static SFXManager s_Instance = null;

        [SerializeField] private AudioMixer masterMixer;

        [SerializeField] private AudioMixerGroup outputMixerGroup;

        /// <summary>
        /// Instantiates a new SoundManager if one cannot be found.
        /// </summary>
        public static SFXManager instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(SFXManager)) as SFXManager;
                }


                if (s_Instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    s_Instance = obj.AddComponent(typeof(SFXManager)) as SFXManager;
                }

                return s_Instance;
            }

            set { }
        }

        /// <summary>
        /// Removes the instance of the SoundManager 
        /// </summary>
        private void OnApplicationQuit()
        {
            s_Instance = null;
        }

        /// <summary>
        /// Loads the sound clips from the Resources/Audio folder so they can be used.
        /// Also makes sure the sound manager persists through every scene.
        /// </summary>
        private void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
            }

            s_Instance = this;
            DontDestroyOnLoad(gameObject);
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