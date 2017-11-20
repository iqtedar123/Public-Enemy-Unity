using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasePlayer : MonoBehaviour
{
	public Transform playerTransform;
	bool chasing = false;
	bool retreating = false;

	EnemyState enemyState;
	public GameObject bullet;
	public AudioSource bulletSound;
	private float lastBulletTime = 0f;
	public float spawnInterval;
	float chaseCounter = 0f;

	GameObject emptyObject;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		emptyObject = new GameObject();
	}

	// Update is called once per frame
	void Update ()
	{
		if ((enemyState.getPatrolMode () || retreating) && shouldStartChasing ()) {
			initiateChase ();
		}

		if (enemyState.getPatrolMode () == false && shouldStopChasing ()) stopChase ();
		if (enemyState.getPatrolMode () == false) {
			if (chasing && shouldStopChasing ())
				stopChase ();
			else if (chasing)
				chase ();

			else if (retreating && shouldStopRetreating ())
				stopRetreat ();
		}


	}

	// Player is within 'distance' and no obstacles are in the way.
	private bool playerInSight(float distance) {
		Vector2 rayDirection = playerTransform.position - transform.position;
		if (Vector2.Distance (playerTransform.position, transform.position) <=
			distance) {
			// Use this raycast to try to hit the player. ~(1 << LayerMask.NameToLayer("Enemy"))
			// ignores the enemy layer (it would always hit itself first).
			RaycastHit2D hit = Physics2D.Raycast (transform.position, rayDirection,
				enemyState.startChasingDistance, ~(1 << LayerMask.NameToLayer ("Enemy")));
			// Check if there is nothing obstructing the view between the enemy and the player.
			return hit.collider && hit.transform.CompareTag ("Player");
		}
		return false;
	}

	private bool shouldStartChasing ()
	{
		if (playerInSight(enemyState.startChasingDistance)) {
			chaseCounter = enemyState.chaseTime;
			return true;
		}
		return false;
	}

	private bool shouldStopChasing()
	{
		if (!playerInSight(enemyState.stopChasingDistance)) {
			chaseCounter -= Time.deltaTime;
			if (chaseCounter <= 0)
				return true;
		} 

		return false;
	}

	private bool shouldStopRetreating() {
		return this.GetComponent<AIPath>().TargetReached;
	}

	private void initiateChase() {
		enemyState.setPatrolMode (false);
		if (retreating) {
			stopRetreat ();
		}

		chasing = true;
		this.GetComponent<AIPath> ().target = playerTransform;
		this.GetComponent<AIPath> ().canSearch = true;
		this.GetComponent<AIPath>().enabled = true;
		this.GetComponent<AIPath> ().SearchPath ();
	}

	private void initiateRetreat() {
		retreating = true;

		emptyObject.transform.position = enemyState.initialPos;
		this.GetComponent<AIPath> ().target = emptyObject.transform;
		this.GetComponent<AIPath> ().SearchPath ();
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
		}

		//sHOOT THE BUllet
		fireGun();

	}

	private void stopChase() {
		var seeker = GetComponent<Seeker> ();

		chasing = false;
		if (seeker == null)
			stopRetreat ();
		else
			initiateRetreat ();

	}

	private void stopRetreat() {
		retreating = false;
		this.GetComponent<AIPath> ().canSearch = false;
		this.GetComponent<AIPath> ().enabled = false;
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
				if(bulletSound!= null)
				{
					bulletSound.Play();
				}
				var bulletIns = Instantiate(bullet, transform.position, transform.rotation);
				bulletIns.transform.Translate(new Vector3(0.303f, 0.738f, 0));
				bulletIns.transform.Rotate(Vector3.forward * 90);
				bulletIns.layer = LayerMask.NameToLayer ("Enemy");
			}
		}
	}
}