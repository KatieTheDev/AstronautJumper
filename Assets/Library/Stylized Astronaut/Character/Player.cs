using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private Animator anim;
    private CharacterController controller;

    public float speed = 600.0f;
    // public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (gameManager.teleportPos != Vector3.zero)
        {
            if (gameManager.debugMode)
            {
                controller.enabled = false;
                Debug.Log("Disabled playercontroller");
                transform.position = gameManager.teleportPos;
                Debug.Log("Teleported player to " + transform.position);
                controller.enabled = true;
                Debug.Log("Enabled playercontroller");
                gameManager.teleportPos = Vector3.zero;
            }
            else if (!gameManager.debugMode)
            {
                controller.enabled = false;
                transform.position = gameManager.teleportPos;
                controller.enabled = true;
                gameManager.teleportPos = Vector3.zero;
            }
        }
        if (Input.GetKey(KeyCode.D) && gameManager.gameStarted && gameManager.isAlive && !gameManager.isPaused)
        {
            anim.SetInteger("AnimationPar", 1);
            moveDirection.x = speed;
        }
        else if (Input.GetKey(KeyCode.A) && gameManager.gameStarted && gameManager.isAlive && !gameManager.isPaused)
        {
            anim.SetInteger("AnimationPar", 1);
            moveDirection.x = -speed;
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
            moveDirection.x = 0;
        }

        if (controller.isGrounded && Input.GetKey(KeyCode.Space) && gameManager.gameStarted && gameManager.isAlive && !gameManager.isPaused)
        {
            moveDirection.y = speed * 2;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        /*float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);*/
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
