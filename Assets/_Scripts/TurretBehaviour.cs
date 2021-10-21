using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject bulletPrefab;
    public float distance, fireRate;

    private GameObject spawnPoint;
    private bool isReadyToFire = true;
    private bool isInRange = false;
    void Start()
    {
        spawnPoint = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > distance)
            return;
        if (isReadyToFire)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        isReadyToFire = false;
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform);
        bulletPrefab.GetComponent<TurretBulletBehaviour>().target = player;
        yield return new WaitForSeconds(fireRate);
        isReadyToFire = true;
    }
}
