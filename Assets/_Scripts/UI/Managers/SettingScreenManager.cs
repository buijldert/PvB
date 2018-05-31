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

    [SerializeField] private Button[] settingsButtons;

    protected override void OnEnable()
    {
        base.OnEnable();

        settingsButtons[0].onClick.AddListener(() => OnSoundButtonClicked());
        settingsButtons[1].onClick.AddListener(() => OnVibrationButtonClicked());
        settingsButtons[2].onClick.AddListener(() => OnAchievementButtonClicked());
        settingsButtons[3].onClick.AddListener(() => OnCreditsButtonClicked());
    }

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

        foreach (Button button in settingsButtons)
        {
            Sequence s = DOTween.Sequence();

            button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);

            s.Append(button.GetComponent<Image>().DOFade(1, 0.75f));
            s.Join(button.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 750 + (index * -325)), 0.5f));

            index++;
        }
    }

    private void OnSoundButtonClicked()
    {
        SettingsController.SetMute(!SettingsController.GetMuteState());
    }

    private void OnVibrationButtonClicked()
    {
        SettingsController.SetVibration(!SettingsController.GetVibrationState());
    }

    private void OnAchievementButtonClicked()
    {
        
    }

    private void OnCreditsButtonClicked()
    {
        
    }

    protected override void StopScreen()
    {
        
    }
}
