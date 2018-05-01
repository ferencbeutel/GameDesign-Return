using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyContainer : Collectable
{

    public float healedAmount;

    public override void OnCollection()
    {
        GameObject rogers = GameObject.FindGameObjectWithTag("Player");
        if (rogers == null)
        {
            Debug.LogError("Rogers cannot be found in the scene!");
            return;
        }
        Damageable rogersDamageComponent = rogers.GetComponent<Damageable>();
        if (rogersDamageComponent == null)
        {
            Debug.LogError("Rogers damage component cannot be found in the scene!");
            return;
        }
        rogersDamageComponent.Heal(healedAmount);
    }
}
