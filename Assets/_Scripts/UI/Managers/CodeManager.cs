using UI.Controllers;
using UnityEngine.UI;
using UI.Base;
using UnityEngine;
using System;
using DG.Tweening;
using System.Linq;

public class CodeManager : ScreenManager 
{
    public static CodeManager instance;

    [SerializeField] private InputField codeInput;
    [SerializeField] private GameObject shirt;

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

            foreach (Item item in ItemManager.instance.GetItemArray().Where(item => item.Key == code))
            {
                ShirtPreviewManager.instance.SetSkin(item.ItemTexture);
            }

            DoAnimationSequence();
        }
        else if (PlayerPrefs.HasKey(code) && PlayerPrefHelper.GetBool(code) == true)
        {
            Debug.Log("Sorry, code is already used.");
        }
        else
        {
            Debug.Log("Code invalid");
        }
    }

    private void DoAnimationSequence()
    {
        Sequence s = DOTween.Sequence();
        s.Append(shirt.transform.DOScale(new Vector3(3, 3, 3), 1));
        s.Append(shirt.transform.DORotate(new Vector3(0, 360,0), 10f));

        s.AppendCallback(() => 
        {
            s.Append(shirt.transform.DOScale(new Vector3(0, 0, 0), 1));
            s.Join(shirt.transform.DORotate(new Vector3(0, 360, 0), 1));
        });

        s.AppendInterval(1f);

        s.AppendCallback(() =>
        {
            UIController.instance.GoToShopScreen();
        });
    }

    protected override void StopScreen()
    {
        codeInput.onEndEdit.RemoveAllListeners();
    }
}
