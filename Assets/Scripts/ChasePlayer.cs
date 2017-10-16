using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
	public Transform playerTransform;

	EnemyState enemyState;
    public GameObject bullet;
    private float lastBulletTime = 0f;
    public float spawnInterval;
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
        //sHOOT THE BUllet
        fireGun();

    }
    private void fireGun()
    {
        Debug.Log("inside fire gun function");
        if (bullet != null && spawnInterval != 0.0)
        {
            if (Time.time - lastBulletTime >= spawnInterval)
            {
                lastBulletTime = Time.time;
                BulletMovement.playerFired = false;
                var bulletIns = Instantiate(bullet, transform.position, transform.rotation);
                bulletIns.transform.Translate(new Vector3(0.303f, 0.738f, 0));
                bulletIns.transform.Rotate(Vector3.forward * 90);
            }
        }
    }
}
