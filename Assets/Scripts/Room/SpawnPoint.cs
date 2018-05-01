using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Room room;
    RoomManager roomManager = null;

    public void Spawn()
    {
        if (roomManager == null)
        {
            roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        }
        roomManager.LoadRoomFromSpawnPoint(this);
    }

    private void Start()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }
}
