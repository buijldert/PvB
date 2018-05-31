using UI.Base;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class GameviewManager : ScreenManager 
{
    public static GameviewManager instance;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Text scoreText;

    private int score;
    private bool b;

    protected override void OnEnable()
    {
        pauseButton.onClick.AddListener(() => OnPauseButtonClicked());

        CollisionHandler.OnFadeThroughCollision += UpdateScore;
    }

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        screenState = MenuState.GameView;
    }

    protected override void StartScreen()
    {
        
    }

    private void UpdateScore(int _scoreMutation)
    {
        score += _scoreMutation;
        scoreText.text = score.ToString();
    }

    private void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    private void OnPauseButtonClicked()
    {
        b = !b;

        if(b)
        {
            PauseGameManager.instance.PauseGame();
        }
        else
        {
            PauseGameManager.instance.ResumeGame();
        }
    }

    protected override void StopScreen()
    {
        
    }

    protected override void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();

        CollisionHandler.OnFadeThroughCollision -= UpdateScore;
    }
}
