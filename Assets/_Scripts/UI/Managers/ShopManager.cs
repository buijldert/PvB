using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using DG.Tweening;

public class ShopManager : ScreenManager
{
    public static ShopManager instance;

    public Image[] images;

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        screenState = MenuState.Shop;
    }

    protected override void StartScreen()
    {
        Sequence s = DOTween.Sequence();

        foreach (Image img in images)
        {
            s.Append(img.DOFade(1, 0.1f));
        }
    }

    protected override void StopScreen()
    {
        Sequence s = DOTween.Sequence();

        foreach (Image img in images)
        {
            s.Append(img.DOFade(0, 0.1f));
        }
    }
}
