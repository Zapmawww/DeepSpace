using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject TaskMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject BagMenu;

    private bool TaskMenuShowed = false;
    private bool PauseMenuShowed = false;
    private bool BagMenuShowed = false;

    public void ShowInGameUI()
    {
        Debug.Log("Show InGameUI");
        InGameUI.SetActive(true);
    }

    public void HideInGameUI()
    {
        Debug.Log("Hide InGameUI");
        InGameUI.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        Debug.Log("Show PauseMenu");
        HideInGameUI();
        PauseMenuShowed = true;
        PauseMenu.SetActive(true);
    }

    public void ShowTaskMenu()
    {
        Debug.Log("Show TaskMenu");
        HideInGameUI();
        TaskMenuShowed = true;
        TaskMenu.SetActive(true);
    }

    public void HideTaskMenu()
    {
        Debug.Log("Hide TaskMenu");
        ShowInGameUI();
        TaskMenuShowed = false;
        TaskMenu.SetActive(false);
    }

    public void HidePauseMenu()
    {
        Debug.Log("Hide PauseMenu");
        ShowInGameUI();
        PauseMenuShowed = false;
        PauseMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
        HidePauseMenu();
        SceneManager.LoadScene(0);
    }

    public void ShowBagMenu()
    {
        Debug.Log("Show BagMenu");
        HideInGameUI();
        BagMenuShowed = true;
        BagMenu.SetActive(true);
    }

    public void HideBagMenu()
    {
        Debug.Log("Hide BagMenu");
        ShowInGameUI();
        BagMenuShowed = false;
        BagMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (PauseMenuShowed)
            {
                HidePauseMenu(); 
            }
            else
            {
                ShowPauseMenu();
            }
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            BackToMainMenu();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            if (BagMenuShowed)
            {
                HideBagMenu();
            }
            else
            {
                ShowBagMenu();
            }
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            if (TaskMenuShowed)
            {
                HideTaskMenu();
            }
            else
            {
                ShowTaskMenu();
            }
        }
    }
}
