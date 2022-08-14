using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody ballRb;
    private BoxCollider ballcollider;
    private GameManager gameManager;
    public float ballSpeed = 20.0f;
    private Vector3 increasedForce;
    [SerializeField] float increaseIncrement = 2.0f;
    private int serveDirection;
    [SerializeField] float serveSpeed = 500.0f;
    [SerializeField] float speedCap = 100.0f;
    private AudioSource ballAudio;
    public AudioClip ballImpact;
    public AudioClip playerGoalSound;
    public AudioClip oppGoalSound;

    void Start()
    {
        ballcollider = GetComponent<BoxCollider>();
        ballRb = GetComponent<Rigidbody>();
        ballAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    public void ResetBall() //kills ball speed and resets position at center of field
    {
        transform.position = new Vector3(0,1,0);
        ballSpeed= 0.0f;
        ballRb.AddForce(Vector3.zero*ballSpeed,ForceMode.Impulse);
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }

    Vector3 RandomServe(int direction) //Randomizes ball serve direction and imports it into serve function
    {
        if(direction>2)
        {return new Vector3(Random.Range(8,17),1,Random.Range(-6,6)).normalized;}
        else {return new Vector3(Random.Range(-17,8),1,Random.Range(-6,6)).normalized;}
    }

    public void Serve() //Serves ball at beginning of game or after each goal
    {
        ResetBall();
        if(gameManager.isGameActive)
        {
        serveDirection = Random.Range(0,4);
        ballRb.AddForce(RandomServe(serveDirection)*serveSpeed*Time.deltaTime,ForceMode.Impulse);
        }
    }

    private void IncreaseSpeed(Vector3 increasedForce) // Increases speed by desired increment,adds force and plays sound effect
    {
        if(ballSpeed<=speedCap)
        {
            ballSpeed += increaseIncrement;
        }
        ballAudio.PlayOneShot(ballImpact, 1.0f);
        ballRb.AddForce(increasedForce*Time.deltaTime,ForceMode.Impulse);
    }

 private void OnCollisionEnter(Collision other) //Increase ball speed on contact with Game Object, using Vectors to influence ball direction based on position of game object
    {
        if(other.gameObject.CompareTag("SWALL"))
        {
            increasedForce = ballSpeed *Vector3.forward;
            IncreaseSpeed(increasedForce);
        }
        if(other.gameObject.CompareTag("NWALL"))
        {
            increasedForce = ballSpeed *Vector3.right;
            IncreaseSpeed(increasedForce);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            increasedForce = ballSpeed *Vector3.right;
            IncreaseSpeed(increasedForce);
        } else if(other.gameObject.CompareTag("Opp"))
        {
            increasedForce = ballSpeed *Vector3.left;
            IncreaseSpeed(increasedForce);
        }
    }
    
    
    private void OnTriggerEnter(Collider other) // Ball resets after goal, goal audio is played and score is tallied
    {
        if(other.gameObject.CompareTag("O Goal"))
        {
            Serve();
            ballAudio.PlayOneShot(oppGoalSound, 1.0f);
            gameManager.PlayerGoal();
        } else if(other.gameObject.CompareTag("P Goal"))
        {
            Serve();
            ballAudio.PlayOneShot(playerGoalSound, 1.0f);
            gameManager.OppGoal();
        }
    }
}


