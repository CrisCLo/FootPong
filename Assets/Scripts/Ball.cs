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
    void Start()
    {
        ballcollider = GetComponent<BoxCollider>();
        ballRb = GetComponent<Rigidbody>();
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

    Vector3 RandomServe(int direction)
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

 private void OnCollisionEnter(Collision other) //Increase ball speed on contact with Game Object
    {
        if(ballSpeed<=speedCap)
        {
        ballSpeed += increaseIncrement;
        if(other.gameObject.CompareTag("SWALL"))
        {
            increasedForce = ballSpeed *Vector3.forward;
            ballRb.AddForce(increasedForce*Time.deltaTime,ForceMode.Impulse);
        }
        if(other.gameObject.CompareTag("NWALL"))
        {
            increasedForce = ballSpeed *Vector3.right;
            ballRb.AddForce(increasedForce*Time.deltaTime,ForceMode.Impulse);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            increasedForce = ballSpeed *Vector3.right;
            ballRb.AddForce(increasedForce*Time.deltaTime,ForceMode.Impulse);
        } else if(other.gameObject.CompareTag("Opp"))
        {  
            increasedForce = ballSpeed *Vector3.left;
            ballRb.AddForce(increasedForce*Time.deltaTime,ForceMode.Impulse);
        }
    }
    }
    
    private void OnTriggerEnter(Collider other) // Ball resets after goal, score is tallied
    {
        if(other.gameObject.CompareTag("O Goal"))
        {
            Serve();
            gameManager.PlayerGoal();
        } else if(other.gameObject.CompareTag("P Goal"))
        {
            Serve();
            gameManager.OppGoal();
        }
    }
}

