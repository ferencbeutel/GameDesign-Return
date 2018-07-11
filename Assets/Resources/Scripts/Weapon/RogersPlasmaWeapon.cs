using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersPlasmaWeapon : Weapon
{

    public PlasmaBullet plasmaBullet;
    private Player2D player2D;

    public override void Shoot(Transform firePoint)
    {
        bool facingRight = firePoint.parent.parent.localScale.x > 0;
        PlasmaBullet bullet = Instantiate(plasmaBullet, new Vector2(firePoint.position.x, firePoint.position.y), Quaternion.identity);
        bullet.facingRight = facingRight;
        bullet.facingUp = player2D.m_FacingUp;
    }

    private void Start()
    {
        player2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D>();
    }
}
