using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasePlayer : MonoBehaviour
{
	public Transform playerTransform;
	bool chasing = false;

	EnemyState enemyState;
	public GameObject bullet;
	private float lastBulletTime = 0f;
	public float spawnInterval;
	float chaseCounter = 0f;
	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (enemyState.getPatrolMode () && shouldStartChasing ()) enemyState.setPatrolMode(false);
		if (enemyState.getPatrolMode () == false && shouldStopChasing ()) stopChase ();
		if (enemyState.getPatrolMode () == false) chase();


	}

	private bool shouldStartChasing ()
	{
		if (Vector2.Distance (playerTransform.position, transform.position) <=
			enemyState.startChasingDistance) {
			chaseCounter = enemyState.chaseTime;
			return true;
		}
		return false;
	}

	private bool shouldStopChasing()
	{
		if (Vector2.Distance (playerTransform.position, transform.position) >=
			enemyState.stopChasingDistance) {
			chaseCounter -= Time.deltaTime;
			if (chaseCounter <= 0)
				return true;
		} else {
			chaseCounter = enemyState.chaseTime;
		}
		return false;
	}

	private void chase ()
	{
		Seeker seeker = GetComponent<Seeker> ();

		if (seeker == null) {
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
		} else if (chasing == false){
			chasing = true;
			this.GetComponent<AIPath> ().canSearch = true;
			this.GetComponent<AIPath>().enabled = true;
		}

		//sHOOT THE BUllet
		fireGun();

	}

	private void stopChase() {
		if (chasing) {
			chasing = false;
			this.GetComponent<AIPath> ().canSearch = false;
			this.GetComponent<AIPath> ().enabled = false;
		}
		enemyState.setPatrolMode (true);
	}

	private void fireGun()
	{
		// Debug.Log("inside fire gun function");
		if (bullet != null && spawnInterval != 0.0)
		{
			if (Time.time - lastBulletTime >= spawnInterval)
			{
				lastBulletTime = Time.time;
				var bulletIns = Instantiate(bullet, transform.position, transform.rotation);
				bulletIns.transform.Translate(new Vector3(0.303f, 0.738f, 0));
				bulletIns.transform.Rotate(Vector3.forward * 90);
				bulletIns.layer = LayerMask.NameToLayer ("Enemy");
			}
		}
	}
}