using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // private float startTime = 1.0f, neededTime = 1.0f, interpVal;

    private void Update()
    {
        //float value = Mathf.Lerp(startTime, neededTime, Time.unscaledDeltaTime);

        //Time.timeScale = value;
        //Time.fixedDeltaTime = 0.02f * value;
    }

    public void SetTime(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * timeScale;
    }
}
