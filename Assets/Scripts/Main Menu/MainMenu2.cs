using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    public string gameSceneName1 = "Story Mode";
    public string gameSceneName2 = "Practice";
    public string gameSceneName3 = "MainMenu";
    // Start is called before the first frame update
    public void StoryMode()
    {
        SceneManager.LoadScene(gameSceneName1);
    }

    // Update is called once per frame
    public void Practice()
    {
        SceneManager.LoadScene(gameSceneName2);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(gameSceneName3);
    }
}
