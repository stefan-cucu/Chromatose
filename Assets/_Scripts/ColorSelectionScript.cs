using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectionScript : MonoBehaviour
{
    public ColorChangeScript ccsImg, ccsLine;
    public Button buttonBlue, buttonGreen, buttonRed, buttonClear;
    public float timeBetweenColors = 1f;
    public Image Color1, Color2, ColorResult;
    public GameObject plus, equals;
    public Image selectedColor;

    private bool blue = false, green = false, red = false;
    
    void Start()
    {
        buttonBlue.onClick.AddListener(SetBlueOn);
        buttonGreen.onClick.AddListener(SetGreenOn);
        buttonRed.onClick.AddListener(SetRedOn);
        buttonClear.onClick.AddListener(ClearColors);
    }

    private void SetBlueOn()
    {
        blue = true;
    }

    private void SetRedOn()
    {
        red = true;
    }

    private void SetGreenOn()
    {
        green = true;
    }

    public void ClearColors()
    {
        red = false; blue = false; green = false;
        Color1.gameObject.SetActive(false);
        Color2.gameObject.SetActive(false);
        plus.SetActive(false);
        ColorResult.gameObject.SetActive(false);
        equals.SetActive(false);
    }
    void Update()
    {
        if (blue && !green && !red)
        {
            Color1.color = Color.blue;
            Color1.gameObject.SetActive(true);
            ccsImg.ChangeColorToBlue();
            ccsLine.LineChangeColorToBlue();
            selectedColor.color = Color.blue;
        }
        else if (!blue && green && !red)
        {
            Color1.color = Color.green;
            Color1.gameObject.SetActive(true);
            ccsImg.ChangeColorToGreen();
            ccsLine.LineChangeColorToGreen();
            selectedColor.color = Color.green;
        }
        else if (!blue && !green && red)
        {
            Color1.color = Color.red;
            Color1.gameObject.SetActive(true);
            ccsImg.ChangeColorToRed();
            ccsLine.LineChangeColorToRed();
            selectedColor.color = Color.red;
        }
        else if (!blue && green && red)
        {
            if (Color1.color == Color.green)
                Color2.color = Color.red;
            else Color2.color = Color.green;
            Color2.gameObject.SetActive(true);
            Color1.gameObject.SetActive(true);
            plus.SetActive(true);
            equals.SetActive(true);
            ColorResult.color = Color.yellow;
            ColorResult.gameObject.SetActive(true);
            ccsImg.ChangeColorToYellow();
            ccsLine.LineChangeColorToYellow();
            selectedColor.color = Color.yellow;
        }

        else if(blue && !green && red)
        {
            if (Color1.color == Color.blue)
                Color2.color = Color.red;
            else Color2.color = Color.blue;
            Color2.gameObject.SetActive(true);
            Color1.gameObject.SetActive(true);
            plus.SetActive(true);
            equals.SetActive(true);
            ColorResult.color = Color.magenta;
            ColorResult.gameObject.SetActive(true);
            ccsImg.ChangeColorToMagenta();
            ccsLine.LineChangeColorToMagenta();
            selectedColor.color = Color.magenta;
        }

        else if(blue && green && !red)
        {
            if (Color1.color == Color.blue)
                Color2.color = Color.green;
            else Color2.color = Color.blue;
            Color2.gameObject.SetActive(true);
            Color1.gameObject.SetActive(true);
            plus.SetActive(true);
            equals.SetActive(true);
            ColorResult.color = Color.cyan;
            ColorResult.gameObject.SetActive(true);
            ccsImg.ChangeColorToCyan();
            ccsLine.LineChangeColorToCyan();
            selectedColor.color = Color.cyan;
        }
    }
}
