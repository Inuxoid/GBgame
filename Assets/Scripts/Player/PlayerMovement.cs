using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Collider bodyCollider;
	[SerializeField] private Collider visionCollider;
	[SerializeField] private Animator animator;
	[SerializeField] private Climb climb;
	[SerializeField] private Fight fight;
	[SerializeField] private Settings settings;
	[SerializeField] private GameObject pausePanel;

	[Header("Settings")]
	[SerializeField] private bool facingRight = true;
	[SerializeField] private float runSpeed;
	[SerializeField] private float crouchSpeed;
	[SerializeField] private float currentSpeed;
	[SerializeField] private float jumpForce;
	[Range(0, .3f)]
	[SerializeField] private float movementSmoothing;
	[SerializeField] private bool airControl = true;
	[SerializeField] private float horizontalMove;
	[SerializeField] private bool jump = false;
	[SerializeField] private bool crouch = false;
	[SerializeField] private bool isGrounded;
	[SerializeField] private bool canDoubleJump;
	[SerializeField] private float maxJumpTimer = 0.3f;
	[SerializeField] private float currentJumpTimer = 0f;
	[Range(1, 2f)]
	[SerializeField] private bool canStandUp = true;
	[SerializeField] private bool WReleased;
	[SerializeField] private bool SReleased;
	[SerializeField] private UnityEvent flip;
	[SerializeField] private float deltaSpeed;
	[SerializeField] private float deltaSpeedMult;
	[SerializeField] private Vector3 velocity = Vector3.zero;
	[SerializeField] private float speedMod;
	[SerializeField] private float airSpeed;

	public bool Crouch { get => crouch; set => crouch = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }

    public void AirGroundCollision(GameObject go)
	{
		if (!Crouch && Input.GetAxisRaw("Horizontal") * (go.transform.position.x - this.transform.position.x) > 0)
		{
			if (go.GetComponent<ClimbData>() != null)
            {
				climb.Point1 = go.GetComponent<ClimbData>().Point1;
				climb.Point2 = go.GetComponent<ClimbData>().Point2;
				animator.SetBool("isClimbing", true);
            }
            else
            {
				Debug.Log("Не туда лезешь");
            }
			//this.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 2f, this.transform.position.z);
			//Debug.Log($"Target - {go.transform.position.x} Player - {transform.position.x}");
		}
	}

	public void AirWallCollision(GameObject go)
	{
		if (Input.GetAxisRaw("Horizontal") * (go.transform.position.x - this.transform.position.x) > 0)
		{
			currentSpeed = 0;
			speedMod = 0;
		}
        else
        {
            if (IsGrounded)
            {
				speedMod = 1;
            }
            else
            {
				speedMod = airSpeed;
			}
			
            if (Crouch)
            {
				currentSpeed = crouchSpeed;
            }
            else
            {
				currentSpeed = runSpeed;
			}
		}
	}

	public void AirWallUnCollision()
	{
		if (Crouch)
		{
			currentSpeed = crouchSpeed;
			if (IsGrounded)
			{
				speedMod = 1;
			}
			else
			{
				speedMod = airSpeed;
			}
		}
		else
		{
			currentSpeed = runSpeed;
			if (IsGrounded)
			{
				speedMod = 1;
			}
			else
			{
				speedMod = airSpeed;
			}
		}
	}

	public void ToGround()
	{
		IsGrounded = true;
		WReleased = false;
		animator.SetBool("isJumping", false);
		speedMod = 1;
        if (!Crouch)
        {
			bodyCollider.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
			StartCrouch();
		}

	}

	public void ToAir()
	{
		IsGrounded = false;
		animator.SetBool("isJumping", true);
		speedMod = airSpeed;
		bodyCollider.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	public void CanStandUp()
	{
		canStandUp = true;
	}

	public void CantStandUp()
	{
		canStandUp = false;
	}

	public void Move(float move, bool crouch, bool jump)
	{
		if (IsGrounded || airControl)
		{
			// Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			// rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			rb.velocity = new Vector2(move * animator.GetFloat("hSpeed"), rb.velocity.y);

			if (move > 0 && !facingRight)
			{
				Flip();
			}

			else if (move < 0 && facingRight)
			{
				Flip();
			}
            if (move > 0)
            {
				animator.SetBool("isPunching", false);
			}
		}

		if (IsGrounded && jump && !crouch)
		{
			StartCoroutine(JumpTimer());
			canDoubleJump = true;
		}
		//else if (canDoubleJump && jump && !crouch)
		//{
		//	canDoubleJump = false;
		//	StartCoroutine(JumpTimer());
		//}
		
	}

	public void StartCrouch()
	{
		Crouch = true;
		currentSpeed = crouchSpeed;
		animator.SetBool("isCrouch", true);
		deltaSpeed = 0;
		bodyCollider.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		visionCollider.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		//body.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
	}
		
	public void EndCrouch()
	{
		Crouch = false;
		currentSpeed = runSpeed;
		animator.SetBool("isCrouch", false);
		bodyCollider.transform.localScale = new Vector3(1f, 1f, 1f);
		visionCollider.transform.localScale = new Vector3(1f, 1f, 1f);
		//body.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	private void Flip()
	{
		this.flip?.Invoke();
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") == 0 && !animator.GetBool("isPunching"))
		{
			if (!Crouch)
			{
				fight.Strike();	
			}
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * speedMod;

		animator.SetFloat("hSpeed", Math.Abs(Input.GetAxisRaw("Horizontal")));
		animator.SetFloat("vSpeed", Math.Abs(GetComponent<Rigidbody>().velocity.y));	

        if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
			if (deltaSpeed < currentSpeed * 10)
			{
				deltaSpeed += Time.deltaTime * deltaSpeedMult;
			}
		}
        else
        {
            if (deltaSpeed > Time.deltaTime * deltaSpeedMult)
            {
				deltaSpeed -= Time.deltaTime * deltaSpeedMult;
            }
            else
            {
				deltaSpeed = 0;
			}
        }
		animator.SetFloat("hSpeed", deltaSpeed);

		if (Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
			WReleased = false;
		}

		if (Input.GetKeyUp(KeyCode.W))
		{
			WReleased = true;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			StartCrouch();
			SReleased = false;
		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			SReleased = true;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
            if (pausePanel.activeInHierarchy)
            {
				settings.Unpause();
				pausePanel.SetActive(false);
            }
            else
            {
				settings.Pause();
				pausePanel.SetActive(true);
			}
		}


		if (Crouch && SReleased && canStandUp)
		{
			EndCrouch();
		}

	}
	void FixedUpdate()
	{
		Move(horizontalMove * Time.fixedDeltaTime, Crouch, jump);
		jump = false;
	}

	IEnumerator JumpTimer()
	{
		currentJumpTimer = 0f;
		while (currentJumpTimer < maxJumpTimer)
		{
			if (WReleased)
			{
				break;
			}
			currentJumpTimer += 0.1f;
			rb.AddForce(new Vector2(0f, jumpForce * (maxJumpTimer - currentJumpTimer)), ForceMode.Force);
			yield return new WaitForSeconds(.1f);
		}
		yield return null;
	}
}
