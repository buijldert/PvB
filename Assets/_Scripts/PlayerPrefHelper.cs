using UnityEngine;

public static class PlayerPrefHelper 
{
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}

