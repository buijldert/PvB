using UI.Controllers;
using UnityEngine.UI;
using UI.Base;
using UnityEngine;
using System;

public class CodeManager : ScreenManager 
{
    public static CodeManager instance;

    [SerializeField] private InputField codeInput;

    public static Action onNewCodeUsed;

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        screenState = MenuState.Code;
    }

    protected override void StartScreen()
    {
        codeInput.onEndEdit.AddListener((x) => OnCodeInputEditEnd(x));
    }

    private void OnCodeInputEditEnd(string code)
    {
        if (PlayerPrefs.HasKey(code) && PlayerPrefHelper.GetBool(code) == false)
        {
            PlayerPrefHelper.SetBool(code, true);

            ItemManager.instance.UpdateItemEntries();
        }
        else if (PlayerPrefs.HasKey(code) && PlayerPrefHelper.GetBool(code) == true)
        {
            Debug.Log("Code already used mf");
        }
        else
        {
            Debug.Log("Stuk");
        }
    }

    protected override void StopScreen()
    {
        codeInput.onEndEdit.RemoveAllListeners();
    }
}
