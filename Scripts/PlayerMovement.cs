using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    bool isAlive = true;
    //when you're pushing vertically or horizontally
    Vector2 moveInput;
    Rigidbody2D myRigidbody; 
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    SpriteRenderer mySpriteRenderer;

    //making a component for gravity
    float gravityScaleAtStart;
    
    
     void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();


        //equating to the player's rigidbody gravity 
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if(!isAlive){
            return;
            };
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

        void OnFire(InputValue value){
        if(!isAlive){
            return;
        }

        if(value.isPressed){
            //instantiating the bullet at the gun's position, in the direction the bullet it pointing
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

    void OnMove(InputValue value){
        if(!isAlive){
            return;
            };
        //storing the values we get as inputs and storing it into "moveInput"
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value){
        if(!isAlive){
            return;
            };
        //if we're not touching the ground, get out of the method. We don't need you to double jump
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        //if the input value's button is pressed
        if(value.isPressed){
            //adding the rigidbody's velocity to
            myRigidbody.velocity  += new Vector2(0f, jumpSpeed);
        }
    }


    void Run(){
        //implementing movement, while keeping y at the same value to stop gravity from screwing with it. 
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y); //moving left and right, but not up and down
        myRigidbody.velocity = playerVelocity;

        //a bool that is the absolute value of the player movement greater than 0
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //setting the animator to running when chara is running horizontally is marked true
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed); 
    }

    //flipping sprite to the other side as it turns to the left and right
    void FlipSprite(){
        //a bool that is the absolute value of the player movement greater than 0
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            //(is the value positive, or negative), yaxis)
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder(){
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myRigidbody.gravityScale = gravityScaleAtStart;
            //if you're not touching the ladder, don't have a climb anim
            myAnimator.SetBool("isClimbing", false);
            return;
        }

         //implementing movement for climbing along the y axis
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed); 
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        //if you're touching the ladder, have a climb anim
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            mySpriteRenderer.color = new Color(1, 0, 0, 1);
            //getting a method from "Game Session" script
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
