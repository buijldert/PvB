using UnityEngine;
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
        public static VolumeManager instance;

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
        /// Changes the master volume to the given value of the slider.
        /// </summary>
        public void ChangeMasterVolume()
        {
            if(!SettingsController.GetMuteState())
            {
                SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(0));
            }
            else
            {
                SFXManager.instance.ChangeGroupVolume("MasterVolume", SoundHelper.VolumeToDecibel(1));
            }
        }
    }
}