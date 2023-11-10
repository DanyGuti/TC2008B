using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //Variables de colisión
    public GameObject rock;

    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Vector3.forward * speed * Time.deltaTime);
    }

    public void StopMotion()
    {
        speed = 0;
    }
}
