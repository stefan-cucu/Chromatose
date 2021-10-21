using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeScript : MonoBehaviour
{
    public void ChangeColorToRed()
    {
        this.GetComponent<Image>().color = Color.red;
    }

    public void ChangeColorToGreen()
    {
        this.GetComponent<Image>().color = Color.green;
    }

    public void ChangeColorToBlue()
    {
        this.GetComponent<Image>().color = Color.blue;
    }

    public void ChangeColorToYellow()
    {
        this.GetComponent<Image>().color = new Color(1, 1, 0);
    }

    public void ChangeColorToMagenta()
    {
        this.GetComponent<Image>().color = Color.magenta;
    }

    public void ChangeColorToCyan()
    {
        this.GetComponent<Image>().color = Color.cyan;
    }


    public void LineChangeColorToRed()
    {
        this.GetComponent<LineRenderer>().startColor = Color.red;
        this.GetComponent<LineRenderer>().endColor = Color.red;
    }

    public void LineChangeColorToGreen()
    {
        this.GetComponent<LineRenderer>().startColor = Color.green;
        this.GetComponent<LineRenderer>().endColor = Color.green;
    }

    public void LineChangeColorToBlue()
    {
        this.GetComponent<LineRenderer>().startColor = Color.blue;
        this.GetComponent<LineRenderer>().endColor = Color.blue;
    }

    public void LineChangeColorToYellow()
    {
        this.GetComponent<LineRenderer>().startColor = new Color(1, 1, 0);
        this.GetComponent<LineRenderer>().endColor = new Color(1, 1, 0);
    }

    public void LineChangeColorToMagenta()
    {
        this.GetComponent<LineRenderer>().startColor = Color.magenta;
        this.GetComponent<LineRenderer>().endColor = Color.magenta;
    }

    public void LineChangeColorToCyan()
    {
        this.GetComponent<LineRenderer>().startColor = Color.cyan;
        this.GetComponent<LineRenderer>().endColor = Color.cyan;
    }
}
