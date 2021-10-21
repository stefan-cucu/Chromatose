using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingEnemyBehaviour: EnemyBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
            ////collision.gameObject.SetActive(false);
            PlayerMovement.endLevel = true;
        }
    }
}
