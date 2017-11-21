using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {

	public Texture[] frames;
	public float frameTime = 7.0f;
	private int frame=0;
	private float mouseDownTime = 1.0f;
	private float mouseTime;
	private float nextFrameTime;
	public string levelname;

	// Use this for initialization
	void Start () {
		nextFrameTime = frameTime;
		mouseTime = mouseDownTime;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (frame < frames.Length) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), frames [frame]);
			nextFrameTime -= Time.deltaTime;
			mouseTime -= Time.deltaTime;
			if (Input.GetMouseButtonDown (0) && mouseTime <= 0) {
				frame++;
				nextFrameTime = frameTime;
				mouseTime = mouseDownTime;
			} else if (nextFrameTime <= 0) {
				frame++;
				nextFrameTime = frameTime;
				mouseTime = mouseDownTime;
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				frame = frames.Length + 1;
				nextFrameTime = frameTime;
			}
		} else {
			SceneManager.LoadScene (levelname);
		}
	}
}
