using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    public UnityEvent Event;
    public float waitTime;
    public Color btnColor;

    void Awake()
    {
        if(Event == null)
        {
            Event = new UnityEvent();
            Debug.Log(gameObject.name + " doesn't have an UnityEvent attached, should be fixed immediately");
        }
        GetComponent<SpriteRenderer>().color = btnColor;
    }

    public void Activate()
    {
        Event.Invoke();
        StartCoroutine(btnWait());
    }

    IEnumerator btnWait()
    {
        yield return new WaitForSeconds(waitTime);
        Event.Invoke();
    }

}
