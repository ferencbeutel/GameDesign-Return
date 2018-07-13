using UnityEngine;

public class HighJumpPlatform : Activateable
{
    public float jumpForceMultiplier;
    public Dialogue tutorialDialogue;

    bool isBoosted;
    Player player;
    Player2D player2D;
    DialogueManager dialogueManager;

    protected override void Awake()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
        player2D = playerGO.GetComponent<Player2D>();
        animator = gameObject.GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBoosted && collision.transform == player2D.transform && isActive)
        {
            player2D.m_JumpForce = player2D.m_JumpForce * jumpForceMultiplier;
            isBoosted = true;
        }

        if (collision.transform == player2D.transform && !player.seenHighJumpTutorial_003)
        {
            if (!isActive)
            {
                Message disabledMessage = new Message();
                disabledMessage.narrator = "Cloud Synapse";
                disabledMessage.Narrative = "This platform looks deactivated, Rogers. It looks like it ran out of power. Maybe you can activate it somehow?";
                tutorialDialogue.messages.Add(disabledMessage);
            }

            dialogueManager.DisplayDialogue(tutorialDialogue, () => { player.seenHighJumpTutorial_003 = true; });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBoosted && collision.transform == player2D.transform && isActive)
        {
            player2D.m_JumpForce = player2D.m_JumpForce / jumpForceMultiplier;
            isBoosted = false;
        }
    }
}
