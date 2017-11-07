﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	EnemyState enemyState;
	public Text currency;
	Transform hpCanvas;
	public Slider hpBar;
	Vector3 hpCanvasOffset = new Vector3(9.65f, -6f, 0);

	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		hpCanvas = transform.Find ("EnemyHPBar");
		if (hpCanvas != null) {
			hpBar.maxValue = enemyState.health;
			hpBar.value = enemyState.health;
			hpBar.fillRect.GetComponent<Image>().color = Color.green;
		}
	}

	public void DisableEnemy ()
	{
		gameObject.SetActive (false);
		Movement.enemiesCount = Movement.enemiesCount - 1;
		isGameOver ();
	}

	public void Damage (int damage)
	{
		var currentCurrent = System.Int32.Parse(currency.text);
		enemyState.health -= damage;
		if (hpBar != null) {
			hpBar.value = enemyState.health;
			if (enemyState.health <= hpBar.maxValue / 3) {
				hpBar.fillRect.GetComponent<Image>().color = Color.red;
			}else if(enemyState.health <= 2*(hpBar.maxValue / 3))
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.yellow;
			}
			else
			{
				hpBar.fillRect.GetComponent<Image>().color = Color.green;
			}
		}
		if (enemyState.health <= 0)
		{
			DisableEnemy();
			currency.text = (currentCurrent + enemyState.pointsForKill).ToString();
		}
	}

	private void isGameOver ()
	{
		//TODO Determine if game is over when all enemies are dead or if the end of level is reached. 

		if (Movement.enemiesCount <= 0) {
			Debug.Log ("No more enemies!");
			//UIManager.gameOverReason = "No more enemies!";
			//UIManager.gameOver = "Congratulations! You win";
			SceneManager.LoadScene ("Level2");
			//Debug.Log("Loaded game over");
		} else {
			Debug.Log ("Not over");
		}
	}

	void Update() {
		if (hpCanvas == null) {
			return;
		}

		Vector3 newHPPosition = transform.position + hpCanvasOffset;
		newHPPosition.z = -1;
		hpCanvas.position = newHPPosition;
		hpCanvas.rotation = Quaternion.Euler (new Vector3(0,0,0));
	}
}
