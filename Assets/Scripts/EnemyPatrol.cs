using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	EnemyState enemyState;
	Vector2 initialPos;
	Vector2 curPos;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		initialPos = transform.position;
		faceCorrectDirection (initialPos);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (enemyState.getPatrolMode ()) {
			updatePos ();
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (enemyState.getPatrolMode () && col.gameObject.CompareTag ("Wall")) {
			changeDirection ();
		}
	}

	private void updatePos ()
	{
		curPos = transform.position;
		if (enemyState.movementPlane == 'x') {
			curPos.x += enemyState.velocity * Time.deltaTime;
		} else {
			curPos.y += enemyState.velocity * Time.deltaTime;
		}

		if (Vector2.Distance (curPos, initialPos) <= enemyState.movementDistance) {
			transform.position = curPos;
		} else {
			changeDirection ();
		}
			
		
	}

	// Move in the opposite direction.
	private void changeDirection ()
	{
		enemyState.velocity *= -1;
		faceCorrectDirection (curPos);
	}

	// Face in the direction of movement.
	private void faceCorrectDirection (Vector2 curPosition)
	{
		if (enemyState.movementPlane == 'x') {
			transform.up = new Vector2 (curPosition.x + enemyState.velocity * 100, 0);
		} else {
			transform.up = new Vector2 (0, curPosition.y + enemyState.velocity * 100);
		}
	}
}
