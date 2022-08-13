using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int oppScore;
    public bool isGameActive;
    public Ball ball;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI oppScoreText;
    public GameObject titleScreen;
    public TextMeshProUGUI redWinsText;
    public TextMeshProUGUI blueWinsText;
    public Button restartButton; 
    public Opponent opp;
    private int savedDiff;
    private bool paused = false;
    public GameObject pauseMenu;

    void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
    }

    public void StartGame(int difficulty)
    {
        savedDiff = difficulty;
        isGameActive = true;
        playerScore = 0;
        oppScore = 0;
        titleScreen.gameObject.SetActive(false);
        playerScoreText.gameObject.SetActive(true);
        oppScoreText.gameObject.SetActive(true);
        oppScoreText.text = oppScore.ToString();
        playerScoreText.text = playerScore.ToString();
        opp.aI(difficulty);
        ball.Serve();
    }

    void Update()
    {
        if(Input.GetKey("r")) // offers ability to restart game at any time
        {
            RestartGame();
        }
        if(Input.GetKey("escape")) // Quits back to main menu
        {
            ball.ResetBall();
            titleScreen.gameObject.SetActive(true);
            playerScoreText.gameObject.SetActive(false);
            oppScoreText.gameObject.SetActive(false);
        }
        if(Input.GetKey("backspace")) //Exits app
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Space)) // Pauses game
        {
            PauseToggle(paused);
        }
    }
    private void PauseToggle(bool pause)
    {
        if(!paused)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        } else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }
    private void RestartGame() //Specific function allows for use of button
    {
        StartGame(savedDiff);
    }

    public void OppGoal()
    {
        oppScore +=1;
        oppScoreText.text = oppScore.ToString();
        goalCap();
    }
    public void PlayerGoal()
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
        goalCap();
    }

    public bool goalCap() // Game is played to 10 goals, first to reach that score wins
    {
        if(playerScore == 10)
        {
            Time.timeScale = 0;
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
            return true;
        } else if(oppScore == 10)
        {
            Time.timeScale=0;
            playerScoreText.gameObject.SetActive(false);
            oppScoreText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
            return true;
        } else {return false;}
    }
}

