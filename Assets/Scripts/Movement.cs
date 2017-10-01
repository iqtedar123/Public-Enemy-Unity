using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public Transform playerTransform;
	public Camera povCam;
	public LineRenderer shootLine;
	private WaitForSeconds gunTimeOut = new WaitForSeconds (0.5f);

	// Use this for initialization
	void Start ()
	{
		shootLine = GetComponent<LineRenderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//Update position of player
		var currentPosition = playerTransform.position;
		currentPosition.x += Input.GetAxisRaw ("Horizontal") * 5 * Time.deltaTime;
		currentPosition.y += Input.GetAxisRaw ("Vertical") * 5 * Time.deltaTime;
		playerTransform.position = new Vector3 (currentPosition.x, currentPosition.y);

		// Update player direction.
		faceMouse ();
		// Fire button is pressed.
		if (Input.GetButton ("Fire1")) {
			// For now, we are only shooting a gun, later we may be utilizing other types
			// of weapons. So this may get replaced with a call to a function that determines
			// which action to take.
			fireGun ();
		}
    
	}

	public IEnumerator Bullet ()
	{
		shootLine.enabled = true;
		yield return gunTimeOut;
		shootLine.enabled = false;
	}

	private void faceMouse ()
	{
		// Get the in-world mouse position using the screen mouse position.
		Vector3 mousePosition = povCam.ScreenToWorldPoint (Input.mousePosition);
		// The direction vector between the character and the mouse.
		playerTransform.up = new Vector3 (
			mousePosition.x - playerTransform.position.x,
			mousePosition.y - playerTransform.position.y,
			0
		);

	}

	private void fireGun ()
	{
		RaycastHit gunShot;
		Vector3 focus = povCam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0));
		StartCoroutine (Bullet ());
		shootLine.SetPosition (0, playerTransform.position);
		if (Physics.Raycast (focus, playerTransform.up, out gunShot, 100)) {
			Debug.Log ("Hit");
			shootLine.SetPosition (1, gunShot.point);
			var enemy = gunShot.collider.GetComponent<Health> ();
			if (enemy != null)
				enemy.DisableEnemy ();
		} else {
			shootLine.SetPosition (1, focus + (playerTransform.up * 100));
		}
	}
}
