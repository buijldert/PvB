using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class HomeManager : ScreenManager 
{
    public static HomeManager instance;

    public static Action OnRestartGame;

    [SerializeField] private Button startbutton;

    [SerializeField] private Image background;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject objectPool;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject player;

    [SerializeField] private Image logo;


    protected override void OnEnable()
    {
        base.OnEnable();
        startbutton.onClick.AddListener(() => OnStartButtonClicked());
    }

    private void Start()
    {
        
    }

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;


        screenState = MenuState.Home;
    }

    protected override void StartScreen()
    {
        DoAnimation();
        player.SetActive(false);
    }

    public void DoAnimation()
    {
        Sequence s = DOTween.Sequence();
        s.Append(startbutton.GetComponent<Image>().DOFade(1, 1));
        s.Join(logo.DOFade(1, 1));

        //s.AppendCallback(() => {
        //    startbutton.interactable = true;
        //});
    }

    private void OnStartButtonClicked()
    {
        UIController.instance.GoToGameView();

        background.gameObject.SetActive(false);
        gameManager.SetActive(true);
        objectPool.SetActive(true);
        menu.SetActive(false);
        player.SetActive(true);

        StartGame();
    }

    public void StartGame()
    {
        if (OnRestartGame != null)
        {
            OnRestartGame();
        }
    }

    protected override void StopScreen()
    {
        Sequence s = DOTween.Sequence();

        s.Append(startbutton.GetComponent<Image>().DOFade(0, 0.1f));
        s.Join(logo.DOFade(0, 0.1f));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        startbutton.onClick.RemoveAllListeners();
    }
}
