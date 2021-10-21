using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Bohnea, this is all your fucking fault, cheater
//Asta se intampla cand resetezi nivelul prin a reseta scena
//Shame!

[ExecuteInEditMode]
public class ChangeSpawnPosition : MonoBehaviour
{
    public GameObject player;
    public void ForceChangeSpawn(Vector3 position)
    {
        player.transform.position = position;
    }
}
