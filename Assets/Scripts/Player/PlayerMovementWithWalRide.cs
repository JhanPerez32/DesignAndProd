using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWithWalRide : PlayerMove
{
    private float horizontalInput;
    private float verticalInput;
    public Animator CamAnim;
    private float wallRunSpeed;
    public LayerMask whatIsWall;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallRight;
    private bool wallLeft;
    public float wallCheckDistance;
    public float wallRunForce;
    public float minJumpHeight;

    protected override void Update()
    {
        base.Update();       
        StateMachine();
        Debug.DrawRay(transform.position, transform.right * wallCheckDistance, Color.red);
        Debug.DrawRay(transform.position, -transform.right * wallCheckDistance, Color.red);
    }
    

    private void FixedUpdate()
    {
        CheckForWall();
    }
    protected override void Start()
    {
        base.Start();
        
        
    }
   void CheckForWall() {
        wallRight = Physics.Raycast(transform.position,transform.right,out rightWallhit,wallCheckDistance, whatIsWall);
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
            WallRunningMovement();
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


    private void WallRunningMovement()
    {
        velocity = new Vector3(velocity.x, 0f, velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        //velocity.AddForce(wallForward * wallRunForce, ForceMode.Force);
    }
}
