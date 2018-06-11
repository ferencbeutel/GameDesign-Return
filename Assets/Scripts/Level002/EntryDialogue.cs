using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDialogue : MonoBehaviour
{

    public Dialogue entryDialogue;

    Player player;

    void MarkDialogueAsFinished()
    {
        player.seenEntryDialogue = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject && !player.seenEntryDialogue)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(entryDialogue, MarkDialogueAsFinished);
        }
    }
}
