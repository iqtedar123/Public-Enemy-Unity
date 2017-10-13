using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
	public Transform playerTransform;

	EnemyState enemyState;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (enemyState.getPatrolMode () && shouldStartChasing ()) {
			enemyState.setPatrolMode (false);
		}
		if (enemyState.getPatrolMode () == false) {
			chase ();
		}
			
	}

	private bool shouldStartChasing ()
	{
		return Vector2.Distance (playerTransform.position, transform.position) <=
			enemyState.startChasingDistance;
	}

	private void chase ()
	{
		// Move towards the player.
		transform.position = Vector2.MoveTowards (
			transform.position,
			playerTransform.position,
			Mathf.Abs (enemyState.velocity) * Time.deltaTime
		);
		// Face the player.
		transform.up = new Vector2 (
			playerTransform.position.x - transform.position.x,
			playerTransform.position.y - transform.position.y
		);
	}

}
