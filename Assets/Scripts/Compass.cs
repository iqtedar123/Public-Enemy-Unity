using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Compass : MonoBehaviour {
	// The point to move to
	public Transform targetPosition;
	public Transform directionArrow;
	public float scaleRange = 0.2f;
	public float pulsingSpeed = 2f;
	bool growing = true;
	private Seeker seeker;
	private CharacterController controller;
	// The calculated path
	public Path path;
	// The AI's speed in meters per second
	public float speed = 2;
	// The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	// The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	// How often to recalculate the path (in seconds)
	public float repathRate = 0.1f;
	private float lastRepath = -9999;
	public void Start () {
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
	}
	public void OnPathComplete (Path p) {
		Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
		if (!p.error) {
			path = p;
			// Reset the waypoint counter so that we start to move towards the first point in the path
			currentWaypoint = 0;
		}
	}
	public void Update () {
		if (Time.time - lastRepath > repathRate && seeker.IsDone()) {
			lastRepath = Time.time+ Random.value*repathRate*0.5f;
			// Start a new path to the targetPosition, call the the OnPathComplete function
			// when the path has been calculated (which may take a few frames depending on the complexity)
			seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
		}
		if (path == null) {
			// We have no path to follow yet, so don't do anything
			return;
		}
		if (currentWaypoint > path.vectorPath.Count) return;
		if (currentWaypoint == path.vectorPath.Count) {
			Debug.Log("End Of Path Reached");
			currentWaypoint++;
			return;
		}

		// Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
		//Vector3 newPos = transform.position + dir;
		//transform.position = newPos;
		// The commented line is equivalent to the one below, but the one that is used
		// is slightly faster since it does not have to calculate a square root
		//if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
		if ((transform.position-path.vectorPath[currentWaypoint]).sqrMagnitude < nextWaypointDistance*nextWaypointDistance) {
			currentWaypoint++;
			return;
		}

		// Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		//dir *= speed * Time.deltaTime;
		dir.Normalize();

		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion directionRotation = Quaternion.Euler (0, 0, zAngle);

		directionArrow.rotation = directionRotation;
		Vector3 arrowOffset = new Vector3 (2, 0, -1);
		arrowOffset = directionRotation * arrowOffset;
		directionArrow.position = arrowOffset + transform.position;
		PulsingArrow ();
	}

	public void PulsingArrow(){
		if (growing) {
			if ((directionArrow.localScale.x + (pulsingSpeed * Time.deltaTime)) >= 1 + scaleRange) {
				growing = false;
			}
		} else {
			if ((directionArrow.localScale.x - (pulsingSpeed * Time.deltaTime)) <= 1 - scaleRange) {
				growing = true;
			}
		}

		if (growing) {
			float newSize = directionArrow.localScale.x + (pulsingSpeed * Time.deltaTime);
			directionArrow.localScale = new Vector3 (newSize, newSize, 1);
		} else {
			float newSize = directionArrow.localScale.x - (pulsingSpeed * Time.deltaTime);
			directionArrow.localScale = new Vector3 (newSize, newSize, 1);
		}
	}
} 
