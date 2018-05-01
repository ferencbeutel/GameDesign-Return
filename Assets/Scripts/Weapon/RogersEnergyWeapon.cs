using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersEnergyWeapon : Weapon
{

    public EnergyBullet energyBullet;
    private Player2D player2D;

    public override void Shoot(Transform firePoint)
    {
        EnergyBullet bullet = Instantiate(energyBullet, new Vector2(firePoint.position.x, firePoint.position.y), Quaternion.identity);
        bullet.facingRight = player2D.m_FacingRight;
        bullet.facingUp = player2D.m_FacingUp;
    }

    private void Start()
    {
        player2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D>();
    }
}
