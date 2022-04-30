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
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject magic;
    [SerializeField] Transform magicSpawn;


    Vector2 moveInput;

    Rigidbody2D myRidgidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float fltGravityScaleAtStart;
    bool bolIsAlive = true;

    
    [SerializeField] AudioClip audioFire;

    

    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        fltGravityScaleAtStart = myRidgidBody.gravityScale;
    }


    void Update()
    {
        if (!bolIsAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!bolIsAlive) { return; }
        AudioSource.PlayClipAtPoint(audioFire, Camera.main.transform.position);
        Instantiate(magic, magicSpawn.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!bolIsAlive) { return; }
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
        if (!bolIsAlive) { return; }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}

        if(value.isPressed)
        {
            myRidgidBody.velocity += new Vector2 (0f, fltJumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myRidgidBody.gravityScale = fltGravityScaleAtStart;
            myAnimator.SetBool("bolIsClimbing", false);
            return; 
        }

        Vector2 climbVelocity = new Vector2(myRidgidBody.velocity.x , moveInput.y * fltClimbSpeed);
        myRidgidBody.velocity = climbVelocity;
        myRidgidBody.gravityScale = 0f;
        bool bolPlayerHasVerticalSpeed = Mathf.Abs(myRidgidBody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("bolIsClimbing", bolPlayerHasVerticalSpeed);
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enimies", "Hazards")))
        {
            bolIsAlive = false;

            // Debug.Log("Player Died");
           
            myAnimator.SetTrigger("Dying");
            
            myRidgidBody.velocity = deathKick;
          
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
