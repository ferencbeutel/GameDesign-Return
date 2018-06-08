using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public Button continueButton;
    public Button muteButton;
    public Button exitButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClick);
        muteButton.onClick.AddListener(OnMuteButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

    }

    private void OnContinueButtonClick()
    {
        Time.timeScale = 1f;
        Object.Destroy(gameObject);
    }

    private void OnMuteButtonClick()
    {
        AudioListener.volume = 1 - AudioListener.volume;
        if (AudioListener.volume > 0)
        {
            muteButton.GetComponentInChildren<Text>().text = "MUTE";
        }
        else
        {
            muteButton.GetComponentInChildren<Text>().text = "UNMUTE";
        }
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
