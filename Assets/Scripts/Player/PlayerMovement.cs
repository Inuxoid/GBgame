using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Collider airCollider;
	[SerializeField] private GameObject body;
	[SerializeField] private Animator anim;
	[SerializeField] private GameObject head;
	[SerializeField] private GameObject vision;

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

	private Vector3 velocity = Vector3.zero;

    public bool Crouch { get => crouch; set => crouch = value; }

    public void AirGroundCollision(GameObject go)
	{
		if (!Crouch && Input.GetAxisRaw("Horizontal") * (go.transform.position.x - this.transform.position.x) > 0)
		{
			StartCoroutine(ClimbTimer(go));

			Debug.Log($"Target - {go.transform.position.x} Player - {transform.position.x}");
		}
	}

	public void AirWallCollision(GameObject go)
	{
		if (Input.GetAxisRaw("Horizontal") * (go.transform.position.x - this.transform.position.x) > 0)
		{
			currentSpeed = 0;
		}
        else
        {
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
		}
		else
		{
			currentSpeed = runSpeed;
		}
	}

	public void ToGround()
	{
		isGrounded = true;
		WReleased = false;
		anim.SetBool("isJumping", false);
	}

	public void ToAir()
	{
		isGrounded = false;
		anim.SetBool("isJumping", true);
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
		if (isGrounded || airControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (move > 0 && !facingRight)
			{
				Flip();
			}

			else if (move < 0 && facingRight)
			{
				Flip();
			}
		}
		
		if (isGrounded && Math.Abs(move) > 0)
        {
			anim.SetBool("isMooving", true);
        }
        else
        {
			anim.SetBool("isMooving", false);
		}

		if (isGrounded && jump && !crouch)
		{
			StartCoroutine(JumpTimer());
			canDoubleJump = true;
		}
		else if (canDoubleJump && jump && !crouch)
		{
			canDoubleJump = false;
			StartCoroutine(JumpTimer());
		}
	}

	public void StartCrouch()
	{
		Crouch = true;
		currentSpeed = crouchSpeed;
		//body.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		airCollider.transform.Rotate(90, 0, 0);
		airCollider.transform.position = new Vector3(airCollider.transform.position.x, airCollider.transform.position.y + 0.4f, airCollider.transform.position.z);
		head.transform.position = new Vector3(airCollider.transform.position.x, airCollider.transform.position.y + 0.4f, airCollider.transform.position.z);
		vision.transform.Rotate(90, 0, 0);
		anim.SetBool("isCrouch", true);
	}
		
	public void EndCrouch()
	{
		Crouch = false;
		currentSpeed = runSpeed;
		//body.transform.localScale = new Vector3(1f, 1f, 1f);
		airCollider.transform.Rotate(-90, 0, 0);
		airCollider.transform.position = new Vector3(airCollider.transform.position.x, airCollider.transform.position.y - 0.4f, airCollider.transform.position.z);
		head.transform.position = new Vector3(airCollider.transform.position.x, airCollider.transform.position.y - 0.4f, airCollider.transform.position.z);
		vision.transform.Rotate(-90, 0, 0);
		anim.SetBool("isCrouch", false);
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
		horizontalMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
		if (Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
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

	IEnumerator ClimbTimer(GameObject go)
	{
		airControl = false;
		anim.SetBool("IsClimbing", true);
		yield return new WaitForSeconds(.3f);
		//this.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 2f, this.transform.position.z);
		anim.SetBool("IsClimbing", false);
		GetComponent<Rigidbody>().AddForce(Vector3.up * 12);
		yield return new WaitForSeconds(.3f);
		airControl = true;
		yield return null;
	}


		IEnumerator JumpTimer()
	{
		currentJumpTimer = 0f;
		while (currentJumpTimer < maxJumpTimer)
		{
			if (WReleased)
			{
				Debug.Log("rel");
				break;
			}
			currentJumpTimer += 0.1f;
			rb.AddForce(new Vector2(0f, jumpForce * (maxJumpTimer - currentJumpTimer)), ForceMode.Force);
			yield return new WaitForSeconds(.1f);
		}
		yield return null;
	}
}
