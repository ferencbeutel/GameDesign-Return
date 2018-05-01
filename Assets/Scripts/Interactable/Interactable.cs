using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{

    public abstract void OnInteraction();

    bool inInteraction = false;
    Text hintText;

    protected virtual void Start()
    {
        hintText = GameObject.FindGameObjectWithTag("InteractionHint").GetComponent<Text>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            hintText.enabled = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            hintText.enabled = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        bool isInteracting = Input.GetKey(KeyCode.E);
        if (!inInteraction && isInteracting && hintText.enabled)
        {
            {
                inInteraction = true;
                OnInteraction();
                inInteraction = false;
            }
        }
    }
}