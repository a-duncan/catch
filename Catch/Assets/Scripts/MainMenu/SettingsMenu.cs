using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;
    public Toggle yInvertToggle;


    Resolution[] resolutions;
    
    float xSensitivity;
    float ySensitivity;
    bool yInvert;

    void Start()
    {
        InitResoDropdown();
        InitControlSettings();
    }

    void InitResoDropdown()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            option += " (" + resolutions[i].refreshRate + "hz)";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void InitControlSettings()
    {
        xSensitivity = PlayerPrefs.GetFloat("XSensitivity", 50f);
        xSensitivitySlider.value = xSensitivity;

        ySensitivity = PlayerPrefs.GetFloat("YSensitivity", 50f);
        ySensitivitySlider.value = ySensitivity;

        yInvert = (PlayerPrefs.GetInt("YInvert", 0) != 0);
        yInvertToggle.isOn = yInvert;
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetXSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("XSensitivity", sensitivity);
    }

    public void SetYSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("YSensitivity", sensitivity);
    }

    public void SetYInvert(bool invert)
    {
        PlayerPrefs.SetInt("YInvert", invert ? 1 : 0);
    }

}
