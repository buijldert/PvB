using UnityEngine;

[System.Serializable]
public struct ItemModel
{
    public string Key;
    public bool Unlocked;
    public bool Selected;
    public Texture ItemTexture;
    public string ItemName;

    public void SetSelected(bool isSelected)
    {
        Selected = isSelected;
    }
}
