using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseAndEndGameUI : MonoBehaviour
{
    AudioManager audioManager;
    public bool gameSceneCalled = false;
    public static bool gameWin = false;
    CharacterSwitch cSwitch;
    bool gameIsPaused = false;
    public GameObject p1InventoryUI;
    public GameObject p2InventoryUI;
    [SerializeField] GameObject gameWinObj;
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "TitleScene") { gameObject.SetActive(true);} else { gameObject.SetActive(false); }
        audioManager = FindObjectOfType<AudioManager>();
        cSwitch = FindObjectOfType<CharacterSwitch>();
        if (gameWin) { gameWinObj.SetActive(true); }
        if (gameIsPaused) { Time.timeScale = 0; } else { Time.timeScale = 1; }
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings -1;

        //if there is another scene/level available after the current scene, go to that scene/level
        //otherwise, run the EndGame method
        if(currentSceneIndex < totalScenes)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameSceneCalled = true;
            audioManager.UpdateMusic();
        }
        else { EndGame(); }
    }

    public void Pause()
    {
        if (cSwitch.p1Active)
        {
            if (!gameIsPaused)
            {
                gameObject.SetActive(true);
                p1InventoryUI.SetActive(false);
                gameIsPaused = true;
            }
            else
            {
                gameObject.SetActive(false);
                p1InventoryUI.SetActive(true);
                gameIsPaused = false;
            }
        }
        else if (!cSwitch.p1Active)
        {
            if (!gameIsPaused)
            {
                gameObject.SetActive(true);
                p2InventoryUI.SetActive(false);
                gameIsPaused = true;
            }
            else
            {
                gameObject.SetActive(false);
                p2InventoryUI.SetActive(true);
                gameIsPaused = false;
            }
        }
    }

    public void EndGame() { SceneManager.LoadScene(0); audioManager.startOfGame = true; audioManager.UpdateMusic(); gameWinObj.SetActive(true); gameWin = true; }
    public void GoToTitle() { SceneManager.LoadScene(0); audioManager.startOfGame = true; audioManager.UpdateMusic(); }
    public void QuitGame() { Application.Quit(); }
    public void RestartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

}
