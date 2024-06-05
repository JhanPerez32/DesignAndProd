using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public float playerSpeed;
    public float jump;
    public float gravity;
    public float groundDistance;

    [Header("Check Ground")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;
    public bool isWallRunning;

    protected virtual void Start()
    {
        if(PlayerManager.Instance != null)
        {
            playerSpeed = PlayerManager.Instance.moveSpeed;
            groundDistance = PlayerManager.Instance.groundDist;
            jump = PlayerManager.Instance.jumpHeight;
        }
        if(GravityManager.Instance != null)
        {
            gravity = GravityManager.Instance.gravityScale;
        }
        else
        {
            Debug.LogError("Player/Gravity Manager Instance is Null");
        }
    }

    protected virtual void Update()
    {   CharMove();
        Jump();
    }

    protected virtual void CharMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
           velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(playerSpeed * Time.deltaTime * move);
        if (!isWallRunning)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y -= (gravity/2) * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    protected virtual void Jump()
    {
        if (!isWallRunning)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            }
        }
        
    }
}
