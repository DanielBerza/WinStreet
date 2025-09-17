using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public string gameSceneName = "MainMenu2";
   public GameObject settingPanel;

   public void PlayGame()
   {
    SceneManager.LoadScene(gameSceneName);
   }

   public void OpenSettings()
   {
        if (settingPanel != null)
        settingPanel.SetActive(true);
   }
   public void CloseSettings()
   {
    if (settingPanel != null)
    settingPanel.SetActive(false);
   }
   public void QuitGame()
   {
    Debug.Log("Quitting the game...");
    Application.Quit();
   }
}
