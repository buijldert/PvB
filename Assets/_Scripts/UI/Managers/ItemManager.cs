using System;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour 
{
    public static ItemManager instance;

    [SerializeField] private Item[] items;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void Start()
    {
        UpdateItemEntries();
    }

    public void SetItemSelected(string key)
    {
        foreach(Item item in GetItemArray().Where(item => item.Key != key))
        {
            PlayerPrefHelper.SetBool(item.Key + "_Selected", false);
        }

        PlayerPrefHelper.SetBool(key + "_Selected", true);
        UpdateItemEntries();
    }

    private void UpdateItemEntries()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].Unlocked = PlayerPrefHelper.GetBool(items[i].Key);
            items[i].Selected = PlayerPrefHelper.GetBool(items[i].Key + "_Selected");
        }
    }

    public Item[] GetItemArray()
    {
        return items;
    }
}

[System.Serializable]
public struct Item
{
    public string Key;
    public bool Unlocked;
    public bool Selected;

    public void SetSelected(bool isSelected)
    {
        Selected = isSelected;
    }
}
