using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    CharacterController2D characterController;
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

	Vector3 velocity = Vector3.zero;
    Vector2 directionInput = Vector2.zero;
	bool isFacingRight = true;
    bool isGrounded = false;
    bool wasGrounded = false;
    bool isJumping = false;


    // Timers
    Timer coyoteTimer;


    [Header("Speed")]
    [SerializeField] float speed = 10f;
    [SerializeField] float walkAcceleration = 200f;
    [SerializeField] float walkDeceleration = 200f;
    [SerializeField] float airAcceleration = 20f;

    
    [Header("Gravity")]
    [SerializeField] float fallMultiplier = 2.5f;


    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] [Range(0,1)] float jumpFloatFeel = 0.5f;
    [SerializeField] float coyoteTime = .2f;

    
    [Header("KnockBack")]
    [SerializeField] Vector2 knockBackForce = new Vector2(5, 5);
    [SerializeField] float knockBackDeceleration = 2f;
    bool isKnockbacked = false;


    [Header("Particle Effect")]
    [SerializeField] ParticleSystem footstepsPS;
    [SerializeField] ParticleSystem dashPS;
    [SerializeField] GameObject jumpImpactPrefab;
    //[SerializeField] GameObject dashEffectPrefab;
    ParticleSystem.EmissionModule footstepsEmission;


    #region getters

    public bool IsGrounded { get { return isGrounded; } }
    public bool IsFacingRight { get { return isFacingRight; } }
    public bool IsJumping { get { return isJumping; } }
    public bool IsCoyoteTimerOn { get { return coyoteTimer.IsOn; } }
    public Vector3 Velocity { get { return velocity; } }
    public Vector2 DirectionInput { get { return directionInput; } }

    #endregion

    void Start()
    {
        characterController = this.GetComponent<CharacterController2D>();
        playerController = this.GetComponent<PlayerController>();
        spriteRenderer = playerController.GFX.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();

        footstepsEmission = footstepsPS.emission;

        coyoteTimer = new Timer(coyoteTime);
    }

    private void Awake()
    {
        
    }
    void Update()
	{
        isGrounded = characterController.isGrounded;

        if (isGrounded) {
            velocity.y = 0;
            isJumping = false;
            if ( !wasGrounded ) {
                //show jump impact
                Instantiate(jumpImpactPrefab, footstepsPS.transform.position, Quaternion.identity);
            } 
        } 
        else if( wasGrounded && !isJumping ) {
            StartCoroutine(CoyoteTime());
        }


        // Wall Slide

        if( !isKnockbacked ) 
        {
            float acceleration = isGrounded ? walkAcceleration : airAcceleration;
            float deceleration = isGrounded ? walkDeceleration : 0;

            if (directionInput.x != 0) {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * directionInput.x, acceleration * Time.deltaTime);
            } else {
                velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            }  
        }

		// if not dashing apply gravity before moving
        ApplyGravity();

        // move
		characterController.move(velocity * Time.deltaTime);

		if ( (directionInput.x > 0 && !isFacingRight) || (directionInput.x < 0 && isFacingRight) ) 
        {
            Flip();
		} 

        // show foosteps
        if(velocity.x != 0 && isGrounded) {
            footstepsEmission.rateOverTime = 15f;
        } else {
            footstepsEmission.rateOverTime = 0f;
        }

        // grab our isGrounded component
        wasGrounded = isGrounded;

		// grab our current _velocity to use as a base for all calculations
		velocity = characterController.velocity;
	}


    void ApplyGravity()
	{
        if( velocity.y < 0) {
		    velocity.y += Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        } 
        else {
		    velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
	}

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    #region inputs

    public void MoveInput(InputAction.CallbackContext context)
    {
        directionInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine( 
                playerController.InputBuffer(() => playerController.CanJump(), Jump) 
            );
        }
        else if (context.canceled && velocity.y > 0) {
            velocity.y *= jumpFloatFeel;
            characterController.move( velocity * Time.deltaTime );
        }
    }

    #endregion


    #region Jump

    void Jump()
	{
        velocity.y = Mathf.Sqrt( 2 * jumpForce * Mathf.Abs(Physics2D.gravity.y) );
        isJumping = true;
        characterController.move(velocity * Time.deltaTime);
	}

    public IEnumerator CoyoteTime () 
    {
        coyoteTimer.Start();
        while ( coyoteTimer.IsOn ) 
        {
            coyoteTimer.Decrease();            
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion



    #region knockBack

	public IEnumerator KnockBack(Vector3 direction, Vector2 force)
	{
		isKnockbacked = true;
		velocity = direction * force;
		characterController.move(velocity * Time.deltaTime);
		while (velocity.x > .1f || velocity.x < -.1f)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, knockBackDeceleration * Time.deltaTime);
			characterController.move(velocity * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
        isKnockbacked = false;
	}

	public IEnumerator KnockBack(Vector3 direction)
	{
		isKnockbacked = true;
		velocity = direction * knockBackForce;
		characterController.move(velocity * Time.deltaTime);
		while (velocity.x > .1f || velocity.x < -.1f)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, knockBackDeceleration * Time.deltaTime);
			characterController.move(velocity * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
        isKnockbacked = false;
	}

    public IEnumerator KnockBackLittle(Vector3 direction)
    {
        isKnockbacked = true;
        velocity = direction * new Vector2(4,4);
        characterController.move(velocity * Time.deltaTime);
        while (velocity.x > .1f || velocity.x < -.1f)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, knockBackDeceleration * Time.deltaTime);
            characterController.move(velocity * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isKnockbacked = false;
    }

    #endregion


}
