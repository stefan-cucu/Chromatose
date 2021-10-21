using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverBehaviour : MonoBehaviour
{
    public UnityEvent Event;
    public Color leverColor;

    void Awake()
    {
        if (Event == null)
        {
            Event = new UnityEvent();
            Debug.Log(gameObject.name + " doesn't have an UnityEvent attached, should be fixed immediately");
        }
        GetComponent<SpriteRenderer>().color = leverColor;
    }

    public void Activate()
    {
        Event.Invoke();
    }
}
