using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{

    public abstract void OnInteraction();

    bool inInteraction = false;
    Text hintText;
    Interactable interactableToUse;

    protected virtual void Start()
    {
        hintText = GameObject.FindGameObjectWithTag("InteractionHint").GetComponent<Text>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            hintText.enabled = true;
            interactableToUse = gameObject.GetComponent<Interactable>();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            hintText.enabled = false;
            interactableToUse = null;
        }
    }

    protected virtual void FixedUpdate()
    {
        bool isInteracting = Input.GetKey(KeyCode.E);
        if (!inInteraction && isInteracting && interactableToUse != null)
        {
            {
                inInteraction = true;
                interactableToUse.OnInteraction();
                inInteraction = false;
            }
        }
    }
}