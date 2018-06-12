using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverInitializer : MonoBehaviour
{
    public CanvasGroup gameOverUi;

    public void InitGameOver()
    {
        CanvasGroup gameOverUiInstance = Instantiate(gameOverUi, new Vector2(0, 0), Quaternion.identity);
        gameOverUiInstance.alpha = 1;
        Invoke("Quit", 5.0f);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
