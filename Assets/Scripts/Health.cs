using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int health = 120;
    public int damageRate = 20;
    
    public void DisableEnemy()
    {
        gameObject.SetActive(false);
        Movement.enemiesCount= Movement.enemiesCount-1;
        isGameOver();
    }

    public void Damage()
    {
        health -= damageRate;
        if (health <= 0) DisableEnemy();
    }
    private void isGameOver()
    {
        //TODO Add condition to check if health is 0. 
        //TODO Determine if game is over when all enemies are dead or if the end of level is reached. 
        
        if (Movement.enemiesCount <= 0)
        {
            Debug.Log("No more enemies!");
            SceneManager.LoadScene("Game_Over");
            //Debug.Log("Loaded game over");
        }
        else
        {
            Debug.Log("Not over");
        }
    }
}
