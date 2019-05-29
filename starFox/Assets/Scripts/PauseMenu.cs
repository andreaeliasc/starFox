using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject HomeButton, Pausemenu, PauseButton;

    public void Pause()
    {
        Pausemenu.SetActive(true);
        PauseButton.SetActive(false);
        HomeButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Pausemenu.SetActive(false);
        PauseButton.SetActive(true);
        HomeButton.SetActive(true);
        Time.timeScale = 1;
    }
}
