using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float lookSpeed = 3;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 lookRotation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        lookRotation.y += -Input.GetAxis("Mouse X");
        lookRotation.x += Input.GetAxis("Mouse Y");

        transform.eulerAngles = (Vector2)lookRotation * lookSpeed;

        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
    }
}
