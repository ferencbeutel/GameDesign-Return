using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWarningTrigger : MonoBehaviour
{

    public Dialogue warningDialogue;

    DialogueManager dialogueManager;
    Player player;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player") == collision.gameObject && !player.seenLastWarning_006)
        {
            dialogueManager.DisplayDialogue(warningDialogue, () => { player.seenLastWarning_006 = true; });
        }
    }
}
