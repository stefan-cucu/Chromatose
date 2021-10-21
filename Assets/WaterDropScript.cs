using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropScript : MonoBehaviour
{
    public GameObject screen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { screen.SetActive(true); Time.timeScale = 0f; }
    }
}
