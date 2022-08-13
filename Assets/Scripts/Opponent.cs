using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Paddles
{
    public GameObject Ball;
    private float trackingInput;
    Vector3 ballDirection;
    public float oppSpeed = 18.0f;
    private GameManager gameManager;
    public bool playerTwo = false;
    private int difficulty;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(!playerTwo){
        if(Ball.transform.position.z < transform.position.z&&transform.position.z>-zBound)
        {
            transform.Translate(0,0,-oppSpeed*Time.deltaTime);
        }if (Ball.transform.position.z > transform.position.z && transform.position.z<zBound)
        {
            transform.Translate(0,0,oppSpeed*Time.deltaTime);
        }
       }
        if(playerTwo)
        {
            if(Input.GetKey("up"))
            {
                transform.Translate(0,0,speed*Time.deltaTime);
            }if(Input.GetKey("down"))
            {
                transform.Translate(0,0,-speed*Time.deltaTime);
            }
        }

        if(Input.GetKey("escape") || Input.GetKey("r"))
        {
            oppSpeed = 18.0f;
        }
        
    }
    public bool aI(int difficulty)
    {
        if(difficulty == 4)
        {
            playerTwo = true;
            return false;
        } else 
        {
            oppSpeed/=difficulty;
            return true;
        }
    }
}
