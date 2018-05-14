using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

namespace Audio
{
    public class SFXManager : MonoBehaviour
    {
        //Instance of this script.
        private static SFXManager s_Instance = null;

        private AudioMixer masterAudioMixer;

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
        void OnApplicationQuit()
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

            masterAudioMixer = Resources.Load<AudioMixer>("Mixer");
        }

        public void ChangeGroupVolume(string groupVolumeName, float volumeValue)
        {
            masterAudioMixer.SetFloat(groupVolumeName, volumeValue);
        }
        
        /// <summary>
        /// Plays the given at default volume and pitch if no volume and pitch are given.
        /// </summary>
        /// <param name="clipToPlay">The clip that will be played.</param>
        /// <param name="volume">The volume at which the clip will be played.</param>
        /// <param name="pitch">The pitch at which the clip will be played.</param>
        public void PlaySound(AudioClip clipToPlay, float volume = 1f, float pitch = 1f)
        {
            StartCoroutine(SimultaneousSound(clipToPlay, volume, pitch));
        }

        /// <summary>
        /// Plays the given at default volume and pitch if no volume and pitch are given.
        /// Plays simultaneously with other sounds.
        /// </summary>
        /// <param name="clipToPlay">The clip that will be played.</param>
        /// <param name="volume">The volume at which the clip will be played.</param>
        /// <param name="pitch">The pitch at which the clip will be played.</param>
        private IEnumerator SimultaneousSound(AudioClip clipToPlay, float volume = 1f, float pitch = 1f)
        {
            AudioSource tempAS = gameObject.AddComponent<AudioSource>();
            tempAS.clip = clipToPlay;
            tempAS.volume = volume;
            tempAS.pitch = pitch;
            tempAS.outputAudioMixerGroup = masterAudioMixer.FindMatchingGroups("SFX")[0];//masterAudioMixer.outputAudioMixerGroup;
            tempAS.Play();

            yield return new WaitForSeconds(clipToPlay.length);
            Destroy(tempAS);
        }
    }
}