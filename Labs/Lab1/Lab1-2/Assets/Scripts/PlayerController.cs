using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PlayerController</c> models a point in a two-dimensional plane.
/// This player controller class will update the events from the vehicle player.
/// Standar coding documentation can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <value>Property <c>speed</c>Adjust car velocity</value>
    public float speed = 5.0f;
    /// <value>Property <c>turnSpeed</c>Adjust speed turn velocity</value>
    public float turnSpeed = 0.0f;
    /// <value>Property <c>horizontalInput</c>Adjust horizontal input</value>
    public float horizontalInput;
    /// <value>Property <c>forwardInput</c>Adjust forward/backward input</value>
    public float forwardInput;
    
    /// <summary>
    /// Method <c>Start</c>This method is called once per frame
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Method <c>Update</c>This method is called once per frame
    /// </summary>
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move vehicle straight
        //transform.Translate(0,0,1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Move the car to the right
        // transform.Translate(Vector3.right * Time.deltaTime * horizontalInput);
        transform.Rotate(Vector3.up, horizontalInput);
    }
}
