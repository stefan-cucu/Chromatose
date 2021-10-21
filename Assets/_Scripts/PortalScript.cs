using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Vector3 destination;

    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.position = destination;

        if (gameObject.GetComponent<AudioSource>()) gameObject.GetComponent<AudioSource>().Play();
    }
}
