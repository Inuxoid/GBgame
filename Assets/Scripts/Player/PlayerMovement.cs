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
	[SerializeField] private float distance = 1.5f;
	[SerializeField] private bool canStandUp = true;
	[SerializeField] private bool WReleased;
	[SerializeField] private bool SReleased;
	[SerializeField] private UnityEvent flip;

	private Vector3 velocity = Vector3.zero;



	public void AirGroundCollision(GameObject go)
	{
		if (!crouch && Input.GetAxisRaw("Horizontal") * (go.transform.position.x - this.transform.position.x) > 0)
		{
			Vector3 delta = (go.transform.position - this.transform.position) / distance;
			this.transform.position = new Vector3(go.transform.position.x - delta.x, go.transform.position.y + 1.5f);
		}
	}

	public void AirWallCollision(GameObject go)
	{
		if (!isGrounded)
		{
			airControl = false;
		}
	}

	public void ToGround()
	{
		isGrounded = true;
		WReleased = false;
		airControl = true;
	}

	public void ToAir()
	{
		isGrounded = false;
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

		if (isGrounded && jump && !crouch)
		{
			StartCoroutine(JumpTimer());
			canDoubleJump = true;
		}
		else if (canDoubleJump && jump)
		{
			canDoubleJump = false;
			StartCoroutine(JumpTimer());
		}
	}

	public void StartCrouch()
	{
		crouch = true;
		currentSpeed = crouchSpeed;
		body.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		airCollider.enabled = false;
	}
		
	public void EndCrouch()
	{
		crouch = false;
		currentSpeed = runSpeed;
		body.transform.localScale = new Vector3(1f, 1f, 1f);
		airCollider.enabled = true;
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

		if (crouch && SReleased && canStandUp)
		{
			EndCrouch();
		}

	}
	void FixedUpdate()
	{
		Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
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
