using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarefulWalk : MonoBehaviour
{
    public LayerMask obstacles;
    public float walkingSpeed, jumpingForce;
    Rigidbody2D body;
    Transform trans;
    float width, height;
    bool isJumping = false;
    int xPosBeforeJump;
    float jumpStartTime;

    private void Start()
    {
        trans = this.transform;
        body = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void FixedUpdate()
    {
        Vector2 lineCastPos = trans.position + trans.right * width;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * jumpingForce / 3, obstacles);

        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * jumpingForce / 3, Color.blue);


        Vector2 rightVector2 = new Vector2(trans.right.x, trans.right.y);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos + rightVector2 * 0.1f, obstacles) ||
            Physics2D.Linecast(lineCastPos + Vector2.up * (height * 0.95f), (lineCastPos + Vector2.up * (height * 0.95f)) + rightVector2 * 0.1f, obstacles) ||
            Physics2D.Linecast(lineCastPos + Vector2.down * (height * 0.95f), (lineCastPos + Vector2.down * (height * 0.95f)) + rightVector2 * 0.1f, obstacles);

        Debug.DrawLine(lineCastPos, lineCastPos + rightVector2 * 0.1f);
        Debug.DrawLine(lineCastPos + Vector2.up * (height * 0.95f), (lineCastPos + Vector2.up * (height * 0.95f)) + rightVector2 * 0.1f);
        Debug.DrawLine(lineCastPos + Vector2.down * (height * 0.95f), (lineCastPos + Vector2.down * (height * 0.95f)) + rightVector2 * 0.1f);

        if (isGrounded && isJumping && Time.time - jumpStartTime > 1)
        {
            isJumping = false;
            if ((int)trans.position.x == xPosBeforeJump)
            {
                Debug.Log("not successfully jumped..");
                TurnAround();
            }
        }
        else if (!isGrounded && !isJumping)
        {
            TurnAround();
        }
        else if (isBlocked && !isJumping)
        {
            Debug.DrawLine(lineCastPos + new Vector2(trans.right.x * jumpingForce / 2, 0), lineCastPos + new Vector2(trans.right.x * jumpingForce / 2, 0) + Vector2.down, Color.red);
            if (Physics2D.Linecast(lineCastPos + new Vector2(trans.right.x * jumpingForce / 2, 0), lineCastPos + new Vector2(trans.right.x * jumpingForce / 2, 0) + Vector2.down, obstacles))
            {
                xPosBeforeJump = (int)trans.position.x;
                jumpStartTime = Time.time;
                body.AddForce(new Vector2(0, jumpingForce), ForceMode2D.Impulse);
                isJumping = true;
            } else
            {
                Debug.Log("would be unsafe to jump!");
                TurnAround();
            }
        }

        Vector2 vel = body.velocity;
        vel.x = trans.right.x * walkingSpeed;
        body.velocity = vel;
    }

    private void TurnAround()
    {
        Vector3 rotation = trans.eulerAngles;
        rotation.y += 180;
        trans.eulerAngles = rotation;
    }
}
