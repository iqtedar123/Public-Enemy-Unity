using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObj : ScriptableObject{
    
    //Market Information
    public string weaponName;
    public int cost;
    public string description;

    //Weapon Stats
    public float speed;
    public float fireRate;
    public int damage;
    public float range;
    	
}
