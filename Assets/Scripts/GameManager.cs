using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Enable debug mode which has extra event logging
    public bool debugMode = true;

    // Used to check if the game has started yet
    public bool gameStarted = false;

    // Public player property variables
    public bool isAlive = false;
    public int lives = 3;
    public int levelNumber = 0;
    public int score = 0;
    public int levelKilled = 0;

    // Boss kill check
    public bool bossKilled = false;
    private int totalLevels = 5;

    // Used for pausing
    public bool isPaused = false;

    // Object to allow TurretFire to use a prefab more easily
    public GameObject enemyAmmoPrefab;

    // Spawn locations for each level in the world
    // Spaced out with one Vector3 position per line for each level in the array
    // Index number is same as level number
    private Vector3[] spawnLocation = new Vector3[] {
        new Vector3(0, 0, 0), // Placeholder to make index number the same as level number
        new Vector3(-13.73f, -5.5f, -10f), // Index 1, Level 1
        new Vector3(52.97f, -5.5f, -10f), // Level 2
        new Vector3(122.8f, -5.5f, -10f), // Level 3
        new Vector3(186.58f, -5.5f, -10f), // Level 4
        new Vector3(256.19f, -5.5f, -10f) // Boss level / level 5
    };
    public Vector3 teleportPos = Vector3.zero; // Used for teleportation

    // Camera positions for each level
    // Spaced out with one Vector3 position per line for each level with the first being for the start of the game
    // Index number is the same as level number
    private Vector3[] cameraLocation = new Vector3[]
    {
        new Vector3(-68.82258f, 1f, -37.2f), // Camera position for starting location with only a background
        new Vector3(0, 1, -37.2f), // Camera position for level 1
        new Vector3(66.17741f, 1, -37.2f), // Camera position for level 2
        new Vector3(135.9774f, 1, -37.2f), // Camera position for level 3
        new Vector3(198.7774f, 1, -37.2f), // Camera position for level 4
        new Vector3(269.1774f, 1, -37.2f) // Camera position for boss level / level 5
    };

    // Array for how many enemies are in each level
    private int[] levelEnemies = new int[]
    {
        0, // Placeholder to keep index right
        1, // Enemies in level 1
        2, // Enemies in level 2
        5, // Enemies in level 3
        6, // Enemies in level 4
        6 // Enemies in boss level / level 5
    };

    public int enemiesToKill;

    // Used to change location of player on level change
    public GameObject playerObject;

    // Used to change location of camera on level change
    public Transform cameraTransform;

    // Text objects
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI livesDisplay;
    public TextMeshProUGUI levelDisplay;
    public GameObject textDisplays;
    public GameObject startScreen;
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Reset all variables to bypass editor overrides
        gameStarted = false;
        isAlive = false;
        lives = 3;
        levelNumber = 0;
        score = 0;
        levelKilled = 0;
        cameraTransform.position = cameraLocation[0];
        teleportPos = spawnLocation[0];
        enemiesToKill = levelEnemies[0];
        ToggleDisplays(false);
        isPaused = false;
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;

        InvokeRepeating("TimedScore", 0.25f, 0.25f); // Add 1 to score about every 1/4th of a second
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !isPaused)
        {
            if (lives < 1)
            {
                LoseGame();
            }
            if (isAlive)
            {
                UpdateDisplays();
            }

            if (levelKilled == levelEnemies[levelNumber] && isAlive && levelNumber < totalLevels)
            {
                ChangeLevel(levelNumber + 1);
            }
            else if (levelKilled == levelEnemies[levelNumber] && isAlive && levelNumber == totalLevels)
            {
                WinGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        UpdateDisplays();
    }

    // Method to increase score every set time from Start method
    private void TimedScore()
    {
        if (isAlive && gameStarted && !isPaused)
        {
            score++;
        }
    }

    // Method to change score by passed amount
    public void ChangeScore(int scoreChange = 1)
    {
        if (isAlive && gameStarted && !isPaused)
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
        textDisplays.gameObject.SetActive(newState);
    }

    public void StartGame()
    {
        // Start level 1
        startScreen.gameObject.SetActive(false);
        ToggleDisplays(true);
        enemiesToKill = levelEnemies[1];
        levelNumber = 1;
        levelKilled = 0;
        cameraTransform.position = cameraLocation[1];
        teleportPos = spawnLocation[1];
        UpdateDisplays();
        gameStarted = true;
        isAlive = true;
    }

    public void UpdateDisplays()
    {
        scoreDisplay.text = "Score: " + score;
        livesDisplay.text = "Lives: " + lives;
        levelDisplay.text = "Level " + levelNumber;
    }

    private void ChangeLevel(int newLevel)
    {
        if (debugMode)
        {
            enemiesToKill = levelEnemies[newLevel];
            levelNumber = newLevel;
            Debug.Log("Updated level number to " + levelNumber);
            levelKilled = 0;
            Debug.Log("Reset levelKilled to " + levelKilled);
            cameraTransform.position = cameraLocation[levelNumber];
            Debug.Log("Teleported camera to new level");
            teleportPos = spawnLocation[levelNumber];
            Debug.Log("Sent teleportation coordinates to Player");
            UpdateDisplays();
            Debug.Log("Displays updated");
        }
        else if (!debugMode)
        {
            enemiesToKill = levelEnemies[newLevel];
            levelNumber = newLevel;
            levelKilled = 0;
            cameraTransform.position = cameraLocation[levelNumber];
            teleportPos = spawnLocation[levelNumber];
            UpdateDisplays();
        }
    }

    public void RestartLevel()
    {
        ChangeLevel(levelNumber);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.gameObject.SetActive(false);
            ToggleDisplays(true);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pauseScreen.gameObject.SetActive(true);
            ToggleDisplays(false);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void WinGame()
    {
        isPaused = true;
        isAlive = false;
        gameStarted = false;
        winScreen.gameObject.SetActive(true);
        ToggleDisplays(false);
        Time.timeScale = 0;
    }

    private void LoseGame()
    {
        isPaused = true;
        isAlive = false;
        gameStarted = false;
        loseScreen.gameObject.SetActive(true);
        ToggleDisplays(false);
        Time.timeScale = 0;
    }
}
