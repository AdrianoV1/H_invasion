using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPausa : MonoBehaviour
{
    bool isPaused;

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }
    }

    public void Pausar()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
