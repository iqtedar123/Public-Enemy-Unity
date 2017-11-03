using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static string gameOverReason;
	public static string gameOver = "GAME OVER";
    public Text gameOverReasonText;
	public Text gameOverText;
    public Button retryButton;
	public Button startGameButton;
	public Button settingsButton;
	public Button exitToMainButton;
	public Button skipCutsceneLevel1;
	public Text statusText;
	public float statusTextDuration = 5; //Seconds to read the text
	int playOnce = 1;
	int playBackground = 1;
	public AudioSource DeathSound, PlayerWon, backgroundAudio;
	// Use this for initialization
	void Start () {
        if (retryButton != null)
        {
            Button retryBtn = retryButton.GetComponent<Button>();
            retryBtn.onClick.AddListener(openLevel0);
        }
		if(startGameButton != null)
		{
			Button startBtn = startGameButton.GetComponent<Button>();
			startBtn.onClick.AddListener(openLevel);
		}
		if (settingsButton != null)
		{
			Button settingsBtn = settingsButton.GetComponent<Button>();
			settingsBtn.onClick.AddListener(OpenSettings);
		}
		if(exitToMainButton != null)
		{
			Button exitButton = exitToMainButton.GetComponent<Button>();
			exitButton.onClick.AddListener(openMainMenu);
		}
		if(skipCutsceneLevel1 != null)
		{
			Button skipCutsceneBtn = skipCutsceneLevel1.GetComponent<Button>();
			skipCutsceneBtn.onClick.AddListener(skipCutscene);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(gameOverReasonText != null)
        {
            if(gameOverReason != null)
            {
                gameOverReasonText.text = gameOverReason;
				gameOverText.text = gameOver;
				if (gameOverReason == "You Died!")
				{
					if (playOnce == 1)
					{
						playOnce++;
						if (DeathSound != null)
						{
							DeathSound.Play();
						}
					}
					else
					{
						
						if (DeathSound != null)
						{
							if (!DeathSound.isPlaying)
							{
								if (playBackground == 1)
								{
									if (backgroundAudio != null)
									{
										playBackground++;
										backgroundAudio.Play();
									}
								}
							}
						}
					}
				}
				else
				{
					if (playOnce == 1)
					{
						playOnce++;
						if (PlayerWon != null)
						{
							PlayerWon.Play();
						}

					}
					else
					{
						//playOnce = 1;
						if (PlayerWon != null)
						{
							if (!PlayerWon.isPlaying)
							{
								if (playBackground == 1)
								{
									if (backgroundAudio != null)
									{
										playBackground++;
										backgroundAudio.Play();
									}
								}
							}
						}
						
					}
				}
            }
        }
	}
    public void openLevel0()
    {
        //Opens level 0 
        gameOverReason = null;
        SceneManager.LoadScene("Level01");
    }
	//Initial Level to be loaded will be set here. 
	public void openLevel()
	{
		//Opens level when button is pressed in start menu. 
		//TODO Change this later. 
		SceneManager.LoadScene("level1_cutscene");
	}
	public void openMainMenu()
	{
		//Opens the main menu. 
		SceneManager.LoadScene("Main_Menu");
	}
	//Opens the settings menu.
	public void OpenSettings()
	{
		//TODO Create the settings menu. 
		//TODO Open the settings menu. 

		statusText.text = "Coming Soon!";
		Invoke("HideStatusText", statusTextDuration);
	}
	public void HideStatusText()
	{
		statusText.text = "";
	}
	public void skipCutscene()
	{
		SceneManager.LoadScene("Level01");
	}
}
