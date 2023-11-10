using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>FollowPlayer</c> models a point in a two-dimensional plane.
/// This follow player class will update the events of the camera from the vehicle as a player.
/// Standar coding documentation can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    /// <value>Property<c>player</c>Instantiate player</value> 
    public GameObject player;
    /// <value>Property<c>offset</c>Create offset of camera when moving</value> 
    private Vector3 offset = new Vector3(0, 6, -7);

    /// <summary>
    /// Method <c>Start</c>This method is called once per frame
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Method <c>LateUpdate</c>This method is called once per frame
    /// </summary>
    void LateUpdate()
    {
        // Take the camera position of the vehicle
        transform.position = player.transform.position + offset;
    }
}
