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

public static class PlayerPrefManager
{

    /// <summary>
    /// Example "level_01" + true
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="value">If set to <c>true</c> value.</param>
    public static void SetLevelUnlocked(string key, bool value)
    {
        PlayerPrefHelper.SetBool(key, value);
    }
}
