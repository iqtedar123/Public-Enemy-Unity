using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public Texture[] frames;
	public float frameTime = 3.0f;
	private int frame=0;
	private float nextFrameTime;

	// Use this for initialization
	void Start () {
		nextFrameTime = Time.time + frameTime;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (frame < frames.Length) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), frames [frame]);
			if (Time.time >= nextFrameTime) {
				frame++;
				nextFrameTime += frameTime;
			}
		} else {
			// Cutscene over
		}
	}
}
