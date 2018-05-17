using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

namespace UI.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        private static Vector2 MENU_MIDDLE_POSITION = new Vector2(0f, -650f);

        private static Vector2 MENU_BIG_BUTTON_SIZE = new Vector2(235f, 270f);
        private static Vector2 MENU_SMALL_BUTTON_SIZE = new Vector2(215f, 250f);

        [SerializeField] private Image hexagonImage;

        [SerializeField] private Button homeButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Text pressToPlayText;

        [SerializeField] private Button demoButton;

        [SerializeField] private Button[] menuButtons;

        Sequence textFade;

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
                homeButton.gameObject.SetActive(true);
                shopButton.gameObject.SetActive(true);
                settingsButton.gameObject.SetActive(true);
            });

            openingSequence.Append(homeButton.GetComponent<Image>().DOFade(1, 1));
            openingSequence.Append(shopButton.GetComponent<Image>().DOFade(1, 2));
            openingSequence.Join(settingsButton.GetComponent<Image>().DOFade(1, 2));
        }

        private void OnDemoButtonClicked()
        {
            textFade.Append(pressToPlayText.DOFade(0, 2));
            textFade.AppendInterval(0.2f);
            textFade.Kill();
        }

        private void OnHomeButtonClicked()
        {
            CheckPosition(homeButton);
        }

        private void OnShopButtonClicked()
        {
            CheckPosition(shopButton);
        }

        private void OnSettingsButtonClicked()
        {
            CheckPosition(settingsButton);
        }

        private void CheckPosition(Button button)
        {
            RectTransform rect = button.GetComponent<RectTransform>();

            if(rect.anchoredPosition == MENU_MIDDLE_POSITION)
            {
                return;
            }

            Vector2 vec2 = new Vector2(rect.anchoredPosition.x * -1, rect.anchoredPosition.y);
                                      
            Sequence s = DOTween.Sequence();
            s.Append(rect.DOAnchorPos(MENU_MIDDLE_POSITION, 1));
            s.Join(rect.DOSizeDelta(MENU_BIG_BUTTON_SIZE, 1));

            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition == MENU_MIDDLE_POSITION))
            {
                s.Join(btn.GetComponent<RectTransform>().DOAnchorPos(vec2, 1));
                s.Join(btn.GetComponent<RectTransform>().DOSizeDelta(MENU_SMALL_BUTTON_SIZE, 1));
            }

            foreach (Button btn in menuButtons.Where(btn => btn.GetComponent<RectTransform>().anchoredPosition != MENU_MIDDLE_POSITION))
            {
                if(btn != button)
                {
                    s.Join(btn.GetComponent<Image>().DOFade(0, 1));
                    s.Append(btn.GetComponent<RectTransform>().DOAnchorPos(new Vector2(Mathf.CeilToInt(vec2.x * -1), vec2.y), 0.1f));
                    s.Append(btn.GetComponent<Image>().DOFade(1, 1));
                }
            }
        }

        private void OnDisable()
        {
            
        }
    }
}
