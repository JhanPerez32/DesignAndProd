using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithWallRide : PlayerMove
{
    [Header("Wall Running Parameters")]
    public float wallCheckDistance;
    public LayerMask whatIsWall;
    public float wallRunForce;
    public float minJumpHeight;
    public float wallJumpForce;
    public float dampingFactor = 5f;
    public float wallJumpUpwardForce = 5f;

    private float horizontalInput;
    private float verticalInput;
    public Animator CamAnim;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallRight;
    private bool wallLeft;
    private bool isWallJumping;

    protected override void Update()
    {
        base.Update();
        StateMachine();
        Debug.DrawRay(transform.position, transform.right * wallCheckDistance, Color.red);
        Debug.DrawRay(transform.position, -transform.right * wallCheckDistance, Color.red);

        if (isWallJumping && velocity.y < 0 && !wallLeft && !wallRight)
        {
            isWallJumping = false; // Unset wall jumping flag
        }
    }

    protected virtual void FixedUpdate()
    {
        CheckForWall();
        ApplyDamping();
    }

    protected override void Start()
    {
        base.Start();
    }

    void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundMask);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                WallJump();
            }
            else
            {
                WallRunningMovement();
            }
            isWallRunning = true;
            if (wallLeft && !wallRight)
            {
                CamAnim.SetBool("WallrideR", true);
            }
            else
            {
                CamAnim.SetBool("WallrideL", true);
            }
        }
        else
        {
            CamAnim.SetBool("WallrideL", false);
            CamAnim.SetBool("WallrideR", false);
            isWallRunning = false;
        }
    }

    private void WallJump()
    {
        // Calculate jump direction based on wall normal and player orientation
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 jumpDirection = (wallNormal + Vector3.up).normalized;

        // Apply jump force in the calculated direction
        velocity = jumpDirection * wallJumpForce;

        // Apply additional force forward based on player's orientation
        velocity += transform.forward * wallJumpForce * 0.5f;

        // Add upward force
        velocity.y = wallJumpUpwardForce;

        // Ensure the player jumps off the wall
        //controller.Move(velocity * Time.deltaTime);
        isWallJumping = true;
    }

    private void WallRunningMovement()
    {
        if (!isWallJumping)
        {
            velocity.y = 0;

            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

            // Calculate the direction along the wall that is forward relative to the player
            Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up).normalized;
            if (Vector3.Dot(wallForward, transform.forward) < 0)
            {
                wallForward = -wallForward;
            }

            // Add force for wall running
            controller.Move(wallForward * wallRunForce * Time.deltaTime);
        }
    }

    private void ApplyDamping()
    {
        if (!isWallRunning)
        {
            // Apply damping to gradually reduce the x and z components of the velocity
            velocity.x = Mathf.Lerp(velocity.x, 0, dampingFactor * Time.deltaTime);
            velocity.z = Mathf.Lerp(velocity.z, 0, dampingFactor * Time.deltaTime);
        }
    }
}
