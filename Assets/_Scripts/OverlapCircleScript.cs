using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverlapCircleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Circle collided with " + collider.name);
        if (collider.tag == "Enemy")
            Debug.Log(PlayerMovement.gunColor + " " + collider.GetComponent<EnemyBehaviour>().enemyColor);

        if (collider.tag == "Enemy" && collider.GetComponent<EnemyBehaviour>().enemyColor == PlayerMovement.gunColor)
        {
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "DestructableBlock" && collider.gameObject.GetComponent<SpriteRenderer>().color == PlayerMovement.gunColor)
        {
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "Button" && collider.gameObject.GetComponent<ButtonBehaviour>().btnColor == PlayerMovement.gunColor)
        {
            collider.gameObject.GetComponent<ButtonBehaviour>().Activate();
        }
        else if (collider.tag == "Lever" && collider.gameObject.GetComponent<LeverBehaviour>().leverColor == PlayerMovement.gunColor)
        {
            collider.gameObject.GetComponent<LeverBehaviour>().Activate();
        }
        else if (collider.tag == "Boss" && collider.gameObject.GetComponent<BossBehaviour>().currentColor == PlayerMovement.gunColor)
        {
            Debug.Log("Zi si tu ceva");
            collider.gameObject.GetComponent<BossBehaviour>().LoseHP();
        }
        else if (collider.tag == "Missile" && collider.gameObject.GetComponent<SpriteRenderer>().color == PlayerMovement.gunColor)
            Destroy(collider.gameObject);
    }

}
