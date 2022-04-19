using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //variables 
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    [SerializeField] float fltClimbSpeed = 5f;


    Vector2 moveInput;

    Rigidbody2D myRidgidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    float fltGravityScaleAtStart;
    

    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        fltGravityScaleAtStart = myRidgidBody.gravityScale;
    }


    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * fltRunSpeed, myRidgidBody.velocity.y);
        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        myRidgidBody.velocity = playerVelocity;

        myAnimator.SetBool("bolIsRunning", bolPlayerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool bolPlayerHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        if (bolPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidBody.velocity.x), 1f);
        }
        
    }
    void OnJump(InputValue value)
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}

        if(value.isPressed)
        {
            myRidgidBody.velocity += new Vector2 (0f, fltJumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myRidgidBody.gravityScale = fltGravityScaleAtStart;
            return; 
        }

        Vector2 climbVelocity = new Vector2(myRidgidBody.velocity.x , moveInput.y * fltClimbSpeed);
        myRidgidBody.velocity = climbVelocity;
        myRidgidBody.gravityScale = 0f;
    }
}
