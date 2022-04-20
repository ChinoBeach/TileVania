using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieMovement : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed = 1f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        //cant move up and down but moves the player side to side
        

    }

    void Run()
    {
        myRigidBody.velocity = new Vector2(fltMoveSpeed, 0f);
        bool bolEnemyHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("bolIsWalking", bolEnemyHasHorizontalSpeed);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        fltMoveSpeed = -fltMoveSpeed;
        FlipEnemyFacing();
        
    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
