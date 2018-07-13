using UnityEngine;

public class EnergySwitch : Interactable
{
    public Dialogue energySwitchActivationDialogue;
    public string playerActivationVariableName;
    public string playerDialogueVariableName;

    Player player;
    Animator myAnimator;

    public override void OnInteraction()
    {
        myAnimator.SetBool("isOn", true);
        player.GetType().GetField(playerActivationVariableName).SetValue(player, true);

        if (!(bool)player.GetType().GetField(playerDialogueVariableName).GetValue(player))
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(energySwitchActivationDialogue, MarkDialogueAsFinished);
        }
    }

    void MarkDialogueAsFinished()
    {
        player.GetType().GetField(playerDialogueVariableName).SetValue(player, true);
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myAnimator = gameObject.GetComponent<Animator>();
        if ((bool)player.GetType().GetField(playerActivationVariableName).GetValue(player))
        {
            myAnimator.SetBool("isOn", true);
        }
        base.Start();
    }
}
