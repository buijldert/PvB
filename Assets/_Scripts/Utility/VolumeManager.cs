using Audio;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Volumes")]
        [Range(0, 100)]
        [SerializeField] private int masterVolume;
        [Range(0, 100)]
        [SerializeField] private int sfxVolume;
        [Range(0, 100)]
        [SerializeField] private int musicVolume;

        public void ChangeMasterVolume()
        {
            SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(masterVolumeSlider.value));
        }
    }
}