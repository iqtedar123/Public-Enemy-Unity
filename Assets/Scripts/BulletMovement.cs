using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

	public float speed;

	private float maxX;
	private float maxY;
	private float minX;
	private float minY;
	private Vector3 size;

	void Start ()
	{
		float cameraDistance = (transform.position - Camera.main.transform.position).z;
		maxX = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, cameraDistance)).x;
		maxY = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, cameraDistance)).y;
		minX = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, cameraDistance)).x;
		minY = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, cameraDistance)).y;
		size = GetComponent<Renderer> ().bounds.size;
	}

	void Update ()
	{
		transform.Translate (new Vector3 (1, 0, 0) * speed * Time.deltaTime); 
		if (transform.position.x > maxX + size.x / 2
		    || transform.position.x < minX - size.x / 2
		    || transform.position.y > maxY + size.y / 2
		    || transform.position.y < minY - size.y / 2) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			var enemyHealth = col.gameObject.GetComponent<Health> ();
			enemyHealth.Damage ();
		}
		Destroy (gameObject);
	}

}
