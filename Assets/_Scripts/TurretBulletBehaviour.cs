using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    [HideInInspector]
    public GameObject target;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(-Time.fixedDeltaTime * 0.1f * speed, 0f, 0f), Space.World);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>().Kill());
        }
        else if (collision.gameObject.tag == "Firewall")
            Destroy(gameObject);
    }
}
