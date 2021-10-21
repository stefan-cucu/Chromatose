using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public bool allowUp, allowRight, allowDown, allowLeft;
    public Color laserColor;

    private GameObject up, right, down, left;

    void Start()
    {
        up = transform.GetChild(0).gameObject;
        right = transform.GetChild(1).gameObject;
        down = transform.GetChild(2).gameObject;
        left = transform.GetChild(3).gameObject;

        up.SetActive(allowUp);
        right.SetActive(allowRight);
        down.SetActive(allowDown);
        left.SetActive(allowLeft);

        up.GetComponent<LaserRayBehaviour>().laserColor = laserColor;
        right.GetComponent<LaserRayBehaviour>().laserColor = laserColor;
        down.GetComponent<LaserRayBehaviour>().laserColor = laserColor;
        left.GetComponent<LaserRayBehaviour>().laserColor = laserColor;
    }

    public void ToggleUp()
    {
        up.SetActive(!up.activeSelf);
    }

    public void ToggleRight()
    {
        right.SetActive(!right.activeSelf);
    }

    public void ToggleDown()
    {
        down.SetActive(!down.activeSelf);
    }

    public void ToggleLeft()
    {
        left.SetActive(!down.activeSelf);
    }
}
