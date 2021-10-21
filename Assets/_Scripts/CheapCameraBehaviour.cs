using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapCameraBehaviour : MonoBehaviour
{
    public GameObject mCamera;
    public GameObject player;
    private Vector3 offset;

    private void Start()
    {
        offset = player.transform.position - mCamera.transform.position;
    }

    void FixedUpdate()
    {
        mCamera.transform.position = player.transform.position - offset;
    }
}
