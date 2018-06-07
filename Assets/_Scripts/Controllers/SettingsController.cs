using RR.Helpers;

namespace RR.Controllers
{
    public static class SettingsController
    {
        public static void SetMute(bool _value)
        {
            PlayerPrefHelper.SetBool("Music_Mute", _value);
        }

        public static void SetVibration(bool _value)
        {
            PlayerPrefHelper.SetBool("Vibration", _value);
        }

        public static bool GetVibrationState()
        {
            return PlayerPrefHelper.GetBool("Vibration");
        }

        public static bool GetMuteState()
        {
            return PlayerPrefHelper.GetBool("Music_Mute");
        }
    } 
}