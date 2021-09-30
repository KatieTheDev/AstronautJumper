using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Used to check if the game has started yet
    public bool gameStarted = true;

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
        new Vector3 (-13.73f, -5.96f, -10f) // Index 1, Level 1
    };

    // Used to change location of player on game restart
    public GameObject playerObject;

    // Text objects
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI livesDisplay;
    public TextMeshProUGUI levelNumberDisplay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TimedScore", 0.25f, 0.25f); // Add 1 to score about every 1/4th of a second
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            isAlive = false;
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
        playerObject.transform.position = spawnLocation[levelNumber];
    }
}
