using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseAndEndGameUI : MonoBehaviour
{

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings -1;

        if(currentSceneIndex < totalScenes)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else { EndGame(); }
    }

    public void EndGame() { Debug.LogWarning("End of available levels, activating end game canvas"); }
    public void GoToTitle() { Debug.LogWarning("button pressed, returning to title"); }
    public void QuitGame() { Application.Quit(); }
    public void RestartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

}
