using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleScript : MonoBehaviour
{
    public float speed = 2.5f;

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.fixedDeltaTime * speed);
    }
}
