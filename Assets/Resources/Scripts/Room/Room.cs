using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    public bool shouldDespawn = false;
    public string uuid;

    protected virtual void Update()
    {
        if (shouldDespawn)
        {
            Destroy(gameObject, 0);
        }
    }
}
