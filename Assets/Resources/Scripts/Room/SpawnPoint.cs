using UnityEngine;

public class SpawnPoint : Interactable
{
    public Room room;

    RoomManager roomManager;
    Player player;

    public override void OnInteraction()
    {
        player.Save();
    }

    public void Spawn()
    {
        if (roomManager == null)
        {
            roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        }
        roomManager.LoadRoomFromSpawnPoint(this);
    }

    override protected void Start()
    {
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        base.Start();
    }
}
