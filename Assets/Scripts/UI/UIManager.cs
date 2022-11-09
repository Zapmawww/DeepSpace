using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //Instantiating the MenuManager

    [SerializeField] private GameObject InGameUI;    //The normal interface display of the game running
    [SerializeField] private GameObject TaskMenu;    //task menu interface
    [SerializeField] private GameObject PauseMenu;   //pause menu interface
    [SerializeField] private GameObject BagMenu;     //bag menu interface
    [SerializeField] private GameObject GameOverDead;  //game over interface, Player dies , with zero health or oxygen
    [SerializeField] private GameObject GameOverLose;  //game over interface, Player lose the game
    [SerializeField] private GameObject GameOverWin;  //game over interface, Player win the game

    [SerializeField] private TMP_Text HealthPoint;    //Text display of health value
    [SerializeField] private TMP_Text OxygenPoint;    //Text display of oxygen value


    [SerializeField] public int currentHealth;       
    [SerializeField] public int currentOxygen;

    [SerializeField] public bool InGameUIshowed = true;
    [SerializeField] public bool TaskMenuShowed = false;    //Determine if the interface is being displayed
    [SerializeField] public bool PauseMenuShowed = false;
    [SerializeField] public bool BagMenuShowed = false;
    [SerializeField] public bool GameEnd = false;          //Whether the game wins or loses, the game is end

    public void ShowInGameUI()     //display interface
    {
        Debug.Log("Show InGameUI");
        InGameUIshowed = true;
        InGameUI.SetActive(true);
    }

    public void HideInGameUI()    //hide interface
    {
        Debug.Log("Hide InGameUI");
        InGameUIshowed = false;
        InGameUI.SetActive(false);
    }

    public void ShowPauseMenu()   //display interface
    {
        Debug.Log("Show PauseMenu");
        HideInGameUI();
        PauseMenuShowed = true;
        PauseMenu.SetActive(true);
    }

    public void ShowTaskMenu()    //display interface
    {
        Debug.Log("Show TaskMenu");
        HideInGameUI();
        TaskMenuShowed = true;
        TaskMenu.SetActive(true);
    }

    public void HideTaskMenu()  //hide interface
    {
        Debug.Log("Hide TaskMenu");
        ShowInGameUI();
        TaskMenuShowed = false;
        TaskMenu.SetActive(false);
    }

    public void HidePauseMenu()   //hide interface
    {
        Debug.Log("Hide PauseMenu");
        ShowInGameUI();
        PauseMenuShowed = false;
        PauseMenu.SetActive(false);
    }


    public void ShowBagMenu()   //display interface
    {
        Debug.Log("Show BagMenu");
        HideInGameUI();
        BagMenuShowed = true;
        BagMenu.SetActive(true);
    }

    public void HideBagMenu()   //hide interface
    {
        Debug.Log("Hide BagMenu");
        ShowInGameUI();
        BagMenuShowed = false;
        BagMenu.SetActive(false);
    }

    public void ShowGameOverDead()   //display interface
    {
        Debug.Log("Show GameOverDead");
        HideInGameUI();
        GameEnd = true;
        GameOverDead.SetActive(true);
    }

    public void HideGameOverDead()   //hide interface
    {
        Debug.Log("Hide GameOverDead");
        ShowInGameUI();
        GameEnd = false;
        GameOverDead.SetActive(false);
    }

    public void ShowGameOverLose()   //display interface
    {
        Debug.Log("Show GameOverLose");
        HideInGameUI();
        GameEnd = true;
        GameOverLose.SetActive(true);
    }

    public void HideGameOverLose()   //hide interface
    {
        Debug.Log("Hide GameOverLose");
        ShowInGameUI();
        GameEnd = false;
        GameOverLose.SetActive(false);
    }

    public void ShowGameOverWin()   //display interface
    {
        Debug.Log("Show GameOverWin");
        HideInGameUI();
        GameEnd = true;
        GameOverWin.SetActive(true);
    }

    public void HideGameOverWin()   //hide interface
    {
        Debug.Log("Hide GameOverWin");
        ShowInGameUI();
        GameEnd = false;
        GameOverWin.SetActive(false);
    }

    public void BackToMainMenu()   //turn to scene 'mainmenu'
    {
        Debug.Log("Back to Main Menu");
        HidePauseMenu();
        SceneManager.LoadScene(0);
    }

    public void ReplayGame()   //reload scene 'maingame'
    {
        Debug.Log("Replay Game");
        HidePauseMenu();
        SceneManager.LoadScene(2);
    }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        currentHealth = Player.Instance.currentHealth;      //Synchronize the data of the script Player.cs
        currentOxygen = Player.Instance.currentOxygen;

        HealthPoint.text = "" + currentHealth;       //Synchronize the text of value
        OxygenPoint.text = "" + currentOxygen;

        if(currentHealth == 0 || currentOxygen == 0)
        {
            GameEnd = true;
            ShowGameOverDead();
        }

        if (Input.GetKeyUp(KeyCode.P)) //Shortcut keys to control the display of the  pause menu interface
        {
            if (PauseMenuShowed && !InGameUIshowed)   //It can only be triggered when the  pause menu interface is displayed
            {
                HidePauseMenu(); 
            }
            else if (!PauseMenuShowed && InGameUIshowed)
            {
                ShowPauseMenu();
            }
        }


        if (Input.GetKeyUp(KeyCode.B))  //Shortcut keys to control the display of the bag menu interface
        {
            if (BagMenuShowed && !InGameUIshowed)  //It can only be triggered when the bag menu interface is displayed
            {
                HideBagMenu();
            }
            else if (!BagMenuShowed && InGameUIshowed)
            {
                ShowBagMenu();
            }
        }

        if (Input.GetKeyUp(KeyCode.T))  //Shortcut keys to control the display of the task menu interface
        {
            if (TaskMenuShowed && !InGameUIshowed)  //It can only be triggered when the task menu interface is displayed
            {
                HideTaskMenu();
            }
            else if (!TaskMenuShowed && InGameUIshowed)
            {
                ShowTaskMenu();
            }
        }


        if (Input.GetKeyUp(KeyCode.Y))   //Shortcut keys to turn to scene 'main menu'
        {
            if (PauseMenuShowed || GameEnd)      //It can only be triggered when the pause menu interface is displayed  
                                                // or game end
            {
                BackToMainMenu();
            }

        }

        if (Input.GetKeyUp(KeyCode.R))   //Shortcut keys to reload scene 'main game'
        {
            if (PauseMenuShowed || GameEnd)      //It can only be triggered when the pause menu interface is displayed  
                                                // or game end
            {
                ReplayGame();
            }

        }
    }
}
