using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public Dialogue gameOverDialogue;

    MenuInitializer menuInitializer;
    Player player;
    RoomManager roomManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player") == collision.gameObject)
        {
            GameObject.FindObjectOfType<DialogueManager>().DisplayDialogue(gameOverDialogue, () =>
            {
                player.DeleteSave();
                menuInitializer.LoadMenu();
                roomManager.ClearAll();
                Destroy(player.gameObject, 0);
            });
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menuInitializer = GameObject.FindObjectOfType<MenuInitializer>();
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }
}
