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
    public GameObject redWinsText;
    public GameObject blueWinsText;
    public Button redWinRestartButton; 
    public Button blueWinRestartButton;
    public Opponent opp;
    private int savedDiff;
    private bool paused = false;
    public GameObject pauseMenu;
    private AudioSource gameAudio;
    public AudioClip gameOverSound;

    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    public void StartGame(int difficulty) // Starts gameply once difficulty button is pressed
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
        opp.aI(difficulty); // Sends difficulty info to Opponent paddle
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
            ReturnToMainMenu();
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

    public void ReturnToMainMenu() // Allows for main menu button to return game to main menu state.
    {
        ball.ResetBall();
        titleScreen.gameObject.SetActive(true);
        playerScoreText.gameObject.SetActive(false);
        oppScoreText.gameObject.SetActive(false);
        redWinsText.gameObject.SetActive(false);
        blueWinsText.gameObject.SetActive(false);
    }
    private void PauseToggle(bool pause) // Pauses and unpauses game 
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
    private void RestartGame() //Specific function allows for use of  restart button and r key to restart game
    {
        StartGame(savedDiff);
    }

    public void OppGoal() //Tallies blue paddle's score
    {
        oppScore +=1;
        oppScoreText.text = oppScore.ToString();
        goalCap();
    }
    public void PlayerGoal() // Tallies red paddle's score
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
        goalCap();
    }

    public bool goalCap() // Game is played to 10 goals, first to reach that score wins and ends the game.
    {
        if(playerScore == 10)
        {
            gameAudio.PlayOneShot(gameOverSound,3.0f);
            Time.timeScale = 0;
            playerScoreText.gameObject.SetActive(false);
            oppScoreText.gameObject.SetActive(false);
            redWinRestartButton.gameObject.SetActive(true);
            redWinsText.gameObject.SetActive(true);
            isGameActive = false;
            return true;
        } else if(oppScore == 10)
        {
            gameAudio.PlayOneShot(gameOverSound,3.0f);
            Time.timeScale=0;
            playerScoreText.gameObject.SetActive(false);
            oppScoreText.gameObject.SetActive(false);
            blueWinRestartButton.gameObject.SetActive(true);
            blueWinsText.gameObject.SetActive(true);
            isGameActive = false;
            return true;
        } else {return false;}
    }
}

