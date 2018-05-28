using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class VolumeManager : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;

        public void ChangeMasterVolume()
        {
            SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(masterVolumeSlider.value));
        }

        public void ChangeSFXVolume()
        {
            SFXManager.instance.ChangeGroupVolume("SFXVolume", SoundHelper.VolumeToDecibel(sfxVolumeSlider.value));
        }

        public void ChangeMusicVolume()
        {
            SFXManager.instance.ChangeGroupVolume("MusicVolume", SoundHelper.VolumeToDecibel(musicVolumeSlider.value));
        }
    }
}