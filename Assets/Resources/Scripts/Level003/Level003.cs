using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level003 : Room
{
    public HighJumpPlatform highJumpPlatform;

    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void Update()
    {
        if (player.activatedEnergySwitch_003 && !highJumpPlatform.isActive)
        {
            highJumpPlatform.Activate();
        }

        base.Update();
    }
}
