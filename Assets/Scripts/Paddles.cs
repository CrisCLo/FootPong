using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddles : MonoBehaviour
{
    public float zBound = 7.0f;
    public float speed = 18.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {  if(transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,-zBound);
        } else if(transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,zBound);
        }

        
    }
}
