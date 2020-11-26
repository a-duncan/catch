using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public enum PauseState {Unpaused, PauseMenu, OptionsMenu}

    public static PauseState pauseState = PauseState.Unpaused;


    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public CinemachineFreeLook cinemachineFL;

    public float xSensitivityMultiplier = 0.02f;
    public float ySensitivityMultiplier = 0.00005f;


    public void MenuBackout()
    {
        switch (pauseState)
        {
            case PauseState.Unpaused:
                Pause();
                break;
            case PauseState.PauseMenu:
                Resume();
                break;
            case PauseState.OptionsMenu:
                OptionsToPause();
                break;
            default:
                break;
        }
    }


    public void PauseToOptions()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        pauseState = PauseState.OptionsMenu;
    }

    public void OptionsToPause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        pauseState = PauseState.PauseMenu;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseState = PauseState.Unpaused;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseState = PauseState.PauseMenu;
    }

    public void ReloadScene()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //public void LoadMainMenu()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("MainMenu");
    //}

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }


    public void SetYInvert(bool mouseInvert)
    {
        cinemachineFL.m_YAxis.m_InvertInput = !mouseInvert;
    }

    public void SetXSensitivity(float sensitivity)
    {
        //Debug.Log("X sensitivity: " + sensitivity.ToString());
        cinemachineFL.m_XAxis.m_MaxSpeed = sensitivity * xSensitivityMultiplier;
    }

    public void SetYSensitivity(float sensitivity)
    {
        //Debug.Log("Y sensitivity: " + sensitivity.ToString());
        cinemachineFL.m_YAxis.m_MaxSpeed = sensitivity * ySensitivityMultiplier;
    }
}
