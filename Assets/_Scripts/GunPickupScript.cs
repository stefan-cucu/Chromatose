using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupScript : MonoBehaviour
{
    public GameObject playerGun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerGun.SetActive(true);

            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).GetChild(1).gameObject.GetComponent<Animator>().Play("GlowFadeOut");

            GetComponent<BoxCollider2D>().enabled = false;
            // Destroy(transform.GetChild(0).GetChild(1).gameObject);
        }
    }
}
