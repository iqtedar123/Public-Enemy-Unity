using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	EnemyState enemyState;
    public Text currency;

	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
	}

	public void DisableEnemy ()
	{
		gameObject.SetActive (false);
		Movement.enemiesCount = Movement.enemiesCount - 1;
        if (Object.FindObjectsOfType<TutorialLevel>().Length == 0) isGameOver();
	}

	public void Damage (int damage)
	{
        var currentCurrent = System.Int32.Parse(currency.text);
        enemyState.health -= damage;
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
            UIManager.gameOverReason = "No more enemies!";
			UIManager.gameOver = "Congratulations! You win";
            SceneManager.LoadScene ("Game_Over");
			//Debug.Log("Loaded game over");
		} else {
			Debug.Log ("Not over");
		}
	}
}
