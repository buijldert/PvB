using UnityEngine;
using System.Linq;
using System;

namespace UI.Controllers
{
    public enum MenuState
    {
        Home,
        Shop,
        Code,
        Settings,
    }

    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public static Action<MenuState> OnScreenChanged;

        [SerializeField] private GameObject[] holders;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        public void SetState(MenuState state)
        {
            TurnHoldersInactive();
            holders[(int)state].SetActive(true);

            if (OnScreenChanged != null)
            {
                OnScreenChanged(state);
            }
        }

        private void TurnHoldersInactive()
        {
            foreach (GameObject holder in holders.Where(holder => holder.activeSelf))
            {
                holder.SetActive(false);
            }
        }

        #region GoTo methods
        public void GoToHomeScreen()
        {
            SetState(MenuState.Home);
        }

        public void GoToShopScreen()
        {
            SetState(MenuState.Shop);
        }

        public void GoToCodeScreen()
        {
            SetState(MenuState.Code);
        }

        public void GoToSettingsScreen()
        {
            SetState(MenuState.Settings);
        }
        #endregion
    }
}


