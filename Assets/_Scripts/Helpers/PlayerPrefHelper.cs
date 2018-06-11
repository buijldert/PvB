namespace RR.Helpers
{
    /// <summary>
    /// This class is responsible for converting 0s and 1s to true and false for the playerprefs.
    /// </summary>
    public static class PlayerPrefHelper
    {
        /// <summary>
        /// Method to allow booleans to be set in PlayerPrefs. It really is just a cast to a value either 1 or 0.
        /// </summary>
        /// <param name="_key">Key we want to change the value of.</param>
        /// <param name="_value">If set to <c>true</c> value is 1.</param>
        public static void SetBool(string _key, bool _value)
        {
            UnityEngine.PlayerPrefs.SetInt(_key, _value ? 1 : 0);
        }

        /// <summary>
        /// Method to get a boolean back from PlayerPrefs.
        /// </summary>
        /// <returns><c>true</c> if bool was gotten, <c>false</c> otherwise.</returns>
        /// <param name="_key">Key.</param>
        public static bool GetBool(string _key)
        {
            return UnityEngine.PlayerPrefs.GetInt(_key) == 1 ? true : false;
        }
    } 
}
