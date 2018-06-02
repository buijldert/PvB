using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UI.Controllers;
using DG.Tweening;

namespace UI.Managers
{
    /// <summary>
    /// This class controlls and animates the different Menu buttons.
    /// This class is not part of the standard menu flow.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        private static Vector2 MENU_MIDDLE_POSITION = new Vector2(0f, -650f);
        private static Vector2 MENU_BIG_BUTTON_SIZE = new Vector2(235f, 270f);
        private static Vector2 MENU_SMALL_BUTTON_SIZE = new Vector2(215f, 250f);

        public static Action onOpeningSequenceEnded;

        private enum ButtonType
        {
            HomeButton,
            ShopButton,
            SettingsButton
        }

        [SerializeField] private Text pressToPlayText;
        [SerializeField] private Image hexagonImage;
        [SerializeField] private Button demoButton;

        [Header("Menu Buttons")]
        [SerializeField] private Button[] menuButtons;

        private Sequence textFade;

        /// <summary>
        /// Subscribes to the menu methods
        /// </summary>
        private void OnEnable()
        {
            demoButton.onClick.AddListener(() => OnDemoButtonClicked());

            menuButtons[(int)ButtonType.HomeButton].onClick.AddListener(() => OnHomeButtonClicked());
            menuButtons[(int)ButtonType.ShopButton].onClick.AddListener(() => OnShopButtonClicked());
            menuButtons[(int)ButtonType.SettingsButton].onClick.AddListener(() => OnSettingsButtonClicked());
        }

        /// <summary>
        /// Singleton Implementation
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        /// <summary>
        /// Starts the text-pulse
        /// </summary>
        private void Start()
        {
            DOTextPulse();
        }

        #region DOTween Animations
        /// <summary>
        /// Animates the "Tap to start"-text to create a pulse effect
        /// </summary>
        private void DOTextPulse()
        {
            textFade = DOTween.Sequence();
            textFade.Append(pressToPlayText.GetComponent<Text>().DOFade(0, 2));
            textFade.Append(pressToPlayText.GetComponent<Text>().DOFade(1, 2));

            textFade.AppendCallback(() =>
            {
                textFade.Restart();
            });

            textFade.OnKill(() =>
            {
                pressToPlayText.gameObject.SetActive(false);
                demoButton.gameObject.SetActive(false);
                DoOpeningSequence();
            });
        }

        /// <summary>
        /// Aniamtes the opening sequence to make the Menu appear.
        /// </summary>
        private void DoOpeningSequence()
        {
            Sequence openingSequence = DOTween.Sequence();
            openingSequence.Append(hexagonImage.rectTransform.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 1));
            openingSequence.Join(hexagonImage.rectTransform.DOAnchorPos(MENU_MIDDLE_POSITION, 1));

            openingSequence.AppendCallback(() =>
            {
                hexagonImage.gameObject.SetActive(false);
                menuButtons[(int)ButtonType.HomeButton].gameObject.SetActive(true);
                menuButtons[(int)ButtonType.ShopButton].gameObject.SetActive(true);
                menuButtons[(int)ButtonType.SettingsButton].gameObject.SetActive(true);
            });

            openingSequence.Append(menuButtons[(int)ButtonType.HomeButton].GetComponent<Image>().DOFade(1, 1));

            openingSequence.AppendCallback(() =>
            {
                if (onOpeningSequenceEnded != null)
                {
                    onOpeningSequenceEnded();
                }
            });

            openingSequence.Append(menuButtons[(int)ButtonType.ShopButton].GetComponent<Image>().DOFade(1, 2));
            openingSequence.Join(menuButtons[(int)ButtonType.SettingsButton].GetComponent<Image>().DOFade(1, 2));
        }

        /// <summary>
        /// Animates the menu
        /// </summary>
        /// <param name="button">The button we clicked on.</param>
        private void DoButtonAnimation(Button button)
        {
            // If the button we click is the middle button, just return
            RectTransform rect = button.GetComponent<RectTransform>();
            if (rect.anchoredPosition == MENU_MIDDLE_POSITION)
            {
                return;
            }

            // Get components for the VFX
            RectTransform rectTransform = button.transform.GetChild(0).GetComponent<RectTransform>();
            Image VFXImage = rectTransform.GetComponent<Image>();
            Vector2 oppositeVec2 = new Vector2(rect.anchoredPosition.x * -1, rect.anchoredPosition.y);

            // The Sequence 
            Sequence menuSequence = DOTween.Sequence();

            // Move the button we clicked to the middle and scale it
            menuSequence.Append(rect.DOAnchorPos(MENU_MIDDLE_POSITION, 1));
            menuSequence.Join(rect.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 1));

            // We want to move the button which is in de middle to the opposite side of the button whic WAS clicked
            // We also want to resize the button, and make it non-interactable
            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition == MENU_MIDDLE_POSITION))
            {
                btn.interactable = false;

                menuSequence.Join(btn.GetComponent<RectTransform>().DOAnchorPos(oppositeVec2, 1));
                menuSequence.Join(btn.GetComponent<RectTransform>().DOSizeDelta(MENU_SMALL_BUTTON_SIZE, 1));
            }

            // When we click on a button do a VFX
            menuSequence.Join(VFXImage.DOFade(1, 0.1f));
            menuSequence.Join(rectTransform.DOSizeDelta(new Vector2(MENU_BIG_BUTTON_SIZE.x * 1.5f, MENU_BIG_BUTTON_SIZE.y * 1.5f), 1));
            menuSequence.Join(VFXImage.DOFade(0, 1));

            // At last the button which is not clicked and is not in the middle is supposed to move to the old position of the button
            // we clicked. To do this we use the oppositeVec2 and multiply it by -1 to achieve the effect.
            // Also this button should be non-interactable until further notice.
            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition != MENU_MIDDLE_POSITION))
            {
                btn.interactable = false;

                if (btn != button)
                {
                    menuSequence.Join(btn.GetComponent<Image>().DOFade(0, 0.5f));
                    menuSequence.Append(btn.GetComponent<RectTransform>().DOAnchorPos(new Vector2(Mathf.CeilToInt(oppositeVec2.x * -1), oppositeVec2.y), 0.01f));
                    menuSequence.Append(btn.GetComponent<Image>().DOFade(1, 0.5f));
                }
            }

            // Make the VFX-Hexagon the same size as the big button
            menuSequence.Append(rectTransform.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 0.1f));

            // Make all buttons interactable again
            menuSequence.AppendCallback(() =>
            {
                foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition != MENU_MIDDLE_POSITION))
                {
                    btn.interactable = true;
                }
            });
        }
        #endregion // DOTween Animations

        #region UI Events
        /// <summary>
        /// Will fire when the "Tap to start"-screen is tapped and goes to the homescreen
        /// </summary>
        private void OnDemoButtonClicked()
        {
            textFade.Append(pressToPlayText.DOFade(0, 2));
            textFade.AppendInterval(0.2f);
            textFade.Kill();

            UIController.instance.GoToHomeScreen();
        }

        /// <summary>
        /// Method that will fire the moment the home-button is clicked
        /// </summary>
        private void OnHomeButtonClicked()
        {
            DoButtonAnimation(menuButtons[(int)ButtonType.HomeButton]);
            UIController.instance.GoToHomeScreen();
        }

        /// <summary>
        /// Method that will fire the moment the shop-button is clicked
        /// </summary>
        private void OnShopButtonClicked()
        {
            DoButtonAnimation(menuButtons[(int)ButtonType.ShopButton]);
            UIController.instance.GoToShopScreen();
        }

        /// <summary>
        /// Method that will fire the moment the settings-button is clicked
        /// </summary>
        private void OnSettingsButtonClicked()
        {
            DoButtonAnimation(menuButtons[(int)ButtonType.SettingsButton]);
            UIController.instance.GoToSettingsScreen();
        }
        #endregion //UI Events

        /// <summary>
        /// Unsubscribes to the menu methods
        /// </summary>
        private void OnDisable()
        {
            demoButton.onClick.RemoveAllListeners();

            menuButtons[(int)ButtonType.HomeButton].onClick.RemoveAllListeners();
            menuButtons[(int)ButtonType.ShopButton].onClick.RemoveAllListeners();
            menuButtons[(int)ButtonType.SettingsButton].onClick.RemoveAllListeners();
        }
    }
}
