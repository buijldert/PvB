using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UI.Controllers;

namespace UI.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        private static Vector2 MENU_MIDDLE_POSITION = new Vector2(0f, -650f);
        private static Vector2 MENU_BIG_BUTTON_SIZE = new Vector2(235f, 270f);
        private static Vector2 MENU_SMALL_BUTTON_SIZE = new Vector2(215f, 250f);

        [SerializeField] private Image hexagonImage;
        [SerializeField] private Text pressToPlayText;

        [SerializeField] private Button demoButton;

        [SerializeField] private Button[] menuButtons;

        private Sequence textFade;

        private void OnEnable()
        {
            demoButton.onClick.AddListener(() => OnDemoButtonClicked());

            menuButtons[0].onClick.AddListener(() => OnHomeButtonClicked());
            menuButtons[1].onClick.AddListener(() => OnShopButtonClicked());
            menuButtons[2].onClick.AddListener(() => OnSettingsButtonClicked());
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        private void Start()
        {
            DOTextFade();
        }

        private void DOTextFade()
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

        private void DoOpeningSequence()
        {
            Sequence openingSequence = DOTween.Sequence();

            openingSequence.Append(hexagonImage.rectTransform.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 1));
            openingSequence.Join(hexagonImage.rectTransform.DOAnchorPos(MENU_MIDDLE_POSITION, 1));

            openingSequence.AppendCallback(() =>
            {
                hexagonImage.gameObject.SetActive(false);
                menuButtons[0].gameObject.SetActive(true);
                menuButtons[1].gameObject.SetActive(true);
                menuButtons[2].gameObject.SetActive(true);
            });

            openingSequence.Append(menuButtons[0].GetComponent<Image>().DOFade(1, 1));
            openingSequence.Append(menuButtons[1].GetComponent<Image>().DOFade(1, 2));
            openingSequence.Join(menuButtons[2].GetComponent<Image>().DOFade(1, 2));
        }

        private void OnDemoButtonClicked()
        {
            textFade.Append(pressToPlayText.DOFade(0, 2));
            textFade.AppendInterval(0.2f);
            textFade.Kill();
        }

        private void OnHomeButtonClicked()
        {
            DoButtonAnimation(menuButtons[0]);
            UIController.instance.GoToHomeScreen();
        }

        private void OnShopButtonClicked()
        {
            DoButtonAnimation(menuButtons[1]);
            UIController.instance.GoToShopScreen();
        }

        private void OnSettingsButtonClicked()
        {
            DoButtonAnimation(menuButtons[2]);
            UIController.instance.GoToSettingsScreen();
        }

        private void DoButtonAnimation(Button button)
        {
            RectTransform rect = button.GetComponent<RectTransform>();
            if (rect.anchoredPosition == MENU_MIDDLE_POSITION)
            {
                return;
            }

            // Get components for the VFX
            Transform VFX = button.transform.GetChild(0);
            RectTransform r = VFX.GetComponent<RectTransform>();
            Image img = VFX.GetComponent<Image>();

            Vector2 vec2 = new Vector2(rect.anchoredPosition.x * -1, rect.anchoredPosition.y);

            Sequence s = DOTween.Sequence();

            // Move the button we clicked to the middle and scale it
            s.Append(rect.DOAnchorPos(MENU_MIDDLE_POSITION, 1));
            s.Join(rect.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 1));

            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition == MENU_MIDDLE_POSITION))
            {
                btn.interactable = false;

                s.Join(btn.GetComponent<RectTransform>().DOAnchorPos(vec2, 1));
                s.Join(btn.GetComponent<RectTransform>().DOSizeDelta(MENU_SMALL_BUTTON_SIZE, 1));

                // VFX 
                s.Join(img.DOFade(1, 0.1f));
                s.Join(r.DOSizeDelta(new Vector2(MENU_BIG_BUTTON_SIZE.x * 1.5f, MENU_BIG_BUTTON_SIZE.y * 1.5f), 1));
                s.Join(img.DOFade(0, 1));
            }

            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition != MENU_MIDDLE_POSITION))
            {
                btn.interactable = false;

                if (btn != button)
                {
                    s.Join(btn.GetComponent<Image>().DOFade(0, 0.5f));
                    s.Append(btn.GetComponent<RectTransform>().DOAnchorPos(new Vector2(Mathf.CeilToInt(vec2.x * -1), vec2.y), 0.01f));
                    s.Append(btn.GetComponent<Image>().DOFade(1, 0.5f));
                }
            }

            s.Append(r.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 0.1f));

            s.AppendCallback(() => 
            {
                foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition != MENU_MIDDLE_POSITION))
                {
                    btn.interactable = true;
                }
            });
        }

        private void OnDisable()
        {
            
        }
    }
}
