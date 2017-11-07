using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    PlayerState playerState;
	public Slider hpBar;
	public AudioSource audioSrc;
    void Start()
    {
        playerState = gameObject.GetComponent<PlayerState>();
		if (hpBar != null) {
			hpBar.maxValue = playerState.health;
			hpBar.value = playerState.health;
			hpBar.fillRect.GetComponent<Image>().color = Color.green;
		}
    }

    public void DisablePlayer()
    {
        gameObject.SetActive(false);
        //Movement.playerDead = true;
    }

    public void Damage()
    {
		if(audioSrc != null)
		{
			//Play player hurt sound
			audioSrc.Play();
		}
        playerState.health -= playerState.damageRate;
        Debug.Log("Player health: " + playerState.health);
		if (hpBar != null) {
			hpBar.value = playerState.health;
			if (playerState.health <= hpBar.maxValue / 3) {
				hpBar.fillRect.GetComponent<Image>().color = Color.red;
			}else if(playerState.health <= 2*(hpBar.maxValue / 3))
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.yellow;
			}
			else
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.green;
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
