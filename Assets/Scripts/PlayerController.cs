using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using Cursor = UnityEngine.Cursor;

[Serializable]
public class DeathScreens
{
    public PlayerController.DeathTypes deathType;
    public UIDocument deathScreen;
}

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
    [SerializeField] private DeathScreens[] deathScreens;
    [SerializeField] private UIDocument winScreen;
    public ParticleSystem bubbleEmitter;
    private float timer;

    public AudioSource breatheAudio;
    public AudioSource bubbleAudio;


    private static bool isDead = false;
    public static bool IsDead()
    {
        return isDead;
    }

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject altCamera;


    // Start is called before the first frame update
    void Start()
    {
        BeginTimer();
        timer = 0;
        foreach (var deathScreen in deathScreens)
        {
            deathScreen.deathScreen.rootVisualElement.style.display = DisplayStyle.None;
            deathScreen.deathScreen.rootVisualElement.Q<Button>("restart").clicked += Restart;
            deathScreen.deathScreen.rootVisualElement.Q<Button>("quit").clicked += Quit;
        }

        winScreen.rootVisualElement.style.display = DisplayStyle.None;
        winScreen.rootVisualElement.Q<Button>("restart").clicked += Restart;
        winScreen.rootVisualElement.Q<Button>("quit").clicked += Quit;
        altCamera.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        lookRotation.y += Input.GetAxis("Mouse X");
        lookRotation.x += -Input.GetAxis("Mouse Y");

        transform.eulerAngles = (Vector2)lookRotation * lookSpeed;

        if (!isDead)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeRemaining = breathTimer;
            StartCoroutine(PlayBubbles());
            breatheAudio.Play();
            bubbleAudio.Play();
        }
    }
    private void FixedUpdate()
    {
        if (Mathf.Approximately(horizontalInput, 0) && Mathf.Approximately(verticalInput, 0))
        {
            playerRb.velocity = new Vector3(0, 0, 0);
        }
        Vector3 input = new Vector3(horizontalInput, 0, verticalInput).normalized;
        input = playerRb.rotation * input;
        playerRb.MovePosition(transform.position + input * Time.fixedDeltaTime * speed);
    }

    IEnumerator PlayBubbles()
    {
        Debug.Log("playing");
        bubbleEmitter.Play();
        yield return new WaitForSeconds(2);
        bubbleEmitter.Stop();
        Debug.Log("stopped");
    }

    public void BeginTimer()
    {
        if (!isCounting)
        {
            isCounting = true;
            timeRemaining = breathTimer;
            Invoke(nameof(Countdown), 1f);
        }
    }

    public void Countdown()
    {
        timeRemaining--;
        //Debug.Log(timeRemaining);

        if (timeRemaining > 0)
        {
            Invoke("Countdown", 1f);
        }
        else
        {
            Debug.Log("end timer");
            Die(DeathTypes.Drown);
        }
    }

    public enum DeathTypes
    {
        Drown,
        Monster,
        Other
    }
    
    

    public void Die(DeathTypes method)
    {
        Time.timeScale = 0;
        isDead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (method == DeathTypes.Monster)
        {
            var cam = GetComponentInChildren<Camera>();
            cam.enabled = false;
            var altCam = altCamera.GetComponent<Camera>();
            altCamera.SetActive(true);
            altCam.enabled = true;
            videoPlayer.Play();
            _ = new WaitForSeconds(2);
        }
        foreach (var deathScreen in deathScreens)
        {
            if (deathScreen.deathType == method)
            {
                deathScreen.deathScreen.rootVisualElement.style.display = DisplayStyle.Flex;
                deathScreen.deathScreen.rootVisualElement.Q<Label>("time").text = GetPrettyTime();
            }
        }

    }

    private string GetPrettyTime()
    {
        var fancyTime = TimeSpan.FromSeconds(timer);
        return fancyTime.Minutes + ":" + fancyTime.Seconds + ":" + fancyTime.Milliseconds; 
    }

    public void Win()
    {
        isDead = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winScreen.rootVisualElement.style.display = DisplayStyle.Flex;
        winScreen.rootVisualElement.Q<Label>("time").text = GetPrettyTime();
    }
    
    private void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    private void Quit() {
        Application.Quit();
    }
}
