using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraStopper : MonoBehaviour
{
    public cameraMover cameraMover;
    public GameObject bossMusic;
    public GameObject levelSoundtrack;
    public GameObject cutsceneSoundtrack;
    public GameObject bossUI;

    public playerHealth playerHealth;
    
    public bossNormalShooter bossNormalShooter;
    public bossShooting2 bossShooting2;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossStart")
        {
            playerHealth.isAtTheEnd = true;
            cameraMover.canMove = false;
            bossMusic.SetActive(true);
            levelSoundtrack.SetActive(false);
            cutsceneSoundtrack.SetActive(false);
            bossUI.SetActive(true);
            bossShooting2.canShoot = true;
            bossNormalShooter.canShoot = true;
        }

        if(playerHealth.isAtTheEnd == true) 
        {
            cameraMover.canMove = false;
        }
    }
}
