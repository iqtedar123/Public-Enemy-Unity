using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

	public static GameObject[] waypointArray;
	public static int enemiesCount = 0;

	public float spawnInterval;
	public float speed;
	public GameObject bullet;
	public LineRenderer shootLine;

	private float lastBulletTime = 0f;

    public WeaponObj[] availableWeapons;
    public int currentWeapon = 0;
    public bool purchasedMinimap = false;

	// Use this for initialization
	void Start ()
	{
		waypointArray = GameObject.FindGameObjectsWithTag ("Enemy");
		Movement.enemiesCount = waypointArray.Length;
	}

	// Update is called once per frame
	void Update ()
	{
		//Update position of player
		var currentPosition = transform.position;
		currentPosition.x += Input.GetAxisRaw ("Horizontal") * speed * Time.deltaTime;
		currentPosition.y += Input.GetAxisRaw ("Vertical") * speed * Time.deltaTime;
		transform.position = new Vector3 (currentPosition.x, currentPosition.y);

		// Update player direction.
		faceMouse ();

		// Fire button is pressed.
		if (Input.GetButton ("Fire1")) {
			// TODO: Different functions can be called based on weapon type. For now, just gun.
			fireGun ();
		}
	}

	private void faceMouse ()
	{
		// Get the in-world mouse position using the screen mouse position.
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		// The direction vector between the character and the mouse.
		transform.up = new Vector3 (
			mousePosition.x - transform.position.x,
			mousePosition.y - transform.position.y,
			0
		);
	}

	private void fireGun ()
	{
        var currentGun = availableWeapons[currentWeapon];
		if (Time.time - lastBulletTime >= currentGun.fireRate) {
			lastBulletTime = Time.time;
            BulletPlayer.speed = currentGun.speed;
            BulletPlayer.damage = currentGun.damage;
            BulletPlayer.range = currentGun.range;
			var bulletIns = Instantiate (bullet, transform.position, transform.rotation);
			bulletIns.transform.Translate (new Vector3 (0.303f, 0.738f, 0)); 
			bulletIns.transform.Rotate (Vector3.forward * 90);
			bulletIns.layer = LayerMask.NameToLayer ("Player");
		}
	}

}
