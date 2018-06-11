using System.Linq;
using UnityEngine;
using RR.Helpers;
using RR.Models;
using RR.UI.Managers;

namespace RR.Managers
{
    /// <summary>
    /// This class is responsible for handling the items in the shop.
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager instance;

        [SerializeField] private ItemModel[] items;

        /// <summary>
        /// Subscribes to the events we want to use.
        /// </summary>
        private void OnEnable()
        {
            CodeScreenManager.onNewCodeUsed += UpdateItemEntries;
        }

        /// <summary>
        /// Singleton Implementation
        /// Also calls the UpdateEntries function.
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            if(!PlayerPrefs.HasKey("FirstStartUpHappend"))
            {
                PlayerPrefHelper.SetBool("FirstStartUpHappend", true);

                for (int i = 0; i < items.Length; i++)
                {
                    PlayerPrefHelper.SetBool(items[i].Key, false);
                    PlayerPrefHelper.SetBool(items[i].Key + "_Selected", false);
                    PlayerPrefHelper.SetBool("Music_Mute", false);
                }
            }
            else
            {
                UpdateItemEntries(); 
            }
        }

        /// <summary>
        /// Sets the item as selected in the playerprefs.
        /// </summary>
        /// <param name="_key">Key we want to change the value of.</param>
        public void SetItemSelected(string _key)
        {
            foreach (ItemModel item in GetItemArray().Where(item => item.Key != _key))
            {
                PlayerPrefHelper.SetBool(item.Key + "_Selected", false);
            }

            PlayerPrefHelper.SetBool(_key + "_Selected", true);
            UpdateItemEntries();
        }

        /// <summary>
        /// Updates the item entries.
        /// </summary>
        /// <param name="_item">Item.</param>
        public void UpdateItemEntries(ItemModel _item)
        {
            UpdateItemEntries();
        }

        /// <summary>
        /// Updates the item entries.
        /// </summary>
        public void UpdateItemEntries()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Unlocked = PlayerPrefHelper.GetBool(items[i].Key);
                items[i].Selected = PlayerPrefHelper.GetBool(items[i].Key + "_Selected");
            }
        }

        /// <summary>
        /// Gets the item array.
        /// </summary>
        /// <returns>The item array.</returns>
        public ItemModel[] GetItemArray()
        {
            return items;
        }

        /// <summary>
        /// Unsubscribes to the events that we used.
        /// </summary>
        private void OnDisable()
        {
            CodeScreenManager.onNewCodeUsed -= UpdateItemEntries;
        }
    }
}

