using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseAndEndGameUI : MonoBehaviour
{
    AudioManager audioManager;
    public bool gameSceneCalled = false;
    public bool gameWin = false;
    [SerializeField] GameObject gameWinObj;
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "TitleScene") { gameObject.SetActive(true);} else { gameObject.SetActive(false); }
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings -1;

        if(currentSceneIndex < totalScenes)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameSceneCalled = true;
            audioManager.UpdateMusic();
        }
        else { EndGame(); }
    }

    void GameWon()
    {
         gameWinObj.SetActive(true); 
    }

    public void EndGame() { SceneManager.LoadScene(0); audioManager.startOfGame = true; audioManager.UpdateMusic(); GameWon(); }
    public void GoToTitle() { SceneManager.LoadScene(0); audioManager.startOfGame = true; audioManager.UpdateMusic(); }
    public void QuitGame() { Application.Quit(); }
    public void RestartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

}
