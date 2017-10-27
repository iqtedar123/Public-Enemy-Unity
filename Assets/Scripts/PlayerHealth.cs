using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerState playerState;
	public Slider hpBar;
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
		if (hpBar != null) {
			hpBar.value = playerState.health;
			if (playerState.health < 100 && playerState.health >= 75) {
				hpBar.fillRect.GetComponent<Image>().color = Color.green;
			}else if(playerState.health< 75 && playerState.health >= 50)
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.yellow;
			}
			else
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.red;
			}
		}
        if (playerState.health <= 0)
        {
            DisablePlayer();
            playerDied();
        }
    }

    private void playerDied()
    {
        Debug.Log("Player Died!");
        UIManager.gameOverReason = "You Died!";
        SceneManager.LoadScene("Game_Over");
        //Debug.Log("Loaded game over");
    }
}
