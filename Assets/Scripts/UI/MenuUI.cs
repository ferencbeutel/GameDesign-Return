using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    public Button startButton;
    public Button optionsButton;
    public Button exitButton;

    public Button muteButton;
    public Button aboutButton;
    public Button backButton;

    public Button aboutBackButton;

    GameInitializer gameInitializer;
    CanvasGroup thisCanvasGroup;
    Camera mainCam;

    private void Start()
    {
        gameInitializer = GameObject.FindGameObjectWithTag("gameInitializer").GetComponent<GameInitializer>();
        thisCanvasGroup = GetComponent<CanvasGroup>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        startButton.onClick.AddListener(OnStartButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);

        muteButton.onClick.AddListener(OnMuteButtonClick);
        aboutButton.onClick.AddListener(OnAboutButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);

        aboutBackButton.onClick.AddListener(OnAboutBackButtonClick);
    }

    private void OnStartButtonClick()
    {
        thisCanvasGroup.alpha = 0f;
        gameInitializer.InitGame(mainCam);
    }

    private void OnOptionsButtonClick()
    {
        optionsButton.transform.parent.gameObject.SetActive(false);
        backButton.transform.parent.gameObject.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
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

    private void OnAboutButtonClick()
    {
        backButton.transform.parent.gameObject.SetActive(false);
        aboutBackButton.transform.parent.gameObject.SetActive(true);
    }

    private void OnBackButtonClick()
    {
        backButton.transform.parent.gameObject.SetActive(false);
        optionsButton.transform.parent.gameObject.SetActive(true);
    }

    private void OnAboutBackButtonClick()
    {
        aboutBackButton.transform.parent.gameObject.SetActive(false);
        backButton.transform.parent.gameObject.SetActive(true);
    }
}
