using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	EnemyState enemyState;
	Vector2 curPos;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		enemyState.initialPos = transform.position;
		faceCorrectDirection (enemyState.initialPos);
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
		} else {
			enemyState.shouldChangeDirection = true;
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

		if (Vector2.Distance (curPos, enemyState.initialPos) <= enemyState.movementDistance && enemyState.shouldChangeDirection == false) {
			transform.position = curPos;
		} else {
			changeDirection ();
			enemyState.shouldChangeDirection = false;
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
			if (enemyState.velocity <= 0) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			} else {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, -90));
			}
		} else {
			if (enemyState.velocity <= 0) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
			} else {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			}
		}
	}
}