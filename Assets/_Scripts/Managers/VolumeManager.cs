using UnityEngine;
using UnityEngine.UI;
using RR.Audio;
using RR.Controllers;
using RR.Helpers;

namespace RR.Managers
{
    /// <summary>
    /// This class is responsible for managing the volume of the game and the volume of the music/sfx seperately.
    /// </summary>
    public class VolumeManager : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;

        /// <summary>
        /// Changes the master volume to the given value of the slider.
        /// </summary>
        public void ChangeMasterVolume()
        {
            if(SettingsController.GetMuteState())
            {
                SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(0));
            }
            else
            {
                SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(1));
            }
        }

        /// <summary>
        /// Changes the SFX volume to the given value of the slider.
        /// </summary>
        public void ChangeSFXVolume()
        {
            SFXManager.instance.ChangeGroupVolume("SFXVolume", SoundHelper.VolumeToDecibel(sfxVolumeSlider.value));
        }

        /// <summary>
        /// Changes the music volume to the given value of the slider.
        /// </summary>
        public void ChangeMusicVolume()
        {
            SFXManager.instance.ChangeGroupVolume("MusicVolume", SoundHelper.VolumeToDecibel(musicVolumeSlider.value));
        }
    }
}