using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider_Control : MonoBehaviour
{
    public GameObject victoryScreen;
    public StartpointScript spawner;
    public Sprite sprite1;
    public SpriteRenderer sprite_r;
   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.name == "Apple")
        {
            victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (this.gameObject.tag == "SpawnPoint")
        {
            if (col.name== "Player")
            {
                spawner.currentStartpoint = this.transform;
                sprite_r.sprite = sprite1;
            }
        }
    }
}
