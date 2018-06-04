public static class SettingsController 
{
    public static void SetMute(bool b)
    {
        PlayerPrefHelper.SetBool("Music_Mute", b);
    }

    public static void SetVibration(bool b)
    {
        PlayerPrefHelper.SetBool("Vibration", b);
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
