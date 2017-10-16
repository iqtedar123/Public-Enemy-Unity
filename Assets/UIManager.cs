using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static string gameOverReason;
    public Text gameOverReasonText;
    public Button retryButton;
	// Use this for initialization
	void Start () {
        if (retryButton != null)
        {
            Button retryBtn = retryButton.GetComponent<Button>();
            retryBtn.onClick.AddListener(openLevel0);
        }
    }
	
	// Update is called once per frame
	void Update () {

		if(gameOverReasonText != null)
        {
            if(gameOverReason != null)
            {
                gameOverReasonText.text = gameOverReason;
            }
        }
	}
    public void openLevel0()
    {
        //Opens level 0 
        gameOverReason = null;
        SceneManager.LoadScene("Level01");
    }
}
