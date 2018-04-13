using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public AudioClip Music;

        // TODO: remove this after real implementation
        public string DemoText;
    }
}