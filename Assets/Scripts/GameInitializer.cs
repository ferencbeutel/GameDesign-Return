using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    public RoomManager roomManager;
    public Player player;
    public Camera mainCamera;
    public Canvas UI;

    // Use this for initialization
    void Start()
    {
        Player newPlayer = Instantiate(player, new Vector2(0, 0), Quaternion.identity);

        Camera newCam = Instantiate(mainCamera, new Vector3(0, 0, -10), Quaternion.identity);
        newCam.GetComponent<Camera2DFollow>().target = newPlayer.gameObject.transform;

        Canvas newUI = Instantiate(UI, new Vector2(0, 0), Quaternion.identity);
        newUI.worldCamera = newCam;

        Instantiate(roomManager, new Vector2(0, 0), Quaternion.identity);

        newPlayer.Load();
        newPlayer.Spawn();
    }
}
