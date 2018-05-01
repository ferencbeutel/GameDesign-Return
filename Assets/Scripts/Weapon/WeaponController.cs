using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon defaultWeapon;
    public Transform firePoint;

    public readonly List<Weapon> weapons = new List<Weapon>();

    int selectedWeaponIndex = 0;
    Weapon selectedWeapon;

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void CycleWeapon()
    {
        selectedWeaponIndex += 1;
        if (selectedWeaponIndex >= weapons.Count)
        {
            selectedWeaponIndex = 0;
        }
        selectedWeapon = weapons[selectedWeaponIndex];
    }

    public void Shoot()
    {
        selectedWeapon.Shoot(firePoint);
    }

    // Use this for initialization
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint for Rogers Weapon found!");
        }

        if (defaultWeapon == null)
        {
            Debug.LogError("No default Weapon for Rogers given!");
        }
        else
        {
            weapons.Add(Instantiate(defaultWeapon, new Vector2(0, 0), Quaternion.identity));
            selectedWeapon = weapons[selectedWeaponIndex];
        }
    }
}
