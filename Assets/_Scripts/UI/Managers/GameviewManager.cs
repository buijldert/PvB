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

    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;

    private int score;
    private bool b;

    protected override void OnEnable()
    {
        pauseButton.onClick.AddListener(() => OnPauseButtonClicked());

        CollisionHandler.OnFadeThroughCollision += UpdateScore;

        CollisionHandler.OnDeadlyCollision += ShowGameOver;
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
        ResetScore();
    }

    private void UpdateScore(int _scoreMutation)
    {
        score += _scoreMutation;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    private void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
        GameOverScreenManager.instance.UpdateScore();
    }

    private void OnPauseButtonClicked()
    {
        PauseGameManager.instance.PauseGame();
        pauseScreen.SetActive(true);
    }

    protected override void StopScreen()
    {
        pauseScreen.SetActive(false);
    }

    public int GetScore()
    {
        return score;
    }

    protected override void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();

        CollisionHandler.OnFadeThroughCollision -= UpdateScore;
    }
}
