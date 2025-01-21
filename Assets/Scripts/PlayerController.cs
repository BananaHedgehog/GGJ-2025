using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float lookSpeed = 3;
    private float horizontalInput;
    private float verticalInput;
    public Rigidbody playerRb;
    private Vector2 lookRotation = Vector2.zero;
    private bool isCounting;
    public int breathTimer = 30;
    public int timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        BeginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        lookRotation.y += Input.GetAxis("Mouse X");
        lookRotation.x += -Input.GetAxis("Mouse Y");

        transform.eulerAngles = (Vector2)lookRotation * lookSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeRemaining = breathTimer;
        }
    }
    private void FixedUpdate()
    {
        Vector3 input = new Vector3(horizontalInput, 0, verticalInput).normalized;
        input = playerRb.rotation * input;
        playerRb.MovePosition(transform.position + input * Time.fixedDeltaTime * speed);
    }

    public void BeginTimer()
    {
        if (!isCounting)
        {
            isCounting = true;
            timeRemaining = breathTimer;
            Invoke("Countdown", 1f);
        }
    }

    public void Countdown()
    {
        timeRemaining--;
        Debug.Log(timeRemaining);

        if (timeRemaining > 0)
        {
            Invoke("Countdown", 1f);
        }
        else
        {
            Debug.Log("end timer");
        }
    }
}
