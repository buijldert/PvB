using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using UI.Controllers;

public class HomeManager : ScreenManager 
{
    public static HomeManager instance;

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

    protected override void StopScreen()
    {
        
    }
}
