using System.Collections;
using UnityEngine;

public class Level001 : Room
{
    public EnergyBeamDoor doorToLab;
    public GameObject boulder;
    public Dialogue tutorialDialogue;

    Player player;

    void MarkDialogueAsFinished()
    {
        player.seenTutorialText_001 = true;
    }

    IEnumerator ShowTutorialDialogue()
    {
        if (!player.seenTutorialText_001)
        {
            yield return new WaitForSeconds(2);
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(tutorialDialogue, MarkDialogueAsFinished);
        }
    }

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(ShowTutorialDialogue());
    }

    protected override void Update()
    {
        if (player.activatedEnergySwitch_001 && !doorToLab.isActive)
        {
            doorToLab.Activate();
        }
        if (player.readDiary_002 && !boulder.activeSelf)
        {
            boulder.SetActive(true);
        }

        base.Update();
    }
}
