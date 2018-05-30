using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using DG.Tweening;
using UnityEngine.UI;
using UI.Controllers;

public class SettingScreenManager : ScreenManager 
{
    public static SettingScreenManager instance;

    [SerializeField] private GameObject[] settingsButtons;

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        screenState = MenuState.Settings;
    }

    protected override void StartScreen()
    {
        DoStartupAnimation();
    }

    private void DoStartupAnimation()
    {
        int index = 0;

        foreach (GameObject button in settingsButtons)
        {
            Sequence s = DOTween.Sequence();

            s.Append(button.GetComponent<Image>().DOFade(1, 0.2f));
            s.Join(button.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 750 + (index * -325)), 1f / settingsButtons.Length));

            index++;
        }
    }

    protected override void StopScreen()
    {
        
    }
}
