using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void PlaySandbox()
    {
        levelLoader.LoadLevel("SandboxScene");
    }

    public void PlayLavaHailstorm()
    {
        levelLoader.LoadLevel("LavaHailstorm");
    }

    public void PlaySpacePong()
    {
        levelLoader.LoadLevel("SpacePong");
    }
}
