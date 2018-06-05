using System.Collections;
using UnityEngine;

public class Level001 : MonoBehaviour
{
    public EnergyBeamDoor doorToLab;
    public GameObject boulder;
    public Dialogue tutorialDialogue;

    Player player;

    void MarkDialogueAsFinished()
    {
        player.seenTutorialText = true;
    }

    IEnumerator ShowTutorialDialogue()
    {
        if (!player.seenTutorialText)
        {
            yield return new WaitForSeconds(2);
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.DisplayDialogue(tutorialDialogue, MarkDialogueAsFinished);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(ShowTutorialDialogue());
    }

    private void Update()
    {
        if (player.activatedEnergySwitch && !doorToLab.isActive)
        {
            doorToLab.Activate();
        }
        if (player.readDiary && !boulder.activeSelf)
        {
            boulder.SetActive(true);
        }
    }
}
