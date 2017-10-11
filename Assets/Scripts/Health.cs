using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health = 120;
    public int damageRate = 20;
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    public void Damage()
    {
        health -= damageRate;
        if (health <= 0) DisableEnemy();
    }
}
