using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class StartScene : MonoBehaviour
{
    public GameObject title;
    public GameObject startButton;
    public GameObject playButton;
    public GameObject quitButton;
    
    public void StartButton()
    {
        startButton.SetActive(false);
        title.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {

    }
}
