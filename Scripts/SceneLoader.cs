using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadMainScene()
    {
        SceneManager.LoadScene(1);
    }
    public void loadDeatchScene()
    {
        SceneManager.LoadScene(4);
    }

    public void loadEndScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void loadMusuemBinnen()
    {
        SceneManager.LoadScene(2);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
