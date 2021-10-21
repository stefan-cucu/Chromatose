using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

using TMPro;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menuPanel, levelSelectPanel, optionsPanel, creditsPanel;
    public GameObject volumeSlider, volumeText;
    public GameObject transition;
    public TMP_Dropdown resDropdown;
    public AudioMixer mainMixer;

    private List<Resolution> possibleResolutions = new List<Resolution>();
    private Slider volSlider;
    private Resolution[] resolutions;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        volSlider = volumeSlider.GetComponent<Slider>();

        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int curRes = 0;

        for (int i=0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width % 16 == 0 && resolutions[i].height % 9 == 0)
            {
                if (i == resolutions.Length - 1)
                {
                    options.Add(resolutions[i].width + " x " + resolutions[i].height);
                    possibleResolutions.Add(resolutions[i]);
                }
                else if (i < resolutions.Length - 1 && resolutions[i].width != resolutions[i + 1].width)
                {
                    options.Add(resolutions[i].width + " x " + resolutions[i].height);
                    possibleResolutions.Add(resolutions[i]);
                }
            }

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                curRes = i;
        }

        resDropdown.AddOptions(options);

        resDropdown.value = curRes;
        resDropdown.RefreshShownValue();

        float y;

        if (mainMixer.GetFloat("volume", out y))
        {
            volumeSlider.GetComponent<Slider>().value = y;

            int maxVal = Mathf.FloorToInt(volSlider.maxValue - volSlider.minValue);
            int curVal = Mathf.FloorToInt(y - volSlider.minValue);

            volumeText.GetComponent<TMP_Text>().text = Mathf.FloorToInt(curVal * 100 / maxVal) + "%";
        }
    }

    public void OpenMenuPanel()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        menuPanel.SetActive(true);
    }

    public void OpenLevelSelectPanel()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(false);

        levelSelectPanel.SetActive(true);
    }

    public void OpenOptionsPanel()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        optionsPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        creditsPanel.SetActive(true);
    }

    public void PlayLevel(string levelName)
    {
        StartCoroutine(PlayTheLevel(levelName));
    }

    IEnumerator PlayTheLevel(string levelName)
    {
        transition.GetComponent<Animator>().Play("FadeIn");

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(levelName);
    }

    public void ChangeResolution(int index)
    {
        Resolution resolution = possibleResolutions[index];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);

        int maxVal = Mathf.FloorToInt(volSlider.maxValue - volSlider.minValue);
        int curVal = Mathf.FloorToInt(volume - volSlider.minValue);

        volumeText.GetComponent<TMP_Text>().text = Mathf.FloorToInt(curVal * 100 / maxVal) + "%";
    }

    public void MakeFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
