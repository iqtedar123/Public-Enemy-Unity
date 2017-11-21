using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Smuggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("SmugglePoint")) {
			UIManager.points = UIManager.points + 50;
			UIManager.gameOverReason = "Smuggline successful";
			UIManager.gameOver = "Congratulations, you win!";
			UIManager.levelToLoad = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene ("level3_cutscene");
		}
	}
}
