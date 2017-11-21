using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialLevel : MonoBehaviour
{

    public int tutorialState = -1;
    public int step0Count = 0;
    public Movement player;
    public Text task;
    public GameObject shop;
    public string[] tutorialSteps;

    // Use this for initialization
    void Start()
    {
        shop.SetActive(false);
        player = Object.FindObjectOfType<Movement>();
        player.playerCanMove = false;
        Time.timeScale = 1;
        tutorialSteps = new string[]{
        "Use your mouse to aim and shoot a few times holding the left button to learn the shooting mechanism",
        "Good job! You can use the 'R' Key to reload your weapon.",
        "Awesome! Now using what you've learned, head over to the right and kill the 2 enemies. Your health is displayed at the top left.",
        "Your mother would be proud! Head over to the right and make sure you don't get caught by the cops!",
        "Congratulations, you have learned the basics! Hit SPACE BAR to play the game. Good luck!"
    };
    }

    // Update is called once per frame
    void Update()
    {

        if (tutorialState == -1 && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            UpdateTask();
        }
        if (tutorialState == 0 && Input.GetButton("Fire1"))
        {
            step0Count++;
            if (step0Count >= 5) UpdateTask(1);
        }
        if( tutorialState == 1 && Input.GetKeyDown(KeyCode.R))
        {
            player.playerCanMove = true;
            UpdateTask();
        }
        if (tutorialState == 2 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) UpdateTask();
        if (tutorialState == 3 && player.gameObject.transform.position.x > 29) UpdateTask();
		if (tutorialState == 4 && Input.GetKeyDown(KeyCode.Space)) {
			//Get the scene selected in main menu. 
			string sceneName = UIManager.sceneName;
			SceneManager.LoadScene(sceneName);
		};
        
    }
    public void UpdateTask(int? taskId = null)
    {
        tutorialState = taskId.HasValue ? taskId.Value : tutorialState + 1;
        task.text = tutorialSteps[tutorialState];
    }
}
