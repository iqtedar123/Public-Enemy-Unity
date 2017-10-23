using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

	public int health = 200;
	public int damageRate = 20;

	// Speed and direction.
	public float velocity = 3f;
	// Move accross 'movementPlane'. Can be 'x' or 'y'.
	public char movementPlane = 'x';
	public float movementDistance = 100f;

	public float viewAngleHalf = 22.5f;

	private bool patrolMode = true;

	// How close the player must be before getting chased.
	public float startChasingDistance = 5f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool getPatrolMode()
	{
		return patrolMode;
	}

	public void setPatrolMode(bool value)
	{
		patrolMode = value;
	}
}
