using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public bool playStartAnim;
    public GameObject player, startText;

    private StartpointScript startpointScript;

    void Start()
    {
        startpointScript = GameObject.FindGameObjectWithTag("SpawnpointManager").GetComponent<StartpointScript>();

        if (playStartAnim == true && startpointScript.currentStartpoint.name == "Spawnpoint")
        {
            PlayerMovement.animationIsPlaying = true;
            PlayerMovement.stopInput = true;

            player.SetActive(false);
            startText.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopStartAnim()
    {
        player.SetActive(true);
        startText.SetActive(true);

        PlayerMovement.animationIsPlaying = false;
        PlayerMovement.stopInput = false;

        Destroy(gameObject);
    }
}
