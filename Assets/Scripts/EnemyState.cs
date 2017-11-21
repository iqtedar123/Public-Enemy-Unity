using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int pointsForKill = 60;
	public int health = 200;
	public int damageRate = 20;

	// Speed and direction.
	public float velocity = 3f;
	// Move accross 'movementPlane'. Can be 'x' or 'y'.
	public char movementPlane = 'x';
	public float movementDistance = 100f;

	public float viewAngleHalf = 22.5f;

	// This is accessed by other scripts.
	public Vector2 initialPos;

	private bool patrolMode = true;

	// How close the player must be before getting chased.
	public float startChasingDistance = 5f;

	public float stopChasingDistance = 10f;

	public bool shouldChangeDirection = false;

	public float chaseTime = 3f;

	public float escapeCopSeconds = 0.5f;

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