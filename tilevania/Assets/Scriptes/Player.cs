using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float ClimbSpeed = 5f;
    [SerializeField] float RunSpeed = 5f;
    Rigidbody2D MyRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2d;
    BoxCollider2D myFeet;
    [SerializeField] float JumpSpeed = 5f;
    float GravityScaleAtStart;
    bool IsAlive = true;
    [SerializeField] Vector2 DeathKick = new Vector2(25f,25f);
     
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2d = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        GravityScaleAtStart = MyRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive) { return; }
        Run();
        ClimbLadder();
        FlipSprite();
        Jump();
        Die();
        
    }
    public void Run()
    {
        float ControlThrow = Input.GetAxis("Horizontal");
        Vector2 PlayerVelocity = new Vector2(ControlThrow * RunSpeed, MyRigidbody.velocity.y);
        MyRigidbody.velocity = PlayerVelocity;

        bool PlayerHorizontalSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("Running", PlayerHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            MyRigidbody.gravityScale = GravityScaleAtStart ;

            return;
        }

        float ControlThrow = Input.GetAxis("Vertical");
        Vector2 ClimbVelocity = new Vector2(MyRigidbody.velocity.x, ControlThrow * ClimbSpeed);
        MyRigidbody.velocity = ClimbVelocity;
       MyRigidbody.gravityScale = 0f;

        bool PlayerVerticalSpeed = Mathf.Abs(MyRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", PlayerVerticalSpeed);

    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("ground"))) { return; }

        if(Input.GetButtonDown("Jump"))
        {
            Vector2 JumpVelocityToAdd = new Vector2(0, JumpSpeed);
            MyRigidbody.velocity += JumpVelocityToAdd;
        }
    }
    private void FlipSprite()
    {
        bool PlayerHorizontalSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        if(PlayerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(MyRigidbody.velocity.x), 1f);
        }
    }
    private void Die()
    {
        if(myBodyCollider2d.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            IsAlive = false; 
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = DeathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
