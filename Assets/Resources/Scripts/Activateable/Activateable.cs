using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activateable : MonoBehaviour
{

    public bool isActive = true;

    protected Animator animator;

    public void Activate()
    {
        isActive = true;
        animator.SetBool("isActive", isActive);
    }

    public void Deactivate()
    {
        isActive = false;
        animator.SetBool("isActive", isActive);
    }

    virtual protected void Awake()
    {
        animator.SetBool("isActive", isActive);
    }
}
