using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Static Variables

    public static Color gunColor;
    public static bool endLevel = false;
    public static bool animationIsPlaying = false;
    public static bool stopInput = false;
    public static int nrOfAvailableColors = 0;

    #endregion

    #region Public Variables

    public LayerMask groundMask, shootableMask, destructableBlocksMask;

    public SpriteRenderer playerSprite;
    public Camera mainCam;
    public LineRenderer dottedLine;

    public GameObject player, pivot;
    public GameObject gun;
    public GameObject groundColl, groundColl2;
    public GameObject gunFire;
    public GameObject fireWall;
    public GameObject circleWheel;
    public GameObject thrustParticles;
    public GameObject deathScreen;
    public GameObject pauseMenu;
    public GameObject startText;
    public GameObject hitCircle;
    public GameObject windParticle;
    public GameObject firewallParticles;
    public GameObject enemy;
    public GameObject[] background = new GameObject[5];

    public float speed = 0.1f, thrust = 1.0f;
    public float timeToJump = 0.2f;
    public float backgroundMoveSpeed1, backgroundMoveSpeed2, backgroundMoveSpeed3, backgroundMoveSpeed4, backgroundMoveSpeed5;

    public bool showThrustParticles = false;

    #endregion

    #region Private Variables

    private Ray2D gunRay;
    private RaycastHit2D gunHit;
    private Vector2 offset;

    private Animator playerAnim;
    private Animator screenTransition;
    private LineRenderer gunLine;
    private Rigidbody2D playerRb;
    private GameObject instantiatedWind;
    private TimeManager timeManager;

    private bool ok = false;
    private bool grounded = true;
    private bool holdingSpace = false;
    private bool doubleJump = false;
    private bool canShoot = true, canShoot2 = true;
    private bool goToPivot = false;
    private bool playerIsDead = false;
    // private bool closeCircle = false;

    private int t = 0;

    #endregion

    #region Main Functions

    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        gunLine = gunFire.GetComponent<LineRenderer>();
        playerAnim = player.GetComponent<Animator>();
        playerAnim.SetBool("grounded_1", true);
        screenTransition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();

        gameObject.SetActive(true);
        offset = gun.transform.position - player.transform.position;
        timeManager = this.GetComponent<TimeManager>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1") nrOfAvailableColors = 0;
        else nrOfAvailableColors = 1;

        stopInput = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundColl.transform.position, 0.15f);
        Gizmos.DrawSphere(groundColl2.transform.position, 0.15f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gunFire.transform.position, 0.15f);
    }

    private void FixedUpdate()
    {
        // Debug.Log(stopInput);

        // Check for Ground

        {
            groundedCheck();

            if (grounded)
            {
                playerAnim.SetBool("grounded_1", true);
            }
            else
            {
                playerAnim.SetBool("grounded_1", false);
            }

        }


        // Background Follow
        {
            background[0].transform.position = new Vector2(mainCam.transform.position.x * backgroundMoveSpeed1, background[0].transform.position.y);
            background[1].transform.position = new Vector2(mainCam.transform.position.x * backgroundMoveSpeed2, background[1].transform.position.y);
            background[2].transform.position = new Vector2(mainCam.transform.position.x * backgroundMoveSpeed3, background[2].transform.position.y);
            background[3].transform.position = new Vector2(mainCam.transform.position.x * backgroundMoveSpeed4, background[3].transform.position.y);
            background[4].transform.position = new Vector2(mainCam.transform.position.x * backgroundMoveSpeed5, background[4].transform.position.y);
        }


        // Camera and Player movement - constant

        {
            if (ok && !playerIsDead)
            {
                t++;

                t = Mathf.Clamp(t, 0, 30);

                pivot.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f));
                player.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f), Space.World);
                mainCam.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f));
                fireWall.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f));
                firewallParticles.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f), Space.World);

                if (instantiatedWind) instantiatedWind.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f), Space.World);

                if (player.transform.position.x < pivot.transform.position.x)
                    player.transform.Translate(new Vector3(t * Time.fixedDeltaTime * 0.03f * speed, 0.0f, 0.0f), Space.World);
                else if (player.transform.position.x > pivot.transform.position.x + 0.2f && !goToPivot)
                {
                    goToPivot = true;

                    playerRb.velocity = new Vector2(0.0f, playerRb.velocity.y);
                }
                else if (goToPivot)
                {
                    if (player.transform.position.x < pivot.transform.position.x + 0.1f)
                        goToPivot = false;
                    else player.transform.Translate(new Vector3(-t * Time.fixedDeltaTime * 0.06f * speed, 0.0f, 0.0f), Space.World);
                }
            }
        }


        // Keep Gun on Player

        {
            gun.transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);
        }
    }

    #endregion

    #region Update Function

    void Update()
    {
        // Check if Dead

        {
            //if (endLevel)
            //{
            //    deathScreen.SetActive(true);

            //    gun.SetActive(false);

            //    TimeManager.SetTime(0.0f);
            //}
        }


        // Start Level - Q

        {
            if (Input.GetKeyDown(KeyCode.Q) && !animationIsPlaying)
            {
                startText.GetComponent<Animator>().SetBool("Disappear", true);
                playerAnim.SetBool("start", true);
                ok = true;
            }
        }

        
        // Jumping - Spacebar

          
        {
            if (!stopInput)
            {
                if (grounded)
                {
                    doubleJump = true;
                    showThrustParticles = false;
                   

                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                   
                    StartCoroutine(Spacebar());
                    
                }
                else if (Input.GetKeyUp(KeyCode.Space)) holdingSpace = false;

                if (holdingSpace && grounded)
                {
                    player.GetComponents<AudioSource>()[1].Play();
                    Jump();
                   // playerAnim.SetBool("grounded_1", false);
                   // playerAnim.Play("first_jump");
                    // doubleJump = true;
                    holdingSpace = false;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !grounded)
                {
                    player.GetComponents<AudioSource>()[2].Play();
                    Jump();

                    playerAnim.Play("Jump");
                    doubleJump = false;
                }
            }
        }


        // Shooting - M1

        {
            if (Input.GetMouseButtonDown(0) && canShoot && canShoot2 && !stopInput && gun.activeSelf)
            {
                /// Needs Improvement
                // playerRb.AddForce(-gun.transform.right * 100.0f);

                if (nrOfAvailableColors == 0) ShootNothing();
                else if (nrOfAvailableColors > 0) Shoot();
            }

            DrawDottedLine();
        }

        // Color Wheel - M2

        {
            if (nrOfAvailableColors > 0 || SceneManager.GetActiveScene().name == "DemoScene")
            {
                if (Input.GetMouseButtonDown(1) && !stopInput)
                {
                    // closeCircle = false;
                    timeManager.SetTime(0.1f);

                    circleWheel.SetActive(true);
                    circleWheel.transform.GetChild(0).GetComponent<ColorSelectionScript>().ClearColors();
                    canShoot2 = false;
                }
                else if (Input.GetMouseButtonUp(1) && !stopInput)
                {
                    timeManager.SetTime(1.0f);
                    // closeCircle = true;

                    circleWheel.SetActive(false);
                    canShoot2 = true;
                }
            }

            //if (closeCircle)
            //{
            //    float val = Mathf.Lerp(0.1f, 1.0f, 0.5f);

            //    TimeManager.SetTime(val);
            //}
        }


        // Thrust - L Ctrl

        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !grounded && !stopInput) ThrustDown();

            if (showThrustParticles) thrustParticles.SetActive(true);
            else thrustParticles.SetActive(false);
        }


        // Reset Scene - T

        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                timeManager.SetTime(1.0f);

                endLevel = false;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }


        // Back to Main Menu - Esc

        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.activeSelf == false)
                {
                    PauseGame();
                }
                else
                {
                    UnpauseGame();
                }
            }
        }


        // Rotate Gun - constant

        {
            rotateGun();
        }


        // Other

        {
            gunColor = gunLine.startColor;

            if (gunFire.transform.position.x < player.transform.position.x)
            {
                gun.GetComponent<SpriteRenderer>().flipY = true;
                playerSprite.flipX = true;
            }
            else
            {
                gun.GetComponent<SpriteRenderer>().flipY = false;
                playerSprite.flipX = false;
            }
        }
    }

    #endregion

    #region Other Functions

    public IEnumerator Kill()
    {
        playerIsDead = true;
        playerRb.bodyType = RigidbodyType2D.Static;
        player.GetComponent<Animator>().Play("Dead");
        gun.GetComponent<SpriteRenderer>().enabled = false;
        playerAnim.SetBool("start", false);
        showThrustParticles = false;

       yield return new WaitForSeconds(0.5f);

        screenTransition.SetBool("Cover", true);

        yield return new WaitForSeconds(0.3f);

        ok = false;

        // Should be a black screen now

        if (SceneManager.GetActiveScene().name == "Level3" || SceneManager.GetActiveScene().name == "BossLevel" || SceneManager.GetActiveScene().name == "LegacyLevel")
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            GameObject.FindGameObjectWithTag("SpawnpointManager").GetComponent<StartpointScript>().RestartPositions();
            player.GetComponent<Animator>().Play("Idle");
            gun.GetComponent<SpriteRenderer>().enabled = true;


            yield return new WaitForSeconds(0.3f);

            startText.GetComponent<Animator>().SetBool("Disappear", false);
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            playerIsDead = false;

            screenTransition.SetBool("Cover", false);
        }
    }

    public void PauseGame()
    {
        timeManager.SetTime(0.0f);

        pauseMenu.SetActive(true);

        stopInput = true;
    }

    public void UnpauseGame()
    {
        timeManager.SetTime(1.0f);

        pauseMenu.SetActive(false);

        stopInput = false;
    }

    public void ResetLevel()
    {
        timeManager.SetTime(1.0f);

        endLevel = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        UnpauseGame();

        SceneManager.LoadScene("MainMenuScene");
    }

    void ThrustDown()
    {
        CancelMomentum();

        showThrustParticles = true;

        if (!grounded)
        {
            playerRb.AddForce(new Vector2(0.0f, -1.5f * thrust));
        }
    }

    IEnumerator DisableEffects()
    {
        yield return null;
        yield return null;

        gunLine.enabled = false;

        yield return new WaitForSeconds(0.3f);

        canShoot = true;
    }

    void ShootNothing()
    {
        canShoot = false;

        instantiatedWind = Instantiate(windParticle, gunFire.transform.position, gun.transform.rotation);
        gun.GetComponent<Animator>().Play("GunAnimation");
        gun.GetComponent<AudioSource>().Play();

        StartCoroutine(DisableEffects());
    }

    void Shoot()
    {
        canShoot = false;
        if (gun.GetComponent<AudioSource>()) gun.GetComponent<AudioSource>().Play();

        StartCoroutine(DisableEffects());

        gunLine.enabled = true;
        gunLine.SetPosition(0, gunFire.transform.position);

        gunRay.origin = gunFire.transform.position;
        gunRay.direction = gunFire.transform.right;

        gunHit = Physics2D.Raycast(gunFire.transform.position, gunFire.transform.right, 1000.0f, shootableMask);

        if (gunHit)
        {
            gunLine.SetPosition(1, gunHit.point);

            GameObject instantiatedHitCircle = null;

            instantiatedHitCircle = Instantiate(hitCircle, gunHit.point, Quaternion.identity);
            instantiatedHitCircle.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = gunColor;
            StartCoroutine(DestroyCircle(instantiatedHitCircle));

            Debug.Log(gunHit.distance + "m is pretty impressive!");
            Debug.Log(gunHit.collider.gameObject.name + " with tag " + gunHit.collider.gameObject.tag);

            #region Tilemap Attempt

            //if (gunHit.collider.gameObject.GetComponent<Tilemap>())
            //{
            //    Tilemap dBlocks = gunHit.collider.gameObject.GetComponent<Tilemap>();

            //    Collider2D[] destructableBlocks = Physics2D.OverlapCircleAll(gunHit.point, 2.0f, destructableBlocksMask);

            //    for (int i = 0; i < destructableBlocks.Length; i++)
            //    {
            //        Debug.Log("Destroying " + destructableBlocks[i].gameObject.name);

            //        Destroy(destructableBlocks[i].gameObject);
            //    }
            //}

            #endregion
        }
        else
        {
            gunLine.SetPosition(1, gunRay.origin + gunRay.direction * 10000.0f);
        }
    }

    void DrawDottedLine()
    {
        dottedLine.SetPosition(0, dottedLine.gameObject.transform.position);
        Ray2D dotRay = new Ray2D();
        dotRay.origin = dottedLine.transform.position;
        dotRay.direction = dottedLine.transform.right;

        RaycastHit2D dotHit = Physics2D.Raycast(dottedLine.gameObject.transform.position, dottedLine.gameObject.transform.right, 1000.0f, shootableMask);
        if (dotHit)
        {
            dottedLine.SetPosition(1, dotHit.point);
        }
        else
            dottedLine.SetPosition(1, dotRay.origin + dotRay.direction * 10000f);
        dottedLine.material.mainTextureScale = new Vector2((int)Vector2.Distance(dottedLine.GetPosition(0), dottedLine.GetPosition(1)), 1);
    }

    IEnumerator DestroyCircle(GameObject instantiatedCircle)
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(instantiatedCircle);
    }

    void Jump()
    {
        CancelMomentum();

        showThrustParticles = false;
        playerRb.AddForce(new Vector2(0.0f, thrust));
    }

    public void CancelMomentum()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = 0.0f;
    }

    IEnumerator Spacebar()
    {
        holdingSpace = true;

        yield return new WaitForSeconds(timeToJump);

        holdingSpace = false;
    }

    void groundedCheck()
    {
        RaycastHit2D groundRayhit1 = Physics2D.Raycast(groundColl.transform.position, Vector2.down, 0.1f, groundMask),
            groundRayhit2 = Physics2D.Raycast(groundColl2.transform.position, Vector2.down, 0.1f, groundMask);

        // Debug.Log("Grounded1 test: " + groundRayhit1.collider.name + "\nGrounded2 test: " + groundRayhit2.collider.name);

        if (groundRayhit1 || groundRayhit2) grounded = true;
        else grounded = false;
    }

    void groundedCheck2()
    {
        bool grounded1 = Physics2D.OverlapCircle(groundColl.transform.position, 0.15f, groundMask),
            grounded2 = Physics2D.OverlapCircle(groundColl2.transform.position, 0.15f, groundMask);

        Debug.Log("Grounded1 test: " + grounded1 + "\nGrounded2 test: " + grounded2);

        if (grounded1 || grounded2) grounded = true;
        else grounded = false;
    }

    void rotateGun()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCam.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - gun.transform.position.x, mousePos.y - gun.transform.position.y);

        gun.transform.right = dir;
    }

    #endregion
}
