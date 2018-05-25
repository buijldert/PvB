using UnityEngine;

namespace Environment
{
    /// <summary>
    /// This class is responsible for holding the leveldata of all levels.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/LevelDataHolder")]
    public class LevelDataHolder : ScriptableObject
    {
        public LevelData[] LevelData;
    }
}