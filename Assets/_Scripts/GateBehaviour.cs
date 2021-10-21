using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    public GameObject correspondingLever;
    public Sprite offSprite, onSprite;
    //public float speed;

    private Transform gate1, gate2;
    private float pos;
    private bool wasActivated = false;

    void Start()
    {
        gate1 = transform.GetChild(0);
        gate2 = transform.GetChild(1);
        pos = gate1.position.y;
    }

    public void Activate()
    {
        if (!wasActivated) {
            wasActivated = true;
            correspondingLever.GetComponent<SpriteRenderer>().sprite = onSprite;

            GetComponent<BoxCollider2D>().enabled = false;
            gate1.Translate(Vector2.up * 2);
            gate2.Translate(Vector2.down * 2);
        }
        else
        {
            wasActivated = false;
            correspondingLever.GetComponent<SpriteRenderer>().sprite = offSprite;

            GetComponent<BoxCollider2D>().enabled = true;
            gate1.Translate(Vector2.down * 2);
            gate2.Translate(Vector2.up * 2);
        }
    }
}
