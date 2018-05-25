using UnityEngine;

namespace Environment
{
    /// <summary>
    /// This class is responsible for holding the level data of one level.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public AudioClip LevelAudio;
    }
}