using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CopDetectPlayer : MonoBehaviour
{

	public GameObject player;

	EnemyState enemyState;

	private MeshFilter mf;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		drawFieldOfView ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		detectPlayer ();
	}

	private void detectPlayer ()
	{
		Vector2 rayDirection = player.transform.position - transform.position;

		// Detect if player is within the field of view
		if (Vector2.Angle (transform.up, rayDirection) <= enemyState.viewAngleHalf) {
			// Use this raycast to try to hit the player. ~(1 << LayerMask.NameToLayer("Enemy")) ignores the enemy layer (it would always hit itself first).
			RaycastHit2D hit = Physics2D.Raycast (transform.position, rayDirection, enemyState.startChasingDistance, ~(1 << LayerMask.NameToLayer ("Enemy")));
			// Check if there is nothing obstructing the view between the enemy and the player.
			if (hit.collider && hit.transform.CompareTag ("Player")) {
				Debug.DrawRay (transform.position, rayDirection);
				Debug.Log ("I'm seeing the player");
				Debug.Log ("The cops caught you!");
				UIManager.gameOverReason = "The cops caught you!";
				SceneManager.LoadScene ("Game_Over");

			}
		}
	}

	private void drawFieldOfView ()
	{
		gameObject.AddComponent<MeshFilter> ();
		Mesh mesh = GetComponent<MeshFilter> ().mesh;

		float x = (Mathf.Tan (Mathf.Deg2Rad * enemyState.viewAngleHalf) * enemyState.startChasingDistance)
		          / transform.localScale.x;  // Remove the scaling effect of the the enemy.

		float y = enemyState.startChasingDistance / transform.localScale.y;

		mesh.Clear ();

		// make changes to the Mesh by creating arrays which contain the new values
		mesh.vertices = new Vector3[] {
			new Vector2 (0, 0),
			new Vector2 (-x, y),
			new Vector2 (x, y)
		};
		mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

	}
}
