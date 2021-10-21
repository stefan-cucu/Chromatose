using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        SceneManager.LoadScene("MainMenuScene");
    }
}
