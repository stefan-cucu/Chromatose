using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravityScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool top = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale *= -1;
            if(top == false)
            {
                collision.gameObject.transform.Rotate(Vector3.up, 180f);
            }
            else
            {
                collision.gameObject.transform.Rotate(Vector3.zero);
            }
            top = !top;
        }
    }
}
