using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int currentClip;
    public int gunAmmo;
    public bool purchasedMinimap = false;
    public Text ammoText;
    public Text gunCapacityText;

	// Use this for initialization
	void Start ()
	{
		waypointArray = GameObject.FindGameObjectsWithTag ("Enemy");
		Movement.enemiesCount = waypointArray.Length;
        currentClip = availableWeapons[currentWeapon].clipCapacity;
        gunAmmo = availableWeapons[currentWeapon].ammoCapacity;
        ammoText.text = currentClip.ToString();
        gunCapacityText.text = gunAmmo.ToString();
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
    public IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1);
        if(gunAmmo < availableWeapons[currentWeapon].clipCapacity)
        {
            currentClip = gunAmmo;
            gunAmmo = 0;
        } else
        {
            currentClip = availableWeapons[currentWeapon].clipCapacity;
            gunAmmo -= currentClip;
        }
        ammoText.text = currentClip.ToString();
        gunCapacityText.text = gunAmmo.ToString();
    }

	private void fireGun ()
	{
        var currentGun = availableWeapons[currentWeapon];
		if (Time.time - lastBulletTime >= currentGun.fireRate && currentClip > 0) {
			lastBulletTime = Time.time;
            currentClip -= 1;
            ammoText.text = currentClip.ToString();
            BulletPlayer.speed = currentGun.speed;
            BulletPlayer.damage = currentGun.damage;
            BulletPlayer.range = currentGun.range;
            if(currentClip == 0) StartCoroutine(ReloadGun());
            var bulletIns = Instantiate (bullet, transform.position, transform.rotation);
			bulletIns.transform.Translate (new Vector3 (0.303f, 0.738f, 0)); 
			bulletIns.transform.Rotate (Vector3.forward * 90);
			bulletIns.layer = LayerMask.NameToLayer ("Player");
		}
	}

}
