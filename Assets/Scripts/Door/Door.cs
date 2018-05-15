using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door<T> : MonoBehaviour
{
    public bool isActive = true;

    Transform playerTransform;
    BoxCollider2D boxCollider;
    Animator doorAnimator;

    bool isOpen = false;
    float openTime = 0;

    public void Open()
    {
        isOpen = true;
        openTime = Time.time;
        boxCollider.enabled = false;
        doorAnimator.SetBool("isOpen", isOpen);
    }

    public void Activate()
    {
        isActive = true;
        doorAnimator.SetBool("isActive", isActive);
    }

    public void Deactivate()
    {
        isActive = false;
        doorAnimator.SetBool("isActive", isActive);
    }

    public void Close()
    {
        Debug.Log("closing door");
        isOpen = false;
        boxCollider.enabled = true;
        doorAnimator.SetBool("isOpen", isOpen);
    }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        doorAnimator = gameObject.GetComponent<Animator>();

        doorAnimator.SetBool("isActive", isActive);
    }

    private void Update()
    {
        if (isOpen && Time.time - openTime > 2)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > 10)
            {
                Close();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        T requiredBeamComponent = collision.gameObject.GetComponent<T>();
        if (requiredBeamComponent != null && isActive)
        {
            Open();
        }
    }
}
