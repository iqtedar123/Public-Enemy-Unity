using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBtn : MonoBehaviour
{
    public Movement playerShooting;
    public int weaponNumber;
    public Text name;
    public Text cost;
    public Text description;
    // Use this for initialization
    void Start()
    {
        SetButton();
    }
    void SetButton()
    {
        var currentWeapon = playerShooting.availableWeapons[weaponNumber];
        name.text = currentWeapon.name;
        cost.text = currentWeapon.cost.ToString();
        description.text = currentWeapon.description;
    }

    public void OnClickWeapon()
    {
        //TODO: Add currency check
        playerShooting.currentWeapon = weaponNumber;
    }
}
