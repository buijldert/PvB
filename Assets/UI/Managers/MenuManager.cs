using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;

namespace UI.Managers
{
    public class MenuManager : ScreenManager
    {
        public static MenuManager instance;

        [SerializeField] private Button playButton;

        protected override void OnEnable()
        {
            base.OnEnable();

            playButton.onClick.AddListener(() => OnPlayButtonClicked());
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

        protected override void PrepareScreen(MenuState state)
        {
            base.PrepareScreen(state);
        }

        protected override void StartScreen()
        {
            playButton.onClick.AddListener(() => OnPlayButtonClicked());
        }

        private void OnPlayButtonClicked()
        {
            UIController.instance.GoToPlayScreen();
        }

        protected override void StopScreen()
        {
            playButton.onClick.RemoveAllListeners();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
