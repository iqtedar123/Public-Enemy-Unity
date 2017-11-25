using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static string gameOverReason;
	public static string gameOver = "GAME OVER";
	public static string levelToLoad;
    public Text gameOverReasonText;
	public Text gameOverText;
	public Dropdown levelSelection;
    public Button retryButton;
	public Button startGameButton;
	public Button settingsButton;
	public Button exitToMainButton;
	public Button skipCutsceneLevel1;
	public Text statusText, pointsText;
	public Toggle tutorialToggle;
	public static string sceneName;
	public float statusTextDuration = 5; //Seconds to read the text
	int playOnce = 1;
	int playBackground = 1;
	public AudioSource DeathSound, PlayerWon, backgroundAudio;
	// Use this for initialization
	void Start () {
		if (levelSelection != null) {
			levelSelection.ClearOptions ();
			if (CarryOverState.furthestScene == 0) {
				levelSelection.AddOptions(new List<string> { "Level 1" });
			} else if (CarryOverState.furthestScene == 1) {
				levelSelection.AddOptions(new List<string> { "Level 1", "Level 2" });
			} else if (CarryOverState.furthestScene == 2) {
				levelSelection.AddOptions(new List<string> { "Level 1", "Level 2", "Level 3" });
			} else if (CarryOverState.furthestScene == 3) {
				levelSelection.AddOptions(new List<string> { "Level 1", "Level 2", "Level 3", "Level 4" });
			} else if (CarryOverState.furthestScene == 4) {
				levelSelection.AddOptions(new List<string> { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" });
			}
		}
        if (retryButton != null)
        {
            Button retryBtn = retryButton.GetComponent<Button>();
            retryBtn.onClick.AddListener(restartLevel);
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
		if(pointsText != null)
		{
			pointsText.text = "" + CarryOverState.points;
		}
		if(gameOverReasonText != null)
        {
            if(gameOverReason != null)
            {
                gameOverReasonText.text = gameOverReason;
				gameOverText.text = gameOver;
				if (gameOver == "GAME OVER")
				{
					if (playOnce == 1)
					{
						playOnce++;
						if (DeathSound != null) DeathSound.Play();
                    }
					else
					{
						
						if (DeathSound != null && !DeathSound.isPlaying && playBackground == 1 && backgroundAudio != null)
						{
                            playBackground++;
                            backgroundAudio.Play();
                        }
					}
				}
				else
				{
					if (playOnce == 1)
					{
						playOnce++;
						if (PlayerWon != null) PlayerWon.Play();
                    }
					else
					{
						//playOnce = 1;
						if (PlayerWon != null && !PlayerWon.isPlaying && playBackground == 1 && backgroundAudio != null)
						{
                            playBackground++;
                            backgroundAudio.Play();
                        }						
					}
				}
            }
        }
	}
    public void restartLevel()
    {
        //Opens level 0 
        gameOverReason = null;
		SceneManager.LoadScene(levelToLoad);
    }
	//Initial Level to be loaded will be set here. 
	public void openLevel()
	{
		CarryOverState.levelSelected = true;
		//Store the selected level in a global variable.
		//Opens level when button is pressed in start menu. 
		if (levelSelection != null)
		{
			if (levelSelection.value == 0)
			{
				//Level 1
				sceneName = "level1_cutscene";
			}
			else if (levelSelection.value == 1)
			{
				//level 2
				sceneName = "level2_cutscene";
			}
			else if (levelSelection.value == 2)
			{
				sceneName = "level3_cutscene";
			}
			else if (levelSelection.value == 3)
			{
				sceneName = "level4_cutscene";
			}
			else if (levelSelection.value == 4)
			{
				sceneName = "level5_cutscene";
			}
		}
		else
		{
			//Default to cutscene. 
			sceneName = "level1_cutscene";
		}
		//Check to see if toggle for tutorial is selected. 
		if (tutorialToggle != null) {
			//Tutorial toggle exists. 
			if (tutorialToggle.isOn == true)
			{
				//Player wants tutorial first. 
				SceneManager.LoadScene("Tutorial");
			}
			else
			{
				SceneManager.LoadScene(sceneName);
			}
		}
		
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
