using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public void PlaySandbox()
    {
        SceneManager.LoadScene("SandboxScene");
    }

    public void PlayLavaHailstorm()
    {
        SceneManager.LoadScene("LavaHailstorm");
    }

    public void PlaySpacePong()
    {
        SceneManager.LoadScene("SpacePong");
    }
}
