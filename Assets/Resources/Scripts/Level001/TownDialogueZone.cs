using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownDialogueZone : MonoBehaviour
{

    public Dialogue townDialogue;

    Player player;

    void MarkDialogueAsFinished()
    {
        player.seenTownDialogue_001 = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject && !player.seenTownDialogue_001)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(townDialogue, MarkDialogueAsFinished);
        }
    }

}
