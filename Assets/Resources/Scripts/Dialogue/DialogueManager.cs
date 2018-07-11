using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text narratorText;
    public Text narrativeText;

    bool isDisplayingMessage = false;
    bool isAnimatingText = false;
    float fastForwardInitializationTime = 0;
    float nextMessageDisplayedTime = 0;

    Queue<Message> messageQueue = new Queue<Message>();
    Message currentMessage;
    System.Action afterDialogueCallback = () => { };

    public void DisplayDialogue(Dialogue dialogue, System.Action afterDialogueCallback)
    {
        this.afterDialogueCallback = afterDialogueCallback;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        Time.timeScale = 0f;
        messageQueue.Clear();

        foreach (Message message in dialogue.messages)
        {
            messageQueue.Enqueue(message);
        }
        isDisplayingMessage = true;
        NextMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisplayingMessage)
        {
            if (Application.isEditor && Input.GetKey(KeyCode.G))
            {
                StopAllCoroutines();
                messageQueue.Clear();
                narratorText.text = "";
                narrativeText.text = "";
                isDisplayingMessage = false;
                isAnimatingText = false;
                gameObject.GetComponent<CanvasGroup>().alpha = 0f;
                Time.timeScale = 1.0f;

                this.afterDialogueCallback();

            }
            bool playerInput = Input.GetKey(KeyCode.F);
            if (isAnimatingText)
            {
                if (playerInput && nextMessageDisplayedTime < Time.realtimeSinceStartup - 0.5f)
                {
                    FastForwardCurrentMessage();
                }
            }
            else
            {
                if (playerInput && (fastForwardInitializationTime == 0 || fastForwardInitializationTime < Time.realtimeSinceStartup - 0.5f))
                {
                    if (messageQueue.Count == 0)
                    {
                        narratorText.text = "";
                        narrativeText.text = "";
                        isDisplayingMessage = false;
                        isAnimatingText = false;
                        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
                        Time.timeScale = 1.0f;

                        this.afterDialogueCallback();
                        return;
                    }
                    NextMessage();
                }
            }
        }
    }

    void NextMessage()
    {
        nextMessageDisplayedTime = Time.realtimeSinceStartup;
        fastForwardInitializationTime = 0;
        currentMessage = messageQueue.Dequeue();
        StartCoroutine(TypeMessage(currentMessage));
    }

    IEnumerator TypeMessage(Message message)
    {
        isAnimatingText = true;
        narratorText.text = message.narrator;
        narrativeText.text = "";
        foreach (char character in message.Narrative.ToCharArray())
        {
            narrativeText.text += character;
            for (int i = 0; i < 4; i++)
            {
                yield return null;
            }
        }

        isAnimatingText = false;
    }

    void FastForwardCurrentMessage()
    {
        fastForwardInitializationTime = Time.realtimeSinceStartup;
        StopAllCoroutines();
        isAnimatingText = false;
        narratorText.text = System.String.Copy(currentMessage.narrator);
        narrativeText.text = System.String.Copy(currentMessage.Narrative);
    }
}
