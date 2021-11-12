using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
