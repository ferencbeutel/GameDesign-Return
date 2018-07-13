using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door<T> : Activateable
{
    Transform playerTransform;
    PolygonCollider2D polyCollider;

    bool isOpen = false;
    float openTime = 0;

    public void Open()
    {
        isOpen = true;
        openTime = Time.time;
        polyCollider.enabled = false;
        animator.SetBool("isOpen", isOpen);
    }

    public void Close()
    {
        Debug.Log("closing door");
        isOpen = false;
        polyCollider.enabled = true;
        animator.SetBool("isOpen", isOpen);
    }

    protected override void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        polyCollider = gameObject.GetComponent<PolygonCollider2D>();
        animator = gameObject.GetComponent<Animator>();

        base.Awake();
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
