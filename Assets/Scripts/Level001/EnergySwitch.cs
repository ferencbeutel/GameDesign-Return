using UnityEngine;

public class EnergySwitch : Interactable
{
    Player player;
    Animator myAnimator;

    public override void OnInteraction()
    {
        myAnimator.SetBool("isOn", true);
        player.activatedEnergySwitch = true;
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myAnimator = gameObject.GetComponent<Animator>();
        base.Start();
    }
}
