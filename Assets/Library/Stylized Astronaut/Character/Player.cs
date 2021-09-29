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
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("AnimationPar", 1);
            moveDirection.x = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("AnimationPar", 1);
            moveDirection.x = -speed;
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
            moveDirection.x = 0;
        }

        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
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
