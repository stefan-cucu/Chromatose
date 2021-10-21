using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlatformBehaviour : MonoBehaviour
{
    public float distance, speed;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private bool direction = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        //Debug.Log(startPos.x + " " + (startPos.x + distance) + " " + pos.x);

        if (pos.x <= startPos.x)
            {direction = true; }
        if (pos.x >= startPos.x + distance)
            {direction = false; }
        if (direction)
        {
            rb.velocity = Vector3.zero;
            transform.Translate(Vector3.right * Time.fixedDeltaTime * 40.0f * speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.Translate(Vector3.left * Time.fixedDeltaTime * 40.0f * speed);
        }
    }
}
