using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrbBehaviour : MonoBehaviour
{
    public float thrust;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());

        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * thrust);
        }
    }
}
