using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UI.Base;
using UI.Controllers;
using DG.Tweening;

namespace UI.Managers 
{
    /// <summary>
    /// This class represents de CodeInputScreen, and controlls all the UI related
    /// animations and events of this screen. 
    /// </summary>
    public class CodeScreenManager : ScreenManager
    {
        public static CodeScreenManager instance;

        private static Vector3 START_ROTATION = new Vector3(0, 180, 0);
        private static Vector3 END_ROTATION = new Vector3(0, 360, 0);

        private static Vector3 START_SIZE = new Vector3(0, 0, 0);
        private static Vector3 END_SIZE = new Vector3(3, 3, 3);

        private const int ROTATION_ANIMATION_DURATION = 10;
        private const int SCALE_ANIMATION_DURATION = 1;
        private const int TIME_TILL_BACK_TO_SHOP = 1;

        public static Action<ItemModel> onNewCodeUsed;

        [SerializeField] private InputField codeInput;
        [SerializeField] private GameObject shirt;
        [SerializeField] private Text message;

        private Sequence shirtPreviewSequence;

        /// <summary>
        /// Subscribes to different events we want react on
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            codeInput.onEndEdit.AddListener((x) => OnCodeInputEditEnd(x));
        }

        /// <summary>
        /// Singleton Implementation.
        /// Also sets the screenstate to the state this script represents.
        /// </summary>
        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Code;
        }

        /// <summary>
        /// Will be called when we are on this particular screen
        /// </summary>
        protected override void StartScreen()
        {
            codeInput.gameObject.SetActive(true);
            shirt.SetActive(true);
        }

        /// <summary>
        /// Will fire when the inputfield is edited and the player has ended it.
        /// When the code is new we set the message and a preview item and add it to our save system;
        /// When the code is wrong or invalid we will display this too.
        /// </summary>
        /// <param name="_code">Code the player put in.</param>
        private void OnCodeInputEditEnd(string _code)
        {
            if (PlayerPrefs.HasKey(_code) && PlayerPrefHelper.GetBool(_code) == false)
            {
                PlayerPrefHelper.SetBool(_code, true);

                foreach (ItemModel item in ItemManager.instance.GetItemArray().Where(item => item.Key == _code))
                {
                    if (onNewCodeUsed != null)
                    {
                        onNewCodeUsed(item);
                    }

                    SetMessage(item.ItemName, true);
                }

                DoItemAnimationSequence();
            }
            else if (PlayerPrefs.HasKey(_code) && PlayerPrefHelper.GetBool(_code) == true)
            {
                Debug.Log("Sorry, code is already used.");
            }
            else
            {
                Debug.Log("Code invalid.");
            }
        }

        /// <summary>
        /// Animates the item to display the newly unlocked item to the player
        /// </summary>
        private void DoItemAnimationSequence()
        {
            codeInput.gameObject.SetActive(false);

            shirtPreviewSequence = DOTween.Sequence();
            shirtPreviewSequence.Append(shirt.transform.DOScale(END_SIZE, SCALE_ANIMATION_DURATION));
            shirtPreviewSequence.Append(shirt.transform.DORotate(END_ROTATION, ROTATION_ANIMATION_DURATION));

            shirtPreviewSequence.AppendCallback(() =>
            {
                shirtPreviewSequence.Append(shirt.transform.DOScale(START_SIZE, SCALE_ANIMATION_DURATION));
                shirtPreviewSequence.Join(shirt.transform.DORotate(START_ROTATION, SCALE_ANIMATION_DURATION));
            });

            shirtPreviewSequence.AppendInterval(TIME_TILL_BACK_TO_SHOP);
            shirtPreviewSequence.AppendCallback(() =>
            {
                UIController.instance.GoToShopScreen();
            });
        }

        /// <summary>
        /// Sets the text message, this message can also be the name of the unlocked item
        /// </summary>
        /// <param name="_message">Message we want te display.</param>
        /// <param name="_isActive">If set to <c>true</c> text-object is active.</param>
        private void SetMessage(string _message, bool _isActive = true)
        {
            message.text = _message;
            message.gameObject.SetActive(_isActive);
        }

        /// <summary>
        /// Will be called when we are not on this particular screen
        /// </summary>
        protected override void StopScreen()
        {
            shirt.SetActive(false);
            shirtPreviewSequence.Kill();

            SetMessage(string.Empty, false);
            codeInput.Clear();
        }

        /// <summary>
        /// Unsubscribes to different events we usedn
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            codeInput.onEndEdit.RemoveAllListeners();
        }
    }
}