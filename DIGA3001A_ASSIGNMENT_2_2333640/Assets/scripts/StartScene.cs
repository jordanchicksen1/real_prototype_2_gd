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

    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject story5;
    
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

    public void PlayButton()
    {
       
        playButton.SetActive(false);
        quitButton.SetActive(false);
        story1.SetActive(true);
    }

    public void Back1()
    {
        
        playButton.SetActive(true);
        quitButton.SetActive(true);
        story1.SetActive(false);
    }

    public void Next1()
    {
        story1.SetActive(false);
        story2.SetActive(true);
    }

    public void Back2()
    {
        story1.SetActive(true);
        story2.SetActive(false);
    }

    public void Next2()
    {
        story2.SetActive(false);
        story3.SetActive(true);
    }

    public void Back3()
    {
        story3.SetActive(false);
        story2.SetActive(true);
    }

    public void Next3()
    {
        story4.SetActive(true);
        story3.SetActive(false);
    }

    public void Back4()
    {
        story4.SetActive(false);
        story3.SetActive(true);
    }

    public void Next4()
    {
        story4.SetActive(false);
        story5.SetActive(true);
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Back5()
    {
        story4.SetActive(true);
        story5.SetActive(false);
    }


}
