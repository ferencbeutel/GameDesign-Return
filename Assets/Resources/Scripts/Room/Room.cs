using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool shouldDespawn = false;
    public string uuid;

    private void Update()
    {
        if (shouldDespawn)
        {
            Destroy(gameObject, 0);
        }
    }
}
