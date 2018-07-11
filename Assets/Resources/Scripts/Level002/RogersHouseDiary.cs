using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersHouseDiary : Interactable
{
    public Dialogue diaryDialogue;
    public Dialogue boulderDialogue;

    Player player;
    Player2D player2D;
    CameraEffects cameraEffects;
    DialogueManager dialogueManager;

    public override void OnInteraction()
    {
        if (!player.readDiary)
        {
            dialogueManager.DisplayDialogue(diaryDialogue, AfterDiaryDialogue);
        }
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player2D = player.gameObject.GetComponent<Player2D>();
        cameraEffects = FindObjectOfType<CameraEffects>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        base.Start();
    }

    void AfterDiaryDialogue()
    {
        player.readDiary = true;
        StartCoroutine(InitBoulderLogic());
    }

    IEnumerator InitBoulderLogic()
    {
        float shakeDuration = 2;
        player2D.LockMovement();
        cameraEffects.ShakeFor(shakeDuration, 0.7f);
        yield return new WaitForSeconds(shakeDuration);
        player2D.UnlockMovement();

        dialogueManager.DisplayDialogue(boulderDialogue, () => { });
    }
}
