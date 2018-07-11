using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBullet : MonoBehaviour
{

    public float velX = 0;
    public float velY = 0;
    public float damage = 0;
    public bool facingRight;
    public bool facingUp;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingUp)
        {
            rb.velocity = new Vector2(velY, velX);
        }
        else
        {
            rb.velocity = new Vector2(facingRight ? velX : velX * -1, velY);
        }
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(gameObject, 0);

            if (collision.gameObject.GetComponent<Damageable>() != null)
            {
                collision.gameObject.GetComponent<Damageable>().ApplyDamage(damage);
            }
        }
    }
}
