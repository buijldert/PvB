using UnityEngine;

namespace RR.Models
{
    /// <summary>
    /// This class is responsible for holding a shop item's data.
    /// </summary>
    [System.Serializable]
    public struct ItemModel
    {
        public string Key;
        public bool Unlocked;
        public bool Selected;
        public Texture ItemTexture;
        public string ItemName;
    }
}