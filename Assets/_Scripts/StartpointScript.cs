using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartpointScript : MonoBehaviour
{
    public Transform spawnpoint, currentStartpoint;
    public GameObject player, pivot, gun, firewall, firewallParticles;
    public bool hasGun;

    private Vector3 gunOffset, firewallOffset, pivotOffset, firewallParticlesOffset;

    void Start()
    {
        if (hasGun == true) gun.SetActive(true);

        gunOffset = gun.transform.position - spawnpoint.position;
        pivotOffset = pivot.transform.position - spawnpoint.position;
        firewallOffset = firewall.transform.position - spawnpoint.position;
        firewallParticlesOffset = firewallParticles.transform.position - spawnpoint.position;

        player.transform.position = currentStartpoint.position;
        gun.transform.position = currentStartpoint.position + gunOffset;
        pivot.transform.position = currentStartpoint.position + pivotOffset;
        firewall.transform.position = currentStartpoint.position + firewallOffset;
        firewallParticles.transform.position = currentStartpoint.position + firewallParticlesOffset;
    }

    public void RestartPositions()
    {
        player.transform.position = currentStartpoint.position;
        gun.transform.position = currentStartpoint.position + gunOffset;
        pivot.transform.position = currentStartpoint.position + pivotOffset;
        firewall.transform.position = currentStartpoint.position + firewallOffset;
        firewallParticles.transform.position = currentStartpoint.position + firewallParticlesOffset;
    }
}
