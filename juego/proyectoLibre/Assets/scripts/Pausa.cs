using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public bool GamsIsPaused;
    public Canvas PauseMenuUI;
    
    // Start is called before the first frame update
    void Start()
    {
        PauseMenuUI.enabled = false;
        GamsIsPaused = false;
    }

    private void Awake()
    {
        GamsIsPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamsIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.enabled = false; 
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GamsIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.enabled = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GamsIsPaused = true;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}