using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    public RoomManager roomManager;
    public Player player;
    public Canvas UI;

    public void InitGame(Camera mainCam)
    {
        Player newPlayer = Instantiate(player, new Vector2(0, 0), Quaternion.identity);

        mainCam.GetComponent<Camera2DFollow>().AttachTarget(newPlayer.gameObject.transform);

        Canvas newUI = Instantiate(UI, new Vector2(0, 0), Quaternion.identity);
        newUI.worldCamera = mainCam;

        Instantiate(roomManager, new Vector2(0, 0), Quaternion.identity);

        newPlayer.Load();
        newPlayer.Spawn();
    }
}
