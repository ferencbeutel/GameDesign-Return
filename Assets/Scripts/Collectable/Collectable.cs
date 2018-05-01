using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public string uuid;

    public abstract void OnCollection();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        Player player = playerGO.GetComponent<Player>();
        if (collision.gameObject == playerGO)
        {
            if (string.IsNullOrEmpty(uuid) || player.Collect(uuid))
            {
                OnCollection();
                Destroy(gameObject, 0);
            }
            else
            {
                Debug.LogError("Could not collect item: " + this.name);
            }
        }
    }

    protected virtual void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!string.IsNullOrEmpty(uuid) && player.HasCollected(uuid))
        {
            Debug.Log("already collected me! " + uuid);
            Destroy(gameObject, 0);
        }
    }
}
