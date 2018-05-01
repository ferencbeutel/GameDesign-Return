using System;
using UnityEngine;
using UnityStandardAssets._2D;

public class Player2D : MonoBehaviour
{
    const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public bool m_FacingUp = false;  // For determining which way the player is currently facing.

    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private Transform m_firePoint;
    private bool rotated = false;
    private bool inCrouch = false;
    private bool movementLocked = false;

    public void Move(float move, bool crouch, bool jump, bool upwards)
    {
        if (!movementLocked)
        {
            Vector3 theFirePointFacingUpOffset = new Vector3(-0.22f, 0.4f, 0);
            Vector3 theFirePointCrouchingOffset = new Vector3(0, -0.3f, 0);

            if (crouch && !inCrouch)
            {
                m_firePoint.localPosition += theFirePointCrouchingOffset;
                inCrouch = true;
            }
            else if (!crouch && inCrouch)
            {
                m_firePoint.localPosition -= theFirePointCrouchingOffset;
                inCrouch = false;
            }

            if (upwards && !m_FacingUp)
            {
                m_firePoint.localPosition += theFirePointFacingUpOffset;
                m_FacingUp = true;
            }
            else if (!upwards && m_FacingUp)
            {
                m_firePoint.localPosition -= theFirePointFacingUpOffset;
                m_FacingUp = false;
            }

            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);
            m_Anim.SetBool("Upwards", upwards);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
    }

    public void LockMovement()
    {
        movementLocked = true;
        m_Rigidbody2D.velocity = Vector3.zero;
        m_Rigidbody2D.angularVelocity = 0;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_firePoint = transform.Find("Weapon").Find("FirePoint");
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        if (!rotated)
        {
            theScale.x *= -1;
        }
        else
        {
            theScale.y *= -1;
        }
        transform.localScale = theScale;
    }
}
