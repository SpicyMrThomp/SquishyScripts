using UnityEngine;
using System.Collections;

public class MoveHazard : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    public float pointRadius;
    public bool isGrounded;
    public bool isTouchingForeGround;
    public bool isTouchingWall;
    public bool isRight;
    public bool isLeft;

    public Transform groundPoint;
    public Transform rightGroundPoint;
    public Transform leftGroundPoint;
    public Transform activeGroundPoint;
    public Transform topRightWallPoint;
    public Transform topLeftWallPoint;
    public Transform bottomRightWallPoint;
    public Transform bottomLeftWallPoint;
    public LayerMask groundMask;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        activeGroundPoint = rightGroundPoint;
        isRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        IsGrounded();
        CheckDirection();
        IsTouchingForeGround();
        if (isGrounded)
        {
            if (isRight && (isTouchingForeGround || isTouchingWall))
            {
                //rb.velocity = new Vector2(speed, rb.velocity.y);
                rb.transform.Translate(Vector3.right * (speed * Time.deltaTime));
            }
            else if (isLeft && (isTouchingForeGround || isTouchingWall))
            {
                //rb.velocity = new Vector2(speed * -1f, rb.velocity.y);
                rb.transform.Translate(Vector3.left * (speed * Time.deltaTime));
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (!isTouchingForeGround)
        {
            if (isRight)
            {
                FlipLeft();
            }
            else if (isLeft)
            {
                FlipRight();
            }
        }
    }

    bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundPoint.position, pointRadius, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

    bool IsTouchingForeGround()
    {
        if (Physics2D.OverlapCircle(activeGroundPoint.position, pointRadius, groundMask))
        {
            isTouchingForeGround = true;
        }
        else
        {
            isTouchingForeGround = false;
        }
        return isTouchingForeGround;
    }

    bool IsTouchingWall()
    {
        if (Physics2D.OverlapCircle(topRightWallPoint.position, pointRadius, groundMask) || Physics2D.OverlapCircle(topLeftWallPoint.position, pointRadius, groundMask)
            || Physics2D.OverlapCircle(bottomRightWallPoint.position, pointRadius, groundMask) || Physics2D.OverlapCircle(bottomLeftWallPoint.position, pointRadius, groundMask))
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }
        return isTouchingWall;
    }

    void CheckDirection()
    {
        if (rb.velocity.x > 0f)
        {
            FlipRight();
        }
        else if (rb.velocity.x < 0f)
        {
            FlipLeft();
        }
    }

    void FlipRight()
    {
        isRight = true;
        isLeft = false;
        activeGroundPoint = rightGroundPoint;
    }

    void FlipLeft()
    {
        isLeft = true;
        isRight = false;
        activeGroundPoint = leftGroundPoint;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(rightGroundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(leftGroundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(topRightWallPoint.position, pointRadius);
        Gizmos.DrawWireSphere(topLeftWallPoint.position, pointRadius);
        Gizmos.DrawWireSphere(bottomRightWallPoint.position, pointRadius);
        Gizmos.DrawWireSphere(bottomLeftWallPoint.position, pointRadius);
    }
}
