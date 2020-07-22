using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{ 
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject DeadMenuUI;
    public GameObject FinishCheck;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    } 

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Dead()
    {
        DeadMenuUI.SetActive(true); 
    }

    public void Complete()
    {
        DeadMenuUI.SetActive(true); 
        DeadMenuUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "WELL PLAYED";
        FinishCheck.gameObject.SetActive(true);
    }

    public void Restart()
    { 
        Debug.Log("Restarting game..."); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    { 
        Debug.Log("Loading game..."); 
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game..."); 
        Application.Quit();
    } 
}
