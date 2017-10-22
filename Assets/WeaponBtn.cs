using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBtn : MonoBehaviour {

    public Movement playerShooting;
    public int weaponNumber;
    public Text name;
    public Text cost;
    public Text description;
	// Use this for initialization
	void Start () {
        SetButton();
	}
    void SetButton()
    {
        
        name.text = playerShooting.availableWeapons[weaponNumber].name;
        cost.text = playerShooting.availableWeapons[weaponNumber].cost.ToString();
        description.text = playerShooting.availableWeapons[weaponNumber].description;
    }

    public void OnClickWeapon()
    {
        //TODO: Add currency check
        playerShooting.currentWeapon = weaponNumber;

    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
