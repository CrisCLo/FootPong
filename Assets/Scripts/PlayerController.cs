using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Paddles
{
    private float verticalInput;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void FixedUpdate()
    {  if(transform.position.z < -zBound) // Bounds player's movement so paddle does not exit playing field.
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,-zBound);
        } else if(transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,zBound);
        }
       if(Input.GetKey("w")) // Input that allows W and S keys to move player's paddle vertically.
       {
            transform.Translate(0,0,speed*Time.deltaTime);
       }
       if(Input.GetKey("s"))
        {
            transform.Translate(0,0,-speed*Time.deltaTime);
        }
    }
}

 