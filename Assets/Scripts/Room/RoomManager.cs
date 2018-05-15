using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    List<Room> loadedRooms = new List<Room>();
    Room activeRoom;
    Camera mainCam;
    GameObject player;
    Player2D player2D;
    Image blackOverlay;

    public void LoadRoomFromSpawnPoint(SpawnPoint spawnPoint)
    {
        StartCoroutine(LoadRoomFromSpawnPointIntern(spawnPoint));
    }

    public void LoadRoom(DoorTrigger destination)
    {
        StartCoroutine(LoadRoomIntern(destination));
    }

    IEnumerator LoadRoomFromSpawnPointIntern(SpawnPoint spawnPoint)
    {
        Debug.Log("loading new room from spawnPoint");
        player2D.LockMovement();
        LoadRoom(spawnPoint.room);

        player.transform.position = spawnPoint.gameObject.transform.position;
        UpdateCamera(spawnPoint.gameObject.transform);

        blackOverlay.CrossFadeAlpha(0, .5f, false);
        yield return new WaitForSeconds(.5f);
        player2D.UnlockMovement();
    }

    IEnumerator LoadRoomIntern(DoorTrigger destination)
    {
        Debug.Log("loading new room");
        player2D.LockMovement();
        blackOverlay.CrossFadeAlpha(1, .5f, false);
        yield return new WaitForSeconds(.5f);
        LoadRoom(destination.room);
        foreach (Transform childTransform in activeRoom.transform)
        {
            DoorTrigger childDoorComponent = childTransform.gameObject.GetComponent<DoorTrigger>();
            if (childDoorComponent != null && destination.name == childDoorComponent.name)
            {
                childDoorComponent.isEntering = true;
                childDoorComponent.OpenDoor();
            }
        }

        player.transform.position = destination.gameObject.transform.position;
        UpdateCamera(destination.gameObject.transform);
        blackOverlay.CrossFadeAlpha(0, .5f, false);
        yield return new WaitForSeconds(.5f);
        player2D.UnlockMovement();
    }

    void LoadRoom(Room room)
    {
        Room newRoom = Instantiate(room, new Vector2(0, 0), Quaternion.identity).GetComponent<Room>();
        activeRoom = newRoom;
        loadedRooms.Add(newRoom);
    }

    void UpdateCamera(Transform destination)
    {
        float minX = 100000;
        float maxX = -100000;
        float minY = 100000;
        float maxY = -100000;
        foreach (Transform childTransform in activeRoom.gameObject.transform)
        {
            if (childTransform.gameObject.tag == "Background")
            {
                continue;
            }

            Renderer renderer = childTransform.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                float rendererMinX = renderer.bounds.min.x;
                float rendererMinY = renderer.bounds.min.y;
                float rendererMaxX = renderer.bounds.max.x;
                float rendererMaxY = renderer.bounds.max.y;

                if (rendererMinX < minX)
                {
                    minX = rendererMinX;
                }
                if (rendererMinY < minY)
                {
                    minY = rendererMinY;
                }
                if (rendererMaxX > maxX)
                {
                    maxX = rendererMaxX;
                }
                if (rendererMaxY > maxY)
                {
                    maxY = rendererMaxY;
                }
            }
        }

        Camera2DFollow cameraFollowComponent = mainCam.GetComponent<Camera2DFollow>();

        cameraFollowComponent.lowerBounds = new Vector2(minX, minY);
        cameraFollowComponent.upperBounds = new Vector2(maxX, maxY);

        mainCam.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, mainCam.transform.position.z);
    }

    void Update()
    {
        loadedRooms.RemoveAll(room => room == null);
        foreach (Room room in loadedRooms)
        {
            if (activeRoom != null && activeRoom != room)
            {
                room.shouldDespawn = true;
                loadedRooms.Remove(room);
            }
        }
    }

    void Awake()
    {
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        player2D = player.GetComponent<Player2D>();
        blackOverlay = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Image>();
    }
}
