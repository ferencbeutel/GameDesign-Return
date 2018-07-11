using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{

    public Text health;
    Damageable rogersDamageableComponent;

    // Use this for initialization
    void Start()
    {
        GameObject rogers = GameObject.FindGameObjectWithTag("Player");
        rogersDamageableComponent = rogers.GetComponent<Damageable>();
        if (rogersDamageableComponent == null)
        {
            Debug.LogError("No Damageable component for rogers found!");
            return;
        }
        health.text = "health: " + rogersDamageableComponent.getCurrentHealth();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "health: " + rogersDamageableComponent.getCurrentHealth();
    }
}
