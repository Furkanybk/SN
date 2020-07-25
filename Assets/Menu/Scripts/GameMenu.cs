﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public static GameMenu current = null;

    public GameObject PauseMenuUI;
    public GameObject DeadMenuUI;
    public GameObject PauseButton;
     
    public static bool GameIsPaused = false;
    public static bool PlayerIsDead = false; 

    private void Awake()
    {
        if(!current)
        {
            current = this;
        }
        else
        {
            Debug.Log("There is already a GameMenu Script.");
            this.enabled = false;
            return;
        }
    }

    private void OnDestroy()
    {
        if (current == this)
        {
            current = null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
                PauseButton.SetActive(true);
            }
            else
            {
                Pause();
                PauseButton.SetActive(false);
            }
        }
    }

    public void Pause()
    {
        if (PlayerIsDead) return;

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
        PauseButton.SetActive(false);
        PlayerIsDead = true;
    }

    public void Complete()
    {
        DeadMenuUI.SetActive(true); 
        DeadMenuUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "WELL PLAYED"; 
        DeadMenuUI.transform.GetChild(1).gameObject.SetActive(false); 
    }

    public void RespawnCheckPoint()
    {
        if(GameManager.current.RemainingChance > 0)
        { 
            Debug.Log("Respawning checkpoint...");
            GameManager.current.Respawn(); 
            DeadMenuUI.SetActive(false);
            PlayerIsDead = false; 
            PauseButton.SetActive(true);
        }
        else
        { 
            Debug.Log("No more respawn...");
            DeadMenuUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " No more respawn you saved " + GameManager.current.ReachedCheckPoint + " checkpoınts";
            DeadMenuUI.transform.GetChild(1).gameObject.SetActive(false);
        }
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
