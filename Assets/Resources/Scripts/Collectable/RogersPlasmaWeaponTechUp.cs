using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogersPlasmaWeaponTechUp : Collectable
{
    public Dialogue tutorialDialogue;
    public RogersPlasmaWeapon rogersPlasmaWeapon;

    public override void OnCollection()
    {
        Debug.Log("collected RogersPlasmaWeapon!");
        GameObject rogersWeaponGO = GameObject.FindGameObjectWithTag("RogersWeapon");
        if (rogersWeaponGO == null)
        {
            Debug.LogError("Rogers Weapon GO cannot be found!");
            return;
        }
        WeaponController rogersWeapon = rogersWeaponGO.GetComponent<WeaponController>();
        if (rogersWeapon == null)
        {
            Debug.LogError("Rogers Weapon Component cannot be found!");
            return;
        }
        rogersWeapon.AddWeapon(Instantiate(rogersPlasmaWeapon, new Vector2(0, 0), Quaternion.identity));

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.DisplayDialogue(tutorialDialogue, () => { });
    }
}
