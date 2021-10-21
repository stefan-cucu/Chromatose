using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform maxLeft, maxRight;
    public Color enemyColor;
    public float enemySpeed, thrust;

    private float pos;
    private float patrolEnd, patrolBegin;
    private Rigidbody2D rb;
    private bool ok = true;

    void Start()
    {
        patrolBegin = maxLeft.position.x;
        patrolEnd = maxRight.position.x;

        pos = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Enemy movement
        pos = transform.position.x;
        if (pos > patrolEnd)
        {
            rb.velocity = Vector2.zero;
            ok = false;

            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (pos < patrolBegin)
        {
            rb.velocity = Vector2.zero;
            ok = true;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (ok)
        {           
            transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }


        //if (CheckWall()) //jump
        //    rb.AddForce(Vector2.up * thrust);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.SetActive(false);
            // PlayerMovement.endLevel = true;
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
        }
        else if (collision.gameObject.tag == "Firewall")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }



    //Check if enemy blocked by wall

    /*
     
    bool CheckWall()
    {
        if (ok == true)
        {
            GameObject point = transform.GetChild(0).gameObject;
            RaycastHit2D hit = Physics2D.Raycast(point.transform.position, Vector2.right, 1f);
            if (hit.collider != null)
            {
                // Debug.Log(hit.collider.name);
                return true;
            }

            return false;
        }
        else
        {
            GameObject point = transform.GetChild(1).gameObject;
            RaycastHit2D hit = Physics2D.Raycast(point.transform.position, Vector2.left, 1f);
            if (hit.collider != null)
                return true;
            return false;
        }
    }

    */
}
