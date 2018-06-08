using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuInitializer : MonoBehaviour
{

    public Canvas pauseMenuUI;
    Canvas pauseMenuUIInstance;

    public void InitPauseMenuUI()
    {
        if (pauseMenuUIInstance == null)
        {
            Time.timeScale = 0;
            pauseMenuUIInstance = Instantiate(pauseMenuUI, new Vector2(0, 0), Quaternion.identity);
        }
    }
}
