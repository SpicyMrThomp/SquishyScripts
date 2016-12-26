using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Animation anim;
    public Transform groundPoint;
    public Transform rightWallPoint;
    private Transform myTransform;
    public LayerMask groundMask;
    

    //Adjustables
    public float normalSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float wallPushForce;
    public float pointRadius;
    public float wallSlide;

    //Watch Values
    public Vector2 moveDirection = Vector2.zero;
    public float speed;
    public bool isSprinting;
    public bool isRight;
    public bool isLeft;
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isAttatchedToWall;
    public float gravityScale;
    public bool gravityAltered;
    public bool canJump;
    public bool onMovingPlatform;

    public float horizontalAxis;
    public float verticalAxis;
    public bool jumpDown;
    public bool jumpUp;
    public bool jumpHeld;
    public float sprintAxis;
    public bool sprintDown;
    public bool sprintUp;
    public bool sprintHeld;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        speed = normalSpeed;
        gravityScale = rb.gravityScale;
        gravityAltered = false;
        canJump = true;
        myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        DetectMovement();
        DetectJumping();
        UndoGravityChange();
    }

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        sprintAxis = Input.GetAxis("Sprint");

        if (Input.GetButtonDown("Jump"))
        {
            jumpDown = true;
            jumpUp = false;
            jumpHeld = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpDown = false;
            jumpUp = true;
            jumpHeld = false;
        }

        if(Input.GetButton("Jump"))
        {
            jumpDown = true;
            jumpUp = false;
            jumpHeld = true;
        }

        if (Input.GetButtonDown("Sprint"))
        {
            sprintDown = true;
            sprintUp = false;
            sprintHeld = false;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            sprintDown = false;
            sprintUp = true;
            sprintHeld = false;
        }

        if (Input.GetButton("Sprint"))
        {
            sprintDown = true;
            sprintUp = false;
            sprintHeld = true;
        }
    }

    void DetectMovement()
    {
        IsGrounded();
        IsTouchingWall();
        if (!isAttatchedToWall)
        {
            rb.velocity = new Vector2((horizontalAxis * speed), rb.velocity.y);
            moveDirection = rb.velocity;
        }
        
        if (sprintAxis == 1)
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        else if (sprintAxis == 0 || sprintUp)
        {
            speed = normalSpeed;
            isSprinting = false;
        }

        if (moveDirection.x > 0 && isRight == false)
        {
            FlipRight();
        }
        if (moveDirection.x < 0 && isLeft == false)
        {
            FlipLeft();
        }

        if (!isGrounded && isTouchingWall && rb.velocity.y <= 0)
        {
            isAttatchedToWall = true;
        }
        else
        {
            isAttatchedToWall = false;
        }

        if (isAttatchedToWall)
        {
            //Moves Player Slightly Down when moving at wall
            if (isSprinting)
            {
                moveDirection = new Vector2(0, -wallSlide);
            }
            else
            {
                moveDirection = new Vector2(0, ((-wallSlide) * 2.0f));
            }
            rb.velocity = moveDirection;
        }
    }

    void DetectJumping()
    {
        if (jumpUp && (isGrounded || isAttatchedToWall))
        {
            canJump = true;
        }

        if (jumpDown && isGrounded && canJump)
        {
            Jump();
        }

        if (jumpDown && isAttatchedToWall && !isGrounded && canJump)
        {
            WallJump();
        }

        if (jumpUp && rb.velocity.y > 0)
        {
            //rb.velocity = new Vector2((Input.GetAxis("Horizontal") * speed), rb.velocity.y * -.5f);
            rb.velocity = Vector3.zero;
        }

        // Slow Gravity during long jumps (for better horizontal movement)
        if (jumpHeld && rb.velocity.y < 0.0f)
        {
            rb.gravityScale = gravityScale * 3.0f;
            gravityAltered = true;
            Debug.Log("Gravity Altered");
        }

        if (jumpHeld && rb.velocity.y < 0.0f && isSprinting)
        {
            rb.gravityScale = gravityScale * 3.0f;
            gravityAltered = true;
            Debug.Log("Gravity Altered");
        }
    }

    void FlipRight()
    {
        transform.parent = null; //To prevent scaling while on a moving platform
        myTransform.localScale = new Vector3(1, myTransform.localScale.y * 1, 1);
        isLeft = false;
        isRight = true;
    }

    void FlipLeft()
    {
        transform.parent = null; //To prevent scaling while on a moving platform
        myTransform.localScale = new Vector3(-1, myTransform.localScale.y * 1, 1);
        isLeft = true;
        isRight = false;
    }

    void Jump()
    {
        if (isSprinting)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce + 2);
            canJump = false;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    void WallJump()
    {
        if (isRight && !(horizontalAxis >= 0))
        {
            FlipLeft();
            //rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2((horizontalAxis * wallPushForce), (verticalAxis * wallPushForce));
            canJump = false;

        }
        else if(isLeft && !(horizontalAxis <= 0))
        {
            FlipRight();
            //rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2((horizontalAxis * wallPushForce), (verticalAxis * wallPushForce));
            canJump = false;
        }
    }

    bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundPoint.position, pointRadius, groundMask))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }

    bool IsTouchingWall()
    {
        if (Physics2D.OverlapCircle(rightWallPoint.position, pointRadius, groundMask))
        {
            isTouchingWall = true;
            return true;
        }
        else
        {
            isTouchingWall = false;
            return false;
        }
    }

    void UndoGravityChange()
    {
        if (gravityAltered)
        {
            if (isGrounded || isAttatchedToWall || isTouchingWall)
            {
                rb.gravityScale = gravityScale;
                gravityAltered = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Checks for a moving platform, then sets the platform as the player transform's parent.
        if (col.tag == "MovingPlatform")
        {
            transform.parent = col.transform;
            onMovingPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "MovingPlatform")
        {
            //Checks for the lack of a moving platform, then removes the platform's transform as a parent.
            transform.parent = null;
            onMovingPlatform = false;
        }
    }

    //Draw Gizmos to help conceptualize collisions
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(rightWallPoint.position, pointRadius);
    }
}
