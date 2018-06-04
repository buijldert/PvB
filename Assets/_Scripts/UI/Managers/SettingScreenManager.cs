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
        for (int i = settingsButtons.Length - 1; i >= 0; i--)
        { 
            Sequence s = DOTween.Sequence();

            settingsButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            settingsButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -350);

            s.Append(settingsButtons[i].GetComponent<Image>().DOFade(1, 0.75f));
            s.Join(settingsButtons[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 750 + (i * -325)), 0.5f));
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
