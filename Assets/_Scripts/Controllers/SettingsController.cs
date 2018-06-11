using RR.Helpers;

namespace RR.Controllers
{
    /// <summary>
    /// This class is responsible for handling the in game settings.
    /// </summary>
    public static class SettingsController
    {
        /// <summary>
        /// Sets the sound to mute active or inactive.
        /// </summary>
        /// <param name="_value">Whether the sound will be muted.</param>
        public static void SetMute(bool _value)
        {
            PlayerPrefHelper.SetBool("Music_Mute", _value);
        }

        /// <summary>
        /// Sets the vibration to active or inactive.
        /// </summary>
        /// <param name="_value"></param>
        public static void SetVibration(bool _value)
        {
            PlayerPrefHelper.SetBool("Vibration", _value);
        }

        /// <summary>
        /// Gets the vibration state so it will stay saved for the player.
        /// </summary>
        /// <returns>Returns the vibration state.</returns>
        public static bool GetVibrationState()
        {
            return PlayerPrefHelper.GetBool("Vibration");
        }

        /// <summary>
        /// Gets the muted state so it will stay saved for the player.
        /// </summary>
        /// <returns>Returns the muted state.</returns>
        public static bool GetMuteState()
        {
            return PlayerPrefHelper.GetBool("Music_Mute");
        }
    } 
}