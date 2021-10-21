using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirewallBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
        }
    }
}
