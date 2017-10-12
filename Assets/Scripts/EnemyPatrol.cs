using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	// Speed and direction.
	public float velocity = 3f;
	// Move accross 'movementPlane'. Can be 'x' or 'y'.
	public char movementPlane = 'x';
	public float maxDistance = 100;

	Vector2 initialPos;
	Vector2 curPos;

	// Use this for initialization
	void Start ()
	{
		initialPos = transform.position;
		faceCorrectDirection (initialPos);
	}
	
	// Update is called once per frame
	void Update ()
	{
		updatePos ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Wall")) {
			changeDirection ();
		}
	}

	private void updatePos ()
	{
		curPos = transform.position;
		if (movementPlane == 'x') {
			curPos.x += velocity * Time.deltaTime;
		} else {
			curPos.y += velocity * Time.deltaTime;
		}

		if (Vector2.Distance (curPos, initialPos) <= maxDistance) {
			transform.position = curPos;
		} else {
			changeDirection ();
		}
			
		
	}

	// Move in the opposite direction.
	private void changeDirection ()
	{
		velocity *= -1;
		faceCorrectDirection (curPos);
	}

	// Face in the direction of movement.
	private void faceCorrectDirection (Vector2 curPosition)
	{
		if (movementPlane == 'x') {
			transform.up = new Vector2 (curPosition.x + velocity * 100, 0);
		} else {
			transform.up = new Vector2 (0, curPosition.y + velocity * 100);
		}
	}
}
