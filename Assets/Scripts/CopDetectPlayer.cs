using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class CopDetectPlayer : MonoBehaviour
{

	public GameObject player;

	EnemyState enemyState;
	PostProcessingBehaviour ppBehaviour;

	private MeshFilter mf;
	private float caughtTimer = 0;
	private float initVigIntensity;

	// Use this for initialization
	void Start ()
	{
		enemyState = gameObject.GetComponent<EnemyState> ();
		ppBehaviour = Camera.main.GetComponent<PostProcessingBehaviour>();
		initVigIntensity = ppBehaviour.profile.vignette.settings.intensity;
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
				caughtTimer += Time.deltaTime;
				changeVignette ();
				if (caughtTimer >= enemyState.escapeCopSeconds) {
					catchPlayer ();
				}
			} else if (caughtTimer > 0) {
				decrementCaughtTimer ();
			}
		} else if (caughtTimer > 0) {
			decrementCaughtTimer ();
		}
//		Debug.Log (caughtTimer);
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

	private void decrementCaughtTimer() {
		caughtTimer -= Time.deltaTime;
		if (caughtTimer < 0) {
			caughtTimer = 0;
		}
		changeVignette ();
	}

	private void changeVignette() {
		var intensityDiff = 1 - initVigIntensity;
		var settings = ppBehaviour.profile.vignette.settings;
		settings.intensity = initVigIntensity + (caughtTimer / enemyState.escapeCopSeconds) * intensityDiff;
		ppBehaviour.profile.vignette.settings = settings;
	}

	private void catchPlayer(){
		UIManager.gameOverReason = "The cops caught you!";
		SceneManager.LoadScene ("Game_Over");
		// Reset vignette
		var settings = ppBehaviour.profile.vignette.settings;
		settings.intensity = initVigIntensity;
		ppBehaviour.profile.vignette.settings = settings;
	}
}
