using UnityEngine;

public class EnergySwitch : Interactable
{
    Player player;

    public override void OnInteraction()
    {
        Debug.Log("TODO: animate current in cable");
        player.activatedEnergySwitch = true;
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        base.Start();
    }
}
