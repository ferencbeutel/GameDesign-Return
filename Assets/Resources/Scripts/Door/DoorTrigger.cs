using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorTrigger : MonoBehaviour
{
    public Room room;
    public DoorTrigger destination;
    public bool isEntering = false;

    RoomManager roomManager;

    public abstract void CloseDoor();
    public abstract void OpenDoor();

    private void Start()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            if (!isEntering)
            {
                isEntering = true;
                roomManager.LoadRoom(destination);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            isEntering = false;
        }
    }
}
