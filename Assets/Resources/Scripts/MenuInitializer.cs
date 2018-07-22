using UnityEngine;

public class MenuInitializer : MonoBehaviour
{

    public Canvas menuUI;
    public Camera mainCam;

    Camera cameraInstance;

    public void LoadMenu()
    {
        Canvas menuUIInstance = Instantiate(menuUI, new Vector2(0, 0), Quaternion.identity);
        menuUIInstance.worldCamera = cameraInstance;
    }

    // Use this for initialization
    void Start()
    {
        cameraInstance = Instantiate(mainCam, new Vector3(0, 0, -10), Quaternion.identity);
        LoadMenu();
    }
}
