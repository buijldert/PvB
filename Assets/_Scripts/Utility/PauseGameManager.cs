using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public delegate void PauseGameAction();
    public static event PauseGameAction OnPauseGame;

    public delegate void ResumeGameAction();
    public static event ResumeGameAction OnResumeGame;

    [SerializeField] private GameObject pauseScreen;

	public void PauseGame()
    {
        pauseScreen.SetActive(true);
        if(OnPauseGame != null)
        {
            OnPauseGame();
        }
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if(OnResumeGame != null)
        {
            OnResumeGame();
        }
        pauseScreen.SetActive(false);

    }
}
