using UnityEngine;

public class ItemManager : MonoBehaviour 
{
    [SerializeField] private Item[] items;

    public void Start()
    {
        //PlayerPrefs.DeleteAll();

        //for (int i = 0; i < items.Length; i++)
        //{
        //    PlayerPrefHelper.SetBool(items[i].Key, items[i].Unlocked);
        //}

        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("AAAHHH: " + items[i].Key + " - "+ PlayerPrefHelper.GetBool(items[i].Key));
        }
    }
}

[System.Serializable]
public struct Item
{
    public string Key;
    public bool Unlocked;
}
