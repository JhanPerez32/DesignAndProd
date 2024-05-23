using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithCrouchSlide : PlayerMovementWithWallRide
{
    [Header("Crouch Slide Parameters")]
    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    public float crouchHeight = 0.5f;
    public float originalHeight;
    private bool isSliding = false;
    private float slideTimer;
    private bool isCrouching = false;

    protected override void Start()
    {
        base.Start();
        originalHeight = controller.height;
    }

    protected override void Update()
    {
        base.Update();
        HandleCrouchSlide();
    }

    void HandleCrouchSlide()
    {
        if (Input.GetButtonDown("Crouch") && isGrounded && !isSliding && !isCrouching)
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0)
            {
                EndSlide();
            }
        }

        // Check for jump input to cancel the slide
        if (isSliding && Input.GetButtonDown("Jump"))
        {
            EndSlide();
            Jump();
        }
    }

    void StartSlide()
    {
        isSliding = true;
        isCrouching = true;
        slideTimer = slideDuration;
        StartCoroutine(SmoothCrouch(crouchHeight));
        // Maintain the current vertical velocity component to account for gravity
        Vector3 slideDirection = transform.forward * slideSpeed;
        velocity = new Vector3(slideDirection.x, velocity.y, slideDirection.z);
    }

    void EndSlide()
    {
        isSliding = false;
        StartCoroutine(SmoothCrouch(originalHeight));
    }

    private IEnumerator SmoothCrouch(float targetHeight)
    {
        float elapsedTime = 0f;
        float duration = 0.3f; // Duration of the crouch/stand transition
        float startHeight = controller.height;

        while (elapsedTime < duration)
        {
            controller.height = Mathf.Lerp(startHeight, targetHeight, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;

        if (targetHeight == originalHeight)
        {
            isCrouching = false;
        }
    }

    protected override void CharMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity/4;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!isSliding)
        {

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(playerSpeed * Time.deltaTime * move);

            if (!isWallRunning)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                velocity.y -= (gravity / 2) * Time.deltaTime;
            }
        }
        else
        {
            // Apply gravity while sliding
            velocity.y += gravity * Time.deltaTime;
            // Ensure the player maintains slide speed while sliding
            Vector3 slideDirection = transform.forward * slideSpeed;
            controller.Move(new Vector3(slideDirection.x, velocity.y, slideDirection.z) * Time.deltaTime);
        }
        if (Mathf.Abs(velocity.x) < 0.05f)
        {
            velocity.x = 0f;
        }
        if (Mathf.Abs(velocity.z) < 0.05f)
        {
            velocity.z = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    protected override void Jump()
    {
        if (!isWallRunning)
        {
            if (Input.GetButtonDown("Jump") && isGrounded || Input.GetButtonDown("Jump") && isSliding)
            {
                velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            }
        }
    }
}
