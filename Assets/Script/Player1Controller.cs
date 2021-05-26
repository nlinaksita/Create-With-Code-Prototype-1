using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    private float speed = 20f;
    private float turnSpeed = 45f;

    // Default values
    private static float defaultSpeed = 20f;
    private static float defaultTurnSpeed = 45f;
    private static float increaseSpeed = 2f;
    private static float increaseTurnSpeed = 5f;

    private float horizontalInput;
    private float forwardInput;

    private GameObject spawnManager;
    private GameObject player2;
    private Vector3 soloStartPosition = Vector3.zero;
    private Vector3 multiStartPosition = new Vector3(-3, 0, 0);
    private Vector3 startPosition;

    // Scoring system
    public Text scoreText;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");
        player2 = GameObject.Find("Player2");

        // Background splash screen will be of a multiplayer game
        SetMulti();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal_P1");
        forwardInput = Input.GetAxis("Vertical_P1");

        // Move the vehicle forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Move the vehicle forward based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

        // If the player falls through the floor, reset position
        if (transform.position.y < -1)
        {
            ResetPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "Finish")
        {
            // Update score
            score++;
            SetScoreText(score);

            // Reset player position
            transform.position = Vector3.zero;
            player2.GetComponent<Player2Controller>().ResetPosition();

            // Reset spawnManager objects
            spawnManager.GetComponent<SpawnManager>().LevelUp();

            // Increase speed
            IncreaseSpeed();
            player2.GetComponent<Player2Controller>().IncreaseSpeed();
        }
    }

    public void ResetPosition()
    {
        transform.rotation = Quaternion.identity;
        transform.position = startPosition;
    }

    public void SetMulti()
    {
        // Set startPosition to multiplayer start position (left of center)
        startPosition = multiStartPosition;
        ResetPosition();
        ResetSpeed();
        score = 0;
        SetScoreText(score);
    }

    public void SetSolo()
    {
        // Set startPosition to solo start position (left of center)
        startPosition = soloStartPosition;
        ResetPosition();
        ResetSpeed();
        score = 0;
        SetScoreText(score);
    }

    public void IncreaseSpeed()
    {
        speed += increaseSpeed;
        turnSpeed += increaseTurnSpeed;
    }

    private void ResetSpeed()
    {
        speed = defaultSpeed;
        turnSpeed = defaultTurnSpeed;
    }

    private void ResetScore()
    {
        score = 0;
        SetScoreText(score);
    }

    private void SetScoreText(int s)
    {
        scoreText.text = "P1 Score: " + s;
    }
}
