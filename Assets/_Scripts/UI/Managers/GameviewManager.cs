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

    protected override void OnEnable()
    {
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

    protected override void StopScreen()
    {
        
    }

    protected override void OnDisable()
    {
        CollisionHandler.OnFadeThroughCollision -= UpdateScore;
    }
}
