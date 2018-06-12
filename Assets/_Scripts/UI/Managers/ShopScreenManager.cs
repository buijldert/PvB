using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RR.Models;
using RR.UI.Base;
using RR.UI.Controllers;
using RR.Managers;
using DG.Tweening;
using RR.Components.Player;

namespace RR.UI.Managers
{
    /// <summary>
    /// This class controlls the UI Elements on the Shop-Screen
    /// </summary>
    public class ShopScreenManager : ScreenManager
    {
        public static ShopScreenManager instance;

        private List<ItemModel> unlockedItems = new List<ItemModel>();
        private List<GameObject> unlockedItemObjects = new List<GameObject>();

        [SerializeField] private Button codeInput;

        public Transform itemHolderContainer;

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            codeInput.onClick.AddListener(() => OnCodeInputButtonClicked());
        }

        /// <summary>
        /// Awake() is called before Start() and OnEnable().
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Shop;
        }

        /// <summary>
        /// Start() is called after Awake() and OnEnable().
        /// </summary>
        private void Start()
        {
            UpdateUnlockedItemsList();
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            UpdateUnlockedItemsList();
            DoStartupAnimation();
        }

        /// <summary>
        /// Does the start animation of this screen
        /// </summary>
        private void DoStartupAnimation()
        {
            unlockedItemObjects.Clear();

            Sequence s = DOTween.Sequence();
            for (int i = 0; i < unlockedItems.Count; i++)
            {
                GameObject itemHolder = Instantiate(Resources.Load<GameObject>("btn_ItemHolder"));

                RectTransform rect = itemHolder.GetComponent<RectTransform>();
                Text itemName = itemHolder.GetComponentInChildren<Text>();
                Button button = itemHolder.GetComponent<Button>();

                Image holderImage = itemHolder.GetComponent<Image>();
                Image[] childImages = itemHolder.GetComponentsInChildren<Image>();

                Image selectedImage = itemHolder.transform.GetChild(1).GetComponent<Image>();

                string key = unlockedItems[i].Key;
                ItemModel model = unlockedItems[i];
                childImages[1].sprite = unlockedItems[i].ItemUIImage; 

                if (unlockedItems[i].Selected)
                {
                    PlayerOutfit.instance.SwitchOutfit(model);
                    selectedImage.gameObject.SetActive(true);
                }

                button.onClick.AddListener(() =>
                {
                    ItemManager.instance.SetItemSelected(key);
                    PlayerOutfit.instance.SwitchOutfit(model);
                    SetSelectionSign(itemHolder);
                });

                unlockedItemObjects.Add(itemHolder);

                itemName.text = unlockedItems[i].ItemName;
                itemHolder.transform.SetParent(itemHolderContainer);

                rect.localScale = new Vector3(1, 1, 1);
                rect.anchoredPosition = new Vector2(0, (i * 325));

                s.Append(holderImage.DOColor(Color.white, 0.5f));
                s.Join(holderImage.DOFade(1, 0.5f));
                s.Join(itemName.DOFade(1, 0.5f));
                s.Join(selectedImage.DOFade(1, 0.5f));

                foreach (Image image in childImages)
                {
                    s.Join(image.DOColor(Color.white, 0.5f));
                }
            }
        }

        /// <summary>
        /// Sets the outfit on first load.
        /// </summary>
        public void SetOutfitOnFirstLoad()
        {
            foreach (ItemModel item in unlockedItems.Where(item => item.Selected == true))
            {
                PlayerOutfit.instance.SwitchOutfit(item);
            }
        }

        #region UI Events
        /// <summary>
        /// Sets the selection sign for the item we want to be selected.
        /// Also turns of the selection sign of all other items.
        /// </summary>
        /// <param name="itemHolder">Ithemholder which is selected.</param>
        private void SetSelectionSign(GameObject _itemHolder)
        {
            for (int i = 0; i < unlockedItemObjects.Count; i++)
            {
                unlockedItemObjects[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            _itemHolder.transform.GetChild(1).gameObject.SetActive(true);
        }

        /// <summary>
        /// Updates the unlockeditems list.
        /// </summary>
        private void UpdateUnlockedItemsList()
        {
            unlockedItems.Clear();
            foreach (ItemModel item in ItemManager.instance.GetItemArray().Where(item => item.Unlocked == true))
            {
                unlockedItems.Add(item);
            }
        }

        /// <summary>
        /// Will fire when the codeInput-button is clicked
        /// </summary>
        private void OnCodeInputButtonClicked()
        {
            UIController.instance.GoToCodeScreen();
        }
        #endregion // UI Events


        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            foreach (Transform child in itemHolderContainer)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        protected override void OnDisable()
        {
            codeInput.onClick.RemoveAllListeners();
        }
    } 
}