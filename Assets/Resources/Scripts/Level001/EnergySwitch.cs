using UnityEngine;

public class EnergySwitch : Interactable
{
    public Dialogue energySwitchActivationDialogue;

    Player player;
    Animator myAnimator;

    public override void OnInteraction()
    {
        myAnimator.SetBool("isOn", true);
        player.activatedEnergySwitch = true;

        if (!player.seenEnergySwitchDialogue)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(energySwitchActivationDialogue, MarkDialogueAsFinished);
        }
    }

    void MarkDialogueAsFinished()
    {
        player.seenEnergySwitchDialogue = true;
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myAnimator = gameObject.GetComponent<Animator>();
        if (player.activatedEnergySwitch)
        {
            myAnimator.SetBool("isOn", true);
        }
        base.Start();
    }
}
