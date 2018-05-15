using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using DG.Tweening;

namespace UI.Managers
{
    public class MenuManager : ScreenManager
    {
        public static MenuManager instance;

        [SerializeField] private Image hexagonImage;

        [SerializeField] private Button homeButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Text pressToPlayText;

        [SerializeField] private Button demoButton;

        Sequence textFade;

        protected override void OnEnable()
        {
            base.OnEnable();

            demoButton.onClick.AddListener(() => OnDemoButtonClicked());
        }

        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

            screenState = MenuState.Menu;
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
            Debug.Log("Help");

            Sequence openingSequence = DOTween.Sequence();

            openingSequence.Append(hexagonImage.rectTransform.DOSizeDelta(new Vector2(250, 250), 1));
            openingSequence.Join(hexagonImage.rectTransform.DOAnchorPos(new Vector2(0, -650), 1));

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

        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
           
        }



        private void OnDemoButtonClicked()
        {
            textFade.Append(pressToPlayText.DOFade(0, 2));

            textFade.AppendInterval(0.2f);
            textFade.Kill();






        }

        protected override void StopScreen()
        {
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
