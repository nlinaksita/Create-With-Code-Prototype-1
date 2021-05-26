using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject restartButton;
    public GameObject gameModeButtons;
    public Text player1Pause;
    public Text player2Pause;
    public Text player2Instructions;
    private bool game1Paused; // Player 1 paused the game
    private bool game2Paused; // Player 2 paused the game

    public GameObject spawnManager;

    // Player 1 game elements
    public GameObject player1;
    public Camera player1Camera;

    // Player 2 game elements
    public GameObject player2;
    public Camera player2Camera;
    // Start is called before the first frame update
    void Start()
    {
        // Clear the Pause menu UI
        DeactivateAll();
    }

    // Update is called once per frame
    void Update()
    {
        // if esc is pressed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape) && !game1Paused && !game2Paused)
        {
            game1Paused = true;
            Time.timeScale = 0;
            PlayerOnePaused();
        } else if (Input.GetKeyDown(KeyCode.Escape) && game1Paused)
        {
            game1Paused = false;
            Time.timeScale = 1;
            // deactivate all Pause UI components
            DeactivateAll();
        } else if (Input.GetKeyDown(KeyCode.Delete) && !game1Paused && !game2Paused)
        {
            game2Paused = true;
            Time.timeScale = 0;
            PlayerTwoPaused();
        } else if (Input.GetKeyDown(KeyCode.Delete) && game2Paused)
        {
            game2Paused = false;
            Time.timeScale = 1;
            // deactivate all Pause UI components
            DeactivateAll();
        }
    }

    public void ConfirmRestart()
    {
        // move to the next menu in the pause menu
        restartButton.gameObject.SetActive(false);
        gameModeButtons.gameObject.SetActive(true);
    }

    public void ConfirmSinglePlayer()
    {
        // Remove Player 2 elements
        player2Instructions.gameObject.SetActive(false);
        player2.GetComponent<Player2Controller>().ResetPosition();
        player2.gameObject.SetActive(false);
        player2Camera.gameObject.SetActive(false);

        // Restart all elements of the game
        // Reset Player 1 camera for solo play
        player1Camera.GetComponent<FollowPlayer1>().SetSolo();
        // Reset Player 1 position
        player1.GetComponent<Player1Controller>().SetSolo();

        // Reset spawn manager
        spawnManager.GetComponent<SpawnManager>().ResetGame();

        // Remove the pause menu
        DeactivateAll();
        game1Paused = false;
        game2Paused = false;

        // Start the game time
        Time.timeScale = 1;
    }

    public void ConfirmMultiPlayer()
    {
        // Modify Player 1 to multiplayer
        player1.GetComponent<Player1Controller>().SetMulti();
        player1Camera.GetComponent<FollowPlayer1>().SetMulti();

        // Reactivate player 2 objects
        player2.gameObject.SetActive(true);
        player2Camera.gameObject.SetActive(true);
        player2Instructions.gameObject.SetActive(true);

        // Reset spawn manager
        spawnManager.GetComponent<SpawnManager>().ResetGame();

        // Remove the pause menu
        DeactivateAll();
        game1Paused = false;
        game2Paused = false;

        // Start the game time
        Time.timeScale = 1;
    }

    private void DeactivateAll()
    {
        pauseMenu.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        gameModeButtons.gameObject.SetActive(false);
        player1Pause.gameObject.SetActive(false);
        player2Pause.gameObject.SetActive(false);
    }

    private void PlayerOnePaused()
    {
        pauseMenu.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        player1Pause.gameObject.SetActive(true);
    }

    private void PlayerTwoPaused()
    {
        pauseMenu.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        player2Pause.gameObject.SetActive(true);
    }
}
