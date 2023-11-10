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

    // Instancia de las cámaras para aplicar Canvas
    public FollowPlayer MainCamera;

    /// <value>Property <c>speed</c>Adjust car velocity</value>
    public float speed = 5.0f;
    /// <value>Property <c>turnSpeed</c>Adjust speed turn velocity</value>
    public float turnSpeed = 0.0f;
    /// <value>Property <c>horizontalInput</c>Adjust horizontal input</value>
    public float horizontalInput;
    /// <value>Property <c>forwardInput</c>Adjust forward/backward input</value>
    public float forwardInput;
    public float turbo = 3.0f;

    private float maxWheelRotation = 45f;
    private float totalRotation = 0f;
    private float rotationSpeed = 100f;
    public GameObject frontLeftWheel;
    public GameObject frontRightWheel;
    
    ///Variables cámara
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey; //Tecla que permite cambiar entre cámaras

    //Variables multijugador
    public string inputId;

    //Variables de colisión
    public GameObject rock;
    public GameObject flameObject;


    public DeformableMesh deformCar;
    private ContactPoint[] contacts;

    // Distancia para saber si se está en caída 
    private float fallThreshold = -10.0f;
    private bool hasFalled = false; // Flag to track if the player has fallen

    /// <summary>
    /// Realizar GameOverSetup
    /// </summary>
    /// <param name="playerController"></param>
    public void GameOver(PlayerController playerController, bool hasWon)
    {
        MainCamera.GameOverSetup(playerController, hasWon);
    }

    /// <summary>
    /// Method <c>Start</c>This method is called once per frame
    /// </summary>
    void Start()
    {
        deformCar = GetComponent<DeformableMesh>();
        rock = GameObject.Find("RocasTraseras");
    }

    /// <summary>
    /// Method <c>Update</c>This method is called once per frame
    /// </summary>
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);
        var turboInput = Input.GetAxis("Fire" + inputId);

        float rotationAmount = horizontalInput * Time.deltaTime * rotationSpeed;
        totalRotation += rotationAmount; // Accumulate total rotation

        transform.Rotate(Vector3.up, rotationAmount);
        Vector3 forwardDirection = transform.forward;
        
        //transform.Translate(0,0,1);
        if(turboInput != 0.0f)
        {
            showFlame(inputId, true);
            transform.Translate(forwardDirection * Time.deltaTime * speed * forwardInput * turbo);
        }
        else
        {
            showFlame(inputId, false);
            transform.Translate(forwardDirection * Time.deltaTime * speed * forwardInput);
        }

        // Move the car to the right
        // transform.Translate(Vector3.right * Time.deltaTime * horizontalInput);
        // Limit the total rotation within the specified range
        if (Mathf.Abs(totalRotation + rotationAmount) <= maxWheelRotation)
        {
            // Apply rotation
            //transform.Rotate(Vector3.up, rotationAmount);
            /// When rotation negative on axis
            frontLeftWheel.transform.Rotate(Vector3.up, (rotationAmount));
            frontRightWheel.transform.Rotate(Vector3.up, (rotationAmount));
        }

        // Cambio entre cámaras
        if(Input.GetKeyDown(switchKey)) 
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
        if (transform.position.y < fallThreshold && !hasFalled)
        {
            hasFalled = true;
            GameOver(this, false);
        }

    }

    void showFlame(string id, bool isActive)
    {
        if(isActive)
        {
            GameObject flameObject = GameObject.Find("Flame" + id);
            if (flameObject != null)
            {
                flameObject.SetActive(isActive);
            }
            else
            {
                Debug.Log("Flame Object not found!");
            }
        } 
        flameObject.gameObject.SetActive(isActive);
    }
    /// <summary>
    /// Any collision handle (bus, rock and barrier)
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // If collision with the Bus
        if (collision.gameObject.CompareTag("Bus"))
        {
            HandleBusCollision(collision);
        }
        // If collision with a Rock
        else if (collision.gameObject.CompareTag("Rock"))
        {
            HandleRockCollision(collision);
        }
        // If collision with barrier, someone has won
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            hasFalled = false;
            speed = 0;
            GameOver(this, true);
        }
    }

    /// <summary>
    /// Rock collision
    /// </summary>
    /// <param name="collision"></param>
    void HandleRockCollision(Collision collision)
    {
        contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        Vector3 averageCollisionNormal = HelperCollisionNormal(collision, contacts);
        foreach(var contact in contacts)
        {
            deformCar.CalculateDepression(contact, 10.0f);
        }
        // Calculate the component of the speed in the collision direction
        float speedInCollisionDirection = Vector3.Dot(averageCollisionNormal, transform.forward) * speed;

        // Slow down the object in the collision direction
        float slowdownRate = 0.5f; // Adjust the slowdown rate as needed
        speedInCollisionDirection -= slowdownRate * Time.deltaTime;

        // Calculate the new speed after slowdown
        float newSpeed = Mathf.Max(speedInCollisionDirection, 0.0f);

        // Move the object
        transform.Translate(Vector3.forward * Time.deltaTime * newSpeed);
    }

    /// <summary>
    /// Handle Bus collision
    /// </summary>
    /// <param name="collision"></param>
    void HandleBusCollision(Collision collision)
    {
        contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        Vector3 averageCollisionNormal = HelperCollisionNormal(collision, contacts);
        foreach(var contact in contacts)
        {
            deformCar.CalculateDepression(contact, 10.0f);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * (speed - 5.0f));
    }

    /// <summary>
    /// Collision normal average calculation
    /// </summary>
    /// <param name="collision"></param>
    /// <return> <c>Vector3</c> Average Normal collision</return>
    Vector3 HelperCollisionNormal(Collision collision, ContactPoint[] contacts)
    {
         // Calculate the average collision normal
        Vector3 averageCollisionNormal = Vector3.zero;
        foreach (var contact in contacts)
        {
            averageCollisionNormal += contact.normal;
        }
        return averageCollisionNormal /= contacts.Length;
    }
}
