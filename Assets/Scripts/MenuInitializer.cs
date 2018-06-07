using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{

    public Canvas menuUI;
    public Camera mainCam;

    // Use this for initialization
    void Start()
    {
        Canvas menuUIInstance = Instantiate(menuUI, new Vector2(0, 0), Quaternion.identity);
        Camera newCam = Instantiate(mainCam, new Vector3(0, 0, -10), Quaternion.identity);
        menuUIInstance.worldCamera = newCam;
    }
}
