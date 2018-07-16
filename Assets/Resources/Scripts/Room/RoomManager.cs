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

    public void ClearAll()
    {
        activeRoom.shouldDespawn = true;
        loadedRooms.Clear();
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
        Debug.Log("new player position: " + destination.gameObject.transform.position);
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
        Transform currentTransform = activeRoom.gameObject.transform;

        Tuple<Tuple<float, float>, Tuple<float, float>> minMaxXYValues = DetermineMinMaxXYValues(activeRoom.gameObject.transform, 0, new Tuple<Tuple<float, float>, Tuple<float, float>>(new Tuple<float, float>(10000, -10000), new Tuple<float, float>(10000, -10000)));

        Camera2DFollow cameraFollowComponent = mainCam.GetComponent<Camera2DFollow>();

        Debug.Log("camera bounds: " + minMaxXYValues);

        cameraFollowComponent.lowerBounds = new Vector2(minMaxXYValues.GetLeft().GetLeft(), minMaxXYValues.GetRight().GetLeft());
        cameraFollowComponent.upperBounds = new Vector2(minMaxXYValues.GetLeft().GetRight(), minMaxXYValues.GetRight().GetRight());

        mainCam.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, mainCam.transform.position.z);
    }

    Tuple<Tuple<float, float>, Tuple<float, float>> DetermineMinMaxXYValues(Transform transform, int depth, Tuple<Tuple<float, float>, Tuple<float, float>> yield)
    {
        float newRendererMinX = yield.GetLeft().GetLeft();
        float newRendererMaxX = yield.GetLeft().GetRight();
        float newRendererMinY = yield.GetRight().GetLeft();
        float newRendererMaxY = yield.GetRight().GetRight();

        // best yield for this transforms children
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.tag == "IgnoreForBounds")
            {
                continue;
            }

            Renderer renderer = childTransform.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                newRendererMinX = Mathf.Min(renderer.bounds.min.x, newRendererMinX);
                newRendererMaxX = Mathf.Max(renderer.bounds.max.x, newRendererMaxX);
                newRendererMinY = Mathf.Min(renderer.bounds.min.y, newRendererMinY);
                newRendererMaxY = Mathf.Max(renderer.bounds.max.y, newRendererMaxY);
            }
        }


        if (newRendererMaxX > 60)
        {
            Debug.Log(transform.gameObject.name);
        }

        Tuple<Tuple<float, float>, Tuple<float, float>> newYield = new Tuple<Tuple<float, float>, Tuple<float, float>>(new Tuple<float, float>(newRendererMinX, newRendererMaxX), new Tuple<float, float>(newRendererMinY, newRendererMaxY));

        // deep enough, just return the best yield for this tree
        if (depth >= 3)
        {
            return newYield;
        }

        // we need to go deeper!
        List<Tuple<Tuple<float, float>, Tuple<float, float>>> childYields = new List<Tuple<Tuple<float, float>, Tuple<float, float>>>();
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.tag == "IgnoreForBounds")
            {
                continue;
            }

            childYields.Add(DetermineMinMaxXYValues(childTransform, depth + 1, newYield));
        }

        // now, determine best yield of children
        foreach (Tuple<Tuple<float, float>, Tuple<float, float>> childYield in childYields)
        {
            newRendererMinX = Mathf.Min(newRendererMinX, childYield.GetLeft().GetLeft());
            newRendererMaxX = Mathf.Max(newRendererMaxX, childYield.GetLeft().GetRight());
            newRendererMinY = Mathf.Min(newRendererMinY, childYield.GetRight().GetLeft());
            newRendererMaxY = Mathf.Max(newRendererMaxY, childYield.GetRight().GetRight());
        }

        return new Tuple<Tuple<float, float>, Tuple<float, float>>(new Tuple<float, float>(newRendererMinX, newRendererMaxX), new Tuple<float, float>(newRendererMinY, newRendererMaxY));
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
