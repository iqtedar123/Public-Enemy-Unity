using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerState playerState;
    public Text playerHealthLabel;
    void Start()
    {
        playerState = gameObject.GetComponent<PlayerState>();
    }

    public void DisablePlayer()
    {
        gameObject.SetActive(false);
        //Movement.playerDead = true;
    }

    public void Damage()
    {
        playerState.health -= playerState.damageRate;
        Debug.Log("Player health: " + playerState.health);
        playerHealthLabel.text = "HP: " + playerState.health;
        if (playerState.health <= 0)
        {
            DisablePlayer();
            playerDied();
        }
    }

    private void playerDied()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene("Game_Over");
        //Debug.Log("Loaded game over");
    }
}
