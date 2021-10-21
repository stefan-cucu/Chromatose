using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public float speed, rotateSpeed;
    public GameObject enemy;

    private BoxCollider2D missileCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        Debug.Log("Am existat, rip " + gameObject.name);
        rb = GetComponent<Rigidbody2D>();
        missileCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 dir = rb.position - (Vector2)enemy.transform.position;

        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.right).z;

        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(missileCollider, collision.collider);
            return;
        }
        else
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
            PlayerMovement.endLevel = true;
        }
    }
}
