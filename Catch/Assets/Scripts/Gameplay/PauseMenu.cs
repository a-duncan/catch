using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public enum PauseState {Unpaused, PauseMenu, OptionsMenu, OtherPause}

    public static PauseState pauseState = PauseState.Unpaused;


    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject winScreenUI;
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;
    public Toggle yInvertToggle;

    public CinemachineFreeLook cinemachineFL;

    public float xSensitivityMultiplier = 0.002f;
    public float ySensitivityMultiplier = 0.00001f;

    private void Start()
    {
        pauseState = PauseState.Unpaused;

        float sensitivity = PlayerPrefs.GetFloat("XSensitivity", 50f);
        xSensitivitySlider.value = sensitivity;
        cinemachineFL.m_XAxis.m_MaxSpeed = sensitivity * xSensitivityMultiplier;

        sensitivity = PlayerPrefs.GetFloat("YSensitivity", 50f);
        ySensitivitySlider.value = sensitivity;
        cinemachineFL.m_YAxis.m_MaxSpeed = sensitivity * ySensitivityMultiplier;

        bool invert = (PlayerPrefs.GetInt("YInvert", 0) != 0);
        yInvertToggle.isOn = invert;
        cinemachineFL.m_YAxis.m_InvertInput = !invert;
    }


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

    public void WinScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        winScreenUI.SetActive(true);
        Time.timeScale = 0f;
        pauseState = PauseState.OtherPause;
    }

    public void ReloadScene()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }


    public void SetXSensitivity(float sensitivity)
    {
        cinemachineFL.m_XAxis.m_MaxSpeed = sensitivity * xSensitivityMultiplier;
        PlayerPrefs.SetFloat("XSensitivity", sensitivity);
    }

    public void SetYSensitivity(float sensitivity)
    {
        cinemachineFL.m_YAxis.m_MaxSpeed = sensitivity * ySensitivityMultiplier;
        PlayerPrefs.SetFloat("YSensitivity", sensitivity);
    }

    public void SetYInvert(bool mouseInvert)
    {
        cinemachineFL.m_YAxis.m_InvertInput = !mouseInvert;
        PlayerPrefs.SetInt("YInvert", mouseInvert ? 1 : 0);
    }
}
