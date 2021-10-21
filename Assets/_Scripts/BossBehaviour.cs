using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    public GameObject player;
    public GameObject missilePrefab;
    public GameObject winScreen, hpbullets, bosshpscreen;
    public Text tmpscore;
    public float distance, speed, sshootCD;
    public int health;

    public Sprite[] bossSprites;
    public Color[] bossColors;
    [HideInInspector]
    public Color currentColor;
    public TimeManager timeManager;

    [HideInInspector]
    public int t;

    private Rigidbody2D rb;
    private GameObject shield;
    private bool ready = false, shoot = true, immune = false;
    private float shootCD;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentColor = bossColors[0];
        GetComponent<SpriteRenderer>().color = currentColor;
        shield = transform.GetChild(0).gameObject;
        shootCD = sshootCD;
        tmpscore.text = "10/10: ";
    }

    private void FixedUpdate()
    {
        //Debug.Log(Vector2.Distance(transform.position, player.transform.position) + " " + distance);
        if (!ready) return;
        transform.Translate(new Vector3(Time.fixedDeltaTime * 0.1f * speed, 0.0f, 0.0f), Space.World);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= distance)
            ready = true;
        else ready = false;
        if (ready == true)
            bosshpscreen.SetActive(true);
        if (shoot) //  && ready
        {
            StartCoroutine(ShootCD());
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity); //Costel Chites was here
            missile.GetComponent<MissileBehaviour>().enemy = player;
            missile.tag = "Missile";
            missile.GetComponent<SpriteRenderer>().color = currentColor;
        }

        if (transform.position.x < 109)
        {shield.SetActive(false); }
        else if (transform.position.x < 152)
            shield.SetActive(true);
        else if (transform.position.x < 228.5f)
            shield.SetActive(false);
        else if (transform.position.x < 288)
            shield.SetActive(true);
        else shield.SetActive(false);
        immune = shield.activeSelf;

        if (immune) shootCD = sshootCD * 2;
        else shootCD = sshootCD;
    }

    public void LoseHP()
    {
        Debug.Log(health);
        if (immune) return;
        Destroy(hpbullets.transform.GetChild(health - 1).gameObject);
        health--;
        tmpscore.text = health + "/10: ";
        if(health < 1)
        {
            Debug.Log("Hey, nu vreau sa mor");
            gameObject.SetActive(false);
            winScreen.SetActive(true);
            Time.timeScale = 0;

            return;
        }
        int index = Random.Range(0, bossColors.Length);
        currentColor = bossColors[index];
        GetComponent<SpriteRenderer>().color = currentColor;
        Debug.Log(currentColor);
    }

    IEnumerator ShootCD()
    {
        shoot = false;
        yield return new WaitForSeconds(shootCD);
        shoot = true;
    }
}
