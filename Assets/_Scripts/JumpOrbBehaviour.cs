using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrbBehaviour : MonoBehaviour
{
    public float thrust;

    private PlayerMovement gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GetComponent<Animator>().Play("JumpOrbAnim");

    //        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
    //        gameManager.CancelMomentum();
    //        rb.AddForce(Vector2.up * thrust);
    //    }
    //    else
    //    {
    //        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().Play("JumpOrbAnim");

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            gameManager.CancelMomentum();
            rb.AddForce(Vector2.up * thrust);

            gameObject.GetComponent<AudioSource>().Play();

            collision.gameObject.GetComponent<PlayerMovement>().showThrustParticles = false;
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
