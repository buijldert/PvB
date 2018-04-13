using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class LevelDataHolder : ScriptableObject
    {
        public LevelData[] Levels;
    }
}