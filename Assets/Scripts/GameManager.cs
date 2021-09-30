using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Used to check if the game has started yet
    public bool gameStarted = false;

    // Public player property variables
    public bool isAlive = true;
    public int lives = 3;
    public int levelNumber = 1;
    public int score = 0;

    // Object to allow TurretFire to use a prefab more easily
    public GameObject enemyAmmoPrefab;

    // Spawn locations for each level in the world
    // Spaced out with one Vector3 position per line for each level in the array
    // Index number is same as level number
    private Vector3[] spawnLocation = new Vector3[] {
        new Vector3 (0, 0, 0), // Placeholder to make index number the same as level number
        new Vector3 (-13.73f, -5.5f, -10f) // Index 1, Level 1
    };

    // Camera positions for each level
    // Spaced out with one Vector3 position per line for each level with the first being for the start of the game
    // Index number is the same as level number
    private Vector3[] cameraLocation = new Vector3[]
    {
        new Vector3 (-49.1f, -10.27283f, 13.53122f),
        new Vector3 (0, 1, -37.2f)
    };
    public Vector3 teleportPos = Vector3.zero;

    // Used to change location of player on level change
    public GameObject playerObject;

    // Used to change location of camera on level change
    public GameObject cameraObject;

    // Text objects
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI livesDisplay;
    public TextMeshProUGUI levelDisplay;

    // CharacterController
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TimedScore", 0.25f, 0.25f); // Add 1 to score about every 1/4th of a second
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            isAlive = false;
        }
        if (gameStarted && isAlive)
        {
            UpdateDisplays();
        }
    }

    // Method to increase score every set time from Start method
    private void TimedScore()
    {
        if (isAlive)
        {
            score++;
        }
    }

    // Method to change score by passed amount
    public void ChangeScore(int scoreChange = 1)
    {
        if (isAlive)
        {
            score += scoreChange;
        }
    }

    // Method to remove a life from the player
    public void RemoveLife()
    {
        lives--;
        teleportPos = spawnLocation[levelNumber];
        UpdateDisplays();
    }

    public void ToggleDisplays(bool newState)
    {
        scoreDisplay.gameObject.SetActive(newState);
        livesDisplay.gameObject.SetActive(newState);
        levelDisplay.gameObject.SetActive(newState);
    }

    public void StartGame()
    {
        ToggleDisplays(false);
        cameraObject.transform.position = cameraLocation[0];
        // Enable level start
        cameraObject.transform.position = cameraLocation[1];
        //characterController.ChangePos(spawnLocation[1];
        gameStarted = true;
        levelNumber = 1;
    }

    public void UpdateDisplays()
    {
        scoreDisplay.text = "Score: " + score;
        livesDisplay.text = "Lives: " + lives;
        levelDisplay.text = "Level " + levelNumber;
    }
}
