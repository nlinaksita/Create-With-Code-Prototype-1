using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    private float speed = 20f;
    private float turnSpeed = 45f;

    // Default speed values
    private static float defaultSpeed = 20f;
    private static float defaultTurnSpeed = 45f;
    private static float increaseSpeed = 2f;
    private static float increaseTurnSpeed = 5f;

    private float horizontalInput;
    private float forwardInput;

    private GameObject spawnManager;
    private GameObject player1;
    private Vector3 startPosition = new Vector3(3,0,0);

    // Scoring system
    public Text scoreText;
    public int score;

    
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");
        player1 = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal_P2");
        forwardInput = Input.GetAxis("Vertical_P2");

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
            player1.GetComponent<Player1Controller>().ResetPosition();

            // Reset spawnManager objects
            spawnManager.GetComponent<SpawnManager>().LevelUp();

            // Increase speed
            IncreaseSpeed();
            player1.GetComponent<Player1Controller>().IncreaseSpeed();
        }
    }

    public void SetMulti()
    {
        ResetPosition();
        ResetSpeed();
        score = 0;
        SetScoreText(score);
    }

    public void ResetPosition()
    {
        transform.rotation = Quaternion.identity;
        transform.position = startPosition;
    }
    public void IncreaseSpeed()
    {
        speed += increaseSpeed;
        turnSpeed += increaseTurnSpeed;
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
        turnSpeed = defaultTurnSpeed;
        score = 0;
        SetScoreText(score);
    }

    private void SetScoreText(int s)
    {
        scoreText.text = "P2 Score: " + s;
    }
}
