using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{

    public float smallEnergyContainerDropChange;
    public float bigEnergyContainerDropChange;
    public AudioClip onImpact;
    public AudioClip onDeath;

    public EnergyContainer smallEnergyContainer;
    public EnergyContainer bigEnergyContainer;

    AudioSource audioSource;

    public override void OnDamageApplied()
    {
        audioSource.PlayOneShot(onImpact);
    }

    public override void OnDeath()
    {
        this.TryToSpawnEnergyContainer(smallEnergyContainerDropChange, smallEnergyContainer);
        this.TryToSpawnEnergyContainer(bigEnergyContainerDropChange, bigEnergyContainer);
        audioSource.PlayOneShot(onDeath);
    }

    void TryToSpawnEnergyContainer(float dropChange, EnergyContainer energyContainer)
    {
        if (Random.Range(0f, 1f) <= dropChange)
        {
            bool placeRight = Random.Range(0f, 1f) > 0.5;
            bool placeUp = Random.Range(0f, 1f) > 0.5;
            EnergyContainer newEnergyContainerInstance = Instantiate(energyContainer,
                new Vector2(transform.position.x + (placeRight ? Random.Range(0.1f, 0.5f) : Random.Range(-0.5f, -0.1f)), transform.position.y + (placeUp ? Random.Range(0.1f, 0.5f) : Random.Range(-0.5f, -0.1f))),
                Quaternion.identity);
            newEnergyContainerInstance.transform.parent = this.transform.parent;
        }
    }

    protected override void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        base.Start();
    }
}
