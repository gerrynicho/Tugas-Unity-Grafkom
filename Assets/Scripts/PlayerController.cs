using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float velocity = 0f;
    public TextMeshProUGUI countText; // HUD
    public GameObject winTextObject; // UI

    public TextMeshProUGUI timerText;
    private float currentTime = 0f;
    private bool isTimerRunning = true;

    public static bool isGameOver = false;
    public Action OnSpeedChange;

    public GameUIHandler speeduihandler;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = 0f; // Range is 0 to speed
        count = 0;
        currentTime = 0f;
        isTimerRunning = true;
        setCountText();
        // UI should be in control when to start game
        //isGameOver = false; 
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update()
    {
        if (isTimerRunning && !isGameOver)
        {
            currentTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerText.text = string.Format("Time: {0:D2}:{1:D2}:{2:D2}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8) // Win condition
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            isGameOver = true;
            speed = 0;
            velocity = 0f;
            isTimerRunning = false;
            TextMeshProUGUI winText = winTextObject.GetComponent<TextMeshProUGUI>();
            OnSpeedChange?.Invoke(); // We dont know if anyone is listening so it might be null
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        if (!isGameOver)
        {
            rb.AddForce(movement * speed);
            velocity = rb.linearVelocity.magnitude;
            OnSpeedChange?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isGameOver)
        {   
            isTimerRunning = false;
            Destroy(this.gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            speeduihandler.SetSpeed(speed);
        }
    }
}
