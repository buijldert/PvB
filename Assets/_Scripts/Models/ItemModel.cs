using UnityEngine;

namespace RR.Models
{
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