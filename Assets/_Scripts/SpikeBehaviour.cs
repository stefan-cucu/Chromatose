using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
            // collision.gameObject.SetActive(false);
            PlayerMovement.endLevel = true;
        }
    }
}
