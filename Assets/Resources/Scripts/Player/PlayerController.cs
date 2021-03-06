using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Player2D))]
public class PlayerController : MonoBehaviour
{
    private PauseMenuInitializer pauseMenu;
    private Player2D m_Character;
    private WeaponController rogersWeapon;
    private bool m_Jump;


    private void Start()
    {
        m_Character = GetComponent<Player2D>();
        rogersWeapon = GetComponentInChildren<WeaponController>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuInitializer>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            rogersWeapon.CycleWeapon();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            rogersWeapon.Shoot();
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.InitPauseMenuUI();
            return;
        }
        bool crouch = Input.GetKey(KeyCode.S);
        bool lookingUp = Input.GetKey(KeyCode.W);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump, lookingUp);
        m_Jump = false;
    }
}
