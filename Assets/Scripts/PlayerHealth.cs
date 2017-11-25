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
		transform.GetComponent<Rigidbody2D>().mass = 10000;
        playerState = gameObject.GetComponent<PlayerState>();
		if (hpBar != null) {
			hpBar.maxValue = playerState.health;
			hpBar.value = playerState.health;
			hpBar.fillRect.GetComponent<Image>().color = Color.green;
		}

		var curScene = SceneManager.GetActiveScene ().name;
		if (curScene == "Level2") {
			CarryOverState.furthestScene = Mathf.Max(CarryOverState.furthestScene, 1);
		} else if (curScene == "Level3") {
			CarryOverState.furthestScene = Mathf.Max(CarryOverState.furthestScene, 2);
		} else if (curScene == "Level4") {
			CarryOverState.furthestScene = Mathf.Max(CarryOverState.furthestScene, 3);
		} else if (curScene == "Level5") {
			CarryOverState.furthestScene = Mathf.Max(CarryOverState.furthestScene, 4);
		} else {
			CarryOverState.furthestScene = Mathf.Max(CarryOverState.furthestScene, 0);
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
		UIManager.gameOver = "GAME OVER";
        UIManager.gameOverReason = "You Died!";
		UIManager.levelToLoad = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game_Over");
        //Debug.Log("Loaded game over");
    }
}
