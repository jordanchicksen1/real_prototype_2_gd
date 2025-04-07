using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]

    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed = 8f; // Speed at which the player moves
    private Vector2 _moveInput; // Stores the movement input from the player
    private Vector3 _velocity; // Velocity of the player
    public float crouchSpeed = 1.5f; //short speed
    public bool isCrouching = false; //if short or normal

    //shooting
    public GameObject bullet;
    public Transform bulletSpawnPoint;

    //boosting
    public bool canBoost = true;
    public ParticleSystem boostParticles;
    public boostBar boostBar;

    //laser
    public bool laserIsOut = false;
    public GameObject laser;
    public cameraMover cameraMover;
    public GameObject playerShip;
    public Color newColour;
    public Color oldColour;
    public Camera playerCam;

    //pause stuff
    public bool isPaused = false;
    public GameObject pauseScreen;

    //health testing
    public playerHealth playerHealth;

    //enemy stuff
   // public List<turretShooter> shooterList;

    //tension gauge stuff
    public tensionPoints tensionPoints;

    //regen stuff
    public regenManager regenManger;
    public ParticleSystem regenParticles;

    //start cutscene stuff
    public bool isAtStart = true;
    public float cutsceneMoveSpeed = 6f;
    public GameObject cutscene1;
    public GameObject cutscene2;
    public GameObject cutscene3;
    public GameObject cutscene4;
    public GameObject cutscene5;
    public GameObject cutscene6;
    public GameObject cutscene7;
    public GameObject cutsceneBorders;

    public AudioSource cutscene;

    
    public float missileSpeed = 5f;

    public GameObject missile1;
    public bool shootMissile1 = false;
    public GameObject missile2;
    public bool shootMissile2 = false;
    public GameObject missile3;
    public bool shootMissile3 = false;

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;

    public GameObject titleScreen;
    public GameObject presents;
    public GameObject title;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    public GameObject tutorial5;
    public GameObject tutorial6;
    public GameObject tutorial7;
    
    public GameObject playerUI;
    public GameObject boostBarUI;
    public GameObject regenUI;
    public GameObject bigLaserUI;

    //sounds
    public AudioSource playerSFX;
    public AudioClip shootSFX;
    public AudioClip usingRegen;
    public AudioClip playerHit;
    public AudioClip pickUpRegen;
    public AudioClip bigLaser;
    public AudioClip boost;
    public AudioClip titleSFX;
    public AudioClip textAppearSFx;
    public GameObject cutsceneSoundtrack;
    public GameObject levelSoundtrack;

    //boss fight
    public GameObject bossMusic;
    public GameObject bossUI;

    private void OnEnable()
    {

        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => _moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the light fire input event
        playerInput.Player.Boost.performed += ctx => Boost(); // Call the PickUpObject method when pick-up input is performed

        //Subscribe to the sprint
        playerInput.Player.Shoot.performed += ctx => Shoot(); // sprint

        //Subscribe to the UseFuel
        playerInput.Player.Bomb.performed += ctx => Bomb(); // use laser

        //Subscribe to the UseFuel
        playerInput.Player.UseRegen.performed += ctx => UseRegen(); // use fuel

        //Subscribe to the pause
        playerInput.Player.Pause.performed += ctx => Pause(); // use pause


    }

    public void Start()
    {
        StartCoroutine(Cutscene());
    }
    
    public void Update()
    {
        Move();

        if(shootMissile1 == true) 
        {
            missile1.transform.position = Vector2.MoveTowards(missile1.transform.position, ship1.transform.position, missileSpeed);
            Debug.Log("missile should fly into the ship 1");
            //shootMissile1 = false;
        }

        if (shootMissile2 == true)
        {
            missile2.transform.position = Vector2.MoveTowards(missile2.transform.position, ship2.transform.position, missileSpeed);
            Debug.Log("missile should fly into the ship 2");
            //shootMissile1 = false;
        }

        if (shootMissile3 == true)
        {
            missile3.transform.position = Vector2.MoveTowards(missile3.transform.position, ship3.transform.position, missileSpeed);
            Debug.Log("missile should fly into the ship 3");
            //shootMissile1 = false;
        }
    }
    public void Move()
    {
        if(isPaused == false && laserIsOut == false && playerHealth.isDead == false && isAtStart == false)
        {
            // Create a movement vector based on the input
            Vector2 move = new Vector2(_moveInput.x, _moveInput.y);

            // Transform direction from local to world space
            move = transform.TransformDirection(move);

            var currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

            // Move the character controller based on the movement vector and speed
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = move * currentSpeed;
            

            if (laserIsOut == true)
            {
                rb.velocity = Vector2.zero;
            }
        }

        if(isAtStart == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(cutsceneMoveSpeed, 0);
        }
       
       
    }
    public void Shoot()
    {
        if(isAtStart == false)
        {
            Debug.Log("Player should shoot");
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            playerSFX.clip = shootSFX;
            playerSFX.Play();
        }
    }
    public void Boost()
    {
        if(canBoost == true && isAtStart == false)
        {
            boostParticles.Play();
            Debug.Log("player should boost");
            canBoost = false;
            moveSpeed = moveSpeed * 2.5f;
            StartCoroutine(BoostTime());
            StartCoroutine(GiveBoostBack());
            boostBar.UseBoost();
            playerSFX.clip = boost;
            playerSFX.Play();
        }
       
    }
    public void Pause()
    {
        Debug.Log("game should pause");

    }

    public void UseRegen()
    {
        if(regenManger.regen > 0.99 && playerHealth.currentHealth < 100)
        {
            Debug.Log("should use a regen");
            regenManger.subtractRegen();
            playerHealth.Regen();
            playerSFX.clip = usingRegen;
            playerSFX .Play();
            regenParticles.Play();
        }
        
        
    }
    public void Bomb()
    {
        if(cameraMover.canMove == true)
        {
            if (laserIsOut == false && tensionPoints.currentTension > 99.9f)
            {
                Debug.Log("player should shoot bomb");
                laserIsOut = true;
                laser.SetActive(true);
                cameraMover.canMove = false;
                playerCam.backgroundColor = newColour;
                StartCoroutine(TurnOffLaser());
                tensionPoints.UsingLaser();
                playerShip.GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                playerSFX.clip = bigLaser;
                playerSFX.Play();

            }
        }
        
        if(cameraMover.canMove == false)
        {
            if (laserIsOut == false && tensionPoints.currentTension > 99.9f)
            {
                Debug.Log("player should shoot bomb");
                laserIsOut = true;
                laser.SetActive(true);
                cameraMover.canMove = false;
                playerCam.backgroundColor = newColour;
                StartCoroutine(TurnOffLaserCantMove());
                tensionPoints.UsingLaser();
                playerShip.GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                playerSFX.clip = bigLaser;
                playerSFX.Play();

            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "healthTester")
        {
            Debug.Log("should decrease health");
            playerHealth.HitByProjectile();
        }

        if(other.tag == "CameraKillBox")
        {
            playerHealth.currentHealth = 0f;
        }



        if(other.tag == "BadBullet")
        {
            playerHealth.HitByProjectile();
            playerSFX.clip = playerHit;
            playerSFX .Play();
            Destroy(other.gameObject);
        }

        if (other.tag == "BossMissile")
        {
            playerHealth.HitByMissile();
            playerSFX.clip = playerHit;
            playerSFX.Play();
            Destroy(other.gameObject);
        }

        if (other.tag == "Health")
        {
            regenManger.addRegen();
            Debug.Log("picked up regen");
            Destroy(other.gameObject);
            playerSFX.clip = pickUpRegen;
            playerSFX .Play();

        }

        if(other.tag == "BossStart")
        {
            cameraMover.canMove = false;
            bossMusic.SetActive(true);
            levelSoundtrack.SetActive(false);
            bossUI.SetActive(true);
        }




    }
    public IEnumerator BoostTime()
    {
        yield return new WaitForSeconds(0.5f);
        moveSpeed = moveSpeed / 2.5f;
    }

    public IEnumerator GiveBoostBack()
    {
        yield return new WaitForSeconds(0.5f);
        boostBar.shouldFillBar = true;
        yield return new WaitForSeconds(3f);
        canBoost = true;
        boostBar.shouldFillBar = false;
    }

    public IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(3f);
        laserIsOut = false;
        laser.SetActive(false);
        cameraMover.canMove = true;
        playerShip.GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerCam.backgroundColor = oldColour;
    }

    public IEnumerator TurnOffLaserCantMove()
    {
        yield return new WaitForSeconds(3f);
        laserIsOut = false;
        laser.SetActive(false);
        playerShip.GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerCam.backgroundColor = oldColour;
    }

    public IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1.5f);
        cutscene1.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(9f);
        cutscene1.SetActive(false);
        cutscene2.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(8f);
        cutscene2.SetActive(false);
        cutscene3.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(11f);
        cutscene3.SetActive(false);
        cutscene4.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(6.5f);
        cutscene4.SetActive(false);
        cutscene5.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(2f);
        cutscene5.SetActive(false);
        shootMissile1 = true;
        yield return new WaitForSeconds(1.5f);
        shootMissile3 = true;
        yield return new WaitForSeconds(1.5f);
        shootMissile2 = true;
        yield return new WaitForSeconds(3f);
        cutscene6.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(4f);
        cutscene6.SetActive(false);
        cutscene7.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(7f);
        cutsceneBorders.SetActive(false);
        titleScreen.SetActive(true);
       
        yield return new WaitForSeconds(1.5f);
        presents.SetActive(true);
        cutscene.Play();
        yield return new WaitForSeconds(2f);
        presents.SetActive(false);
        title.SetActive(true);
        cutscene.clip = titleSFX;
        cutscene.Play();
        yield return new WaitForSeconds(3.5f);
        titleScreen.SetActive(false);
        isAtStart = false;
        cutsceneSoundtrack.SetActive(false);
        levelSoundtrack.SetActive(true);

        //tutorial
        yield return new WaitForSeconds(2f);
        tutorial1.SetActive(true);
        playerUI.SetActive(true);
        cutscene.clip = textAppearSFx;
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial3.SetActive(true);
        tutorial2.SetActive(false);
        boostBarUI.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial3.SetActive(false);
        tutorial4.SetActive(true);
        regenUI.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(7f);
        tutorial4.SetActive(false);
        tutorial5.SetActive(true);
        bigLaserUI.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial5.SetActive(false);
        tutorial6.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial6.SetActive(false);
        tutorial7.SetActive(true);
        cutscene.Play();

        yield return new WaitForSeconds(6f);
        tutorial7.SetActive(false);

        


    }
}
