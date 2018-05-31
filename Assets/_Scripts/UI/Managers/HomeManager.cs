using UnityEngine;
using UI.Base;
using UI.Controllers;
using UnityEngine.UI;
using System;

public class HomeManager : ScreenManager 
{
    public static HomeManager instance;

    public static Action OnRestartGame;

    [SerializeField] private Button startbutton;

    [SerializeField] private Image background;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject objectPool;

    protected override void OnEnable()
    {
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
        
    }

    private void OnStartButtonClicked()
    {
        UIController.instance.GoToGameView();

        background.gameObject.SetActive(false);
        gameManager.SetActive(true);
        objectPool.SetActive(true);

        StartGame();
    }

    private void StartGame()
    {
        if (OnRestartGame != null)
        {
            OnRestartGame();
        }
    }

    protected override void StopScreen()
    {
        
    }
}
