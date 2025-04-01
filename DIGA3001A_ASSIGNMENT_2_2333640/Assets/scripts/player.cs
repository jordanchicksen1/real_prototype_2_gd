using System.Collections;
using System.Collections.Generic;
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

    //pause stuff
    public bool isPaused = false;
    public GameObject pauseScreen;

    //health testing
    public playerHealth playerHealth;

    //enemy stuff
    public turretShooter turretShooter;
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
        playerInput.Player.Bomb.performed += ctx => Bomb(); // use fuel

        //Subscribe to the pause
        playerInput.Player.Pause.performed += ctx => Pause(); // use fuel


    }

    public void Update()
    {
        Move();
    }
    public void Move()
    {
        if(isPaused == false && laserIsOut == false)
        {
            // Create a movement vector based on the input
            Vector2 move = new Vector2(_moveInput.x, _moveInput.y);

            // Transform direction from local to world space
            move = transform.TransformDirection(move);

            var currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

            // Move the character controller based on the movement vector and speed
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = move * currentSpeed;
            Debug.Log("player should move");

            if (laserIsOut == true)
            {
                rb.velocity = Vector2.zero;
            }
        }
       
       
    }
    public void Shoot()
    {
        Debug.Log("Player should shoot");
        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity); 
    }
    public void Boost()
    {
        if(canBoost == true)
        {
            boostParticles.Play();
            Debug.Log("player should boost");
            canBoost = false;
            moveSpeed = moveSpeed * 2.5f;
            StartCoroutine(BoostTime());
            StartCoroutine(GiveBoostBack());
            boostBar.UseBoost();
        }
       
    }
    public void Pause()
    {
        Debug.Log("game should pause");

    }
    public void Bomb()
    {
        if (laserIsOut == false)
        {
            Debug.Log("player should shoot bomb");
            laserIsOut = true;
            laser.SetActive(true);
            cameraMover.canMove = false;
            StartCoroutine(TurnOffLaser());
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

        if(other.tag == "EnterEnemyRange")
        {
            turretShooter.isInEnemyRange = true;
            StartCoroutine(turretShooter.Shooter());
            Debug.Log("should start turret");
        }

        if(other.tag == "ExitEnemyRange")
        {
            turretShooter.isInEnemyRange = false;
            StopCoroutine(turretShooter.Shooter());
            Debug.Log("should stop turret");
        }

        if(other.tag == "BadBullet")
        {
            playerHealth.HitByProjectile();
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
    }
}
