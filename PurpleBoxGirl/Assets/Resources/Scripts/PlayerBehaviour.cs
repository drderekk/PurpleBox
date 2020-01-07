using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBehaviour : MonoBehaviour
{
    public float Speed;
    public float FallIncrease;
    public float DefaultGravity;
    public float MaxFallSpeed;
    public float JumpSpeed;

    private AudioController Audio;
    Rigidbody2D rb;
    public float PlayerSize;

    public bool OnRightWall;
    public bool OnLeftWall;

    public bool GroundTouch;
    public bool LeftTouch;
    public bool RightTouch;
    public bool LeftNear;
    public bool RightNear;
    public LayerMask FloorMask;

    // Player Sprite stuff
	public SpriteRenderer Sprite;
    public Sprite LeftIdle;
    public Sprite LeftMove;
    public Sprite LeftAirUp;
    public Sprite LeftAirDown;
    public Sprite LeftWallSlide;
    public Sprite RightIdle;
    public Sprite RightMove;
    public Sprite RightAirUp;
    public Sprite RightAirDown;
    public Sprite RightWallSlide;
	public Sprite LeftIdleSquish;
	public Sprite RightIdleSquish;
    public bool LastLookedLeft;

	private int wallStickDelay = 5;
	private int currentWallStickTimer = 0;

	private bool wallWasLeft;
	private int wallJumpDelay = 20;
	private int wallJumpTimer = 0;

	private bool getButtonDownJump;
	private bool getButtonJump;

	float deltaTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        Sprite.sprite = RightIdle;
        PlayerSize = GetComponent<CircleCollider2D>().radius;
        Audio = FindObjectOfType<AudioController>();
    }


    void Update()
    {
		//printFPS();

		float xInput;

        // Android
        #if UNITY_ANDROID
			getButtonDownJump = CrossPlatformInputManager.GetButtonDown("Jump");
			getButtonJump = CrossPlatformInputManager.GetButton("Jump");
			xInput = getMobileXInput();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        #endif

        // Windows/Linux/Mac
        #if UNITY_STANDALONE
			getButtonDownJump = Input.GetButtonDown("Jump");
			getButtonJump = Input.GetButton("Jump");
			xInput = Input.GetAxisRaw("Horizontal");
        #endif

        /*
		 *  Sets the xInput to 0 if the wallJumpTimer is greater than zero
		 *  (Since the player just jumped from a wall) and the player is
		 *  trying to move towards the wall they just jumped from.
		 */
        if (wallJumpTimer > 0) {
			wallJumpTimer--;

			if ((wallWasLeft && xInput < -0.5f && getButtonJump && LeftNear) || (!wallWasLeft && xInput > 0.5f && getButtonJump && RightNear)) {
				xInput = 0f;
			}
		}
        
		DetectCollisions();
		MovePlayerAlongXAxis(xInput);

		if (getButtonDownJump && GroundTouch)
        {
			JumpPlayer();
        }

		DetectIfOnWall(xInput);

		if (OnLeftWall || OnRightWall) {
			setWallClingBehaviour(xInput);
		}

		SetPlayerGravityAndMaxFallSpeed();
    }

	/*
	 * Everything in here deals with the player sprite being rendered
	 */
    private void FixedUpdate()
    {
		float xInput;

		// Android
		#if UNITY_ANDROID
			xInput = getMobileXInput();
		#endif

		// Windows/Linux/Mac
		#if UNITY_STANDALONE
			xInput = Input.GetAxisRaw("Horizontal");
		#endif

		if (xInput > 0.5f)
        {
            LastLookedLeft = false;
        }
		if (xInput < -0.5f)
        {
            LastLookedLeft = true;
        }

		if (xInput > 0.5 && !RightTouch)
        {
            Sprite.sprite = RightMove;
        }
		else if (xInput < -0.5 && !LeftTouch)
        {
            Sprite.sprite = LeftMove;
        }
        else if (LastLookedLeft)
        {
            Sprite.sprite = LeftIdle;

			if (Input.GetAxisRaw ("Vertical") < -0.5f) {
				Sprite.sprite = LeftIdleSquish;
			}
        }
        else if (!LastLookedLeft)
        {
            Sprite.sprite = RightIdle;

			if (Input.GetAxisRaw ("Vertical") < -0.5f) {
				Sprite.sprite = RightIdleSquish;
			}
        }

        if (!GroundTouch && rb.velocity.y > 0 && LastLookedLeft)
        {
            Sprite.sprite = LeftAirUp;
        }
        else if (!GroundTouch && rb.velocity.y > 0 && !LastLookedLeft)
        {
            Sprite.sprite = RightAirUp;
        }
        else if (!GroundTouch && rb.velocity.y < 0 && LastLookedLeft)
        {
            Sprite.sprite = LeftAirDown;
        }
        else if (!GroundTouch && rb.velocity.y < 0 && !LastLookedLeft)
        {
            Sprite.sprite = RightAirDown;
        }
        if (OnRightWall && !GroundTouch)
        {
            Sprite.sprite = RightWallSlide;
        }
        else if (OnLeftWall && !GroundTouch)
        {
            Sprite.sprite = LeftWallSlide;
        }
    }

	/*
	 *  Detects what the player is currently touching, or if a wall is near it (for jumping)
	 */
	private void DetectCollisions(){
		GroundTouch = (Physics2D.OverlapBox((Vector2)transform.position + new Vector2(PlayerSize, 0.05f), new Vector2(PlayerSize * 1.6f, 0.1f), 0.0f, FloorMask) != null);
		LeftTouch = (Physics2D.OverlapBox((Vector2)transform.position + new Vector2(-0.005f, PlayerSize), new Vector2(0.01f, PlayerSize), 0.0f, FloorMask) != null);
		RightTouch = (Physics2D.OverlapBox((Vector2)transform.position + new Vector2((PlayerSize * 2) + 0.005f, PlayerSize), new Vector2(0.01f, PlayerSize), 0.0f, FloorMask) != null);
        LeftNear = (Physics2D.OverlapBox((Vector2) transform.position + new Vector2(-0.005f, PlayerSize), new Vector2(1.5f, PlayerSize), 0.0f, FloorMask) != null);
		RightNear = (Physics2D.OverlapBox((Vector2) transform.position + new Vector2((PlayerSize* 2) + 0.005f, PlayerSize), new Vector2(1.5f, PlayerSize), 0.0f, FloorMask) != null);
	}

/*
 *  Moves the player horizontally based on the given horizontal input
 */
private void MovePlayerAlongXAxis(float xInput){
		
		if (xInput > 0.5f || xInput < -0.5f)
		{
			rb.velocity = new Vector2(xInput * Speed, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0.0f, rb.velocity.y);
		}

	}

	/*
	 * Makes the player jump and plays the jump sound
	 */
	private void JumpPlayer(){
		rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
		Audio.PlayJumpSound();
	}

	/*
	 *  Sets which wall the player is currently on based on what it's touching and the
	 *  given horizontal input.
	 */
	private void DetectIfOnWall(float xInput){		
		if (!LeftTouch) {
			SetOnLeftWallWithDelay(false);
		} 

		else if (!GroundTouch && xInput < -0.5f) {
			currentWallStickTimer = wallStickDelay;
			OnLeftWall = true;
		}

		if (!RightTouch) {
			SetOnRightWallWithDelay(false);
		}
		else if (!GroundTouch && xInput > 0.5f)
		{
			currentWallStickTimer = wallStickDelay;
			OnRightWall = true;
		}

	}

	/*
	 *  Sets the player's wall cling behaviour based on the wall it's currently on.
	 */
	private void setWallClingBehaviour(float xInput){
		rb.gravityScale = 0.0f;
		rb.velocity = new Vector2(0.0f, -0.8f);

		if (getButtonDownJump)
		{
			if (OnLeftWall) {
				wallWasLeft = true;
				rb.velocity = new Vector2 (JumpSpeed, JumpSpeed);
			} 
			else if (OnRightWall) {
				wallWasLeft = false;
				rb.velocity = new Vector2 (-JumpSpeed, JumpSpeed);
			}

			wallJumpTimer = wallJumpDelay;

			rb.gravityScale = DefaultGravity;
			Audio.PlayJumpSound();

			OnLeftWall = false;
			OnRightWall = false;
		}
		else if (OnLeftWall && xInput > 0.5f || OnRightWall && xInput < -0.5f)
		{
			rb.gravityScale = DefaultGravity;

			SetOnLeftWallWithDelay(false);
			SetOnRightWallWithDelay(false);
		}
	}

	/*
	 * Sets the boolean value of OnLeftWall, preventing it from
	 * being set to false until the currentWallStickTimer is equal
	 * to or less than zero.
	 */
	private void SetOnLeftWallWithDelay(bool newValue){
		
		if (!newValue && OnLeftWall && currentWallStickTimer > 0) {
			currentWallStickTimer--;
			OnLeftWall = true;
		} else {
			OnLeftWall = newValue;
		}
	}

	/*
	 * Sets the boolean value of OnRightWall, preventing it from
	 * being set to false until the currentWallStickTimer is equal
	 * to or less than zero.
	 */
	private void SetOnRightWallWithDelay(bool newValue){

		if (!newValue && OnRightWall && currentWallStickTimer > 0) {
			currentWallStickTimer--;
			OnRightWall = true;
		} else {
			OnRightWall = newValue;
		}
	}

	/*
	 * Sets the player's gravity depending on certain conditions and limits
	 * the player's Y velocity so that it doesn't exceed the Max fall speed.
	 */
	private void SetPlayerGravityAndMaxFallSpeed(){
		// y axis fall increase
		if (!GroundTouch && rb.velocity.y < 0 || !GroundTouch && (rb.velocity.y > 0 && !getButtonJump))
		{
			rb.gravityScale = FallIncrease;
		}
		else if (!OnLeftWall && !OnRightWall)
		{
			rb.gravityScale = DefaultGravity;
		}

		// limits player y speed
		if (rb.velocity.y < MaxFallSpeed)
		{
			rb.velocity = new Vector2(rb.velocity.x, MaxFallSpeed);
		}
	}

	/*
	 * Outputs the current FPS to the console every 1 seconds.
	 */
	public void printFPS(){
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

		if(Time.fixedTime % 1 == 0){
			Debug.Log ("FPS: "+1.0f / deltaTime);
		}

	}

	public float getMobileXInput(){
		return CrossPlatformInputManager.GetAxis("Horizontal");
	}
}