using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody rb;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Collider bodyCollider;
    [SerializeField] private Collider visionCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private Climb climb;
    [FormerlySerializedAs("fight")] [SerializeField] private RemoveFight removeFight;
    [SerializeField] private Settings settings;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject body;

    [Header("Settings")][SerializeField] private bool facingRight = true;

    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float jumpForce;

    [Range(0, .3f)][SerializeField] private float movementSmoothing;

    [SerializeField] private bool airControl = true;
    [SerializeField] private float horizontalMove;
    [SerializeField] private bool jump;
    [SerializeField] private bool crouch;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private float maxJumpTimer = 0.3f;
    [SerializeField] private float currentJumpTimer;

    [Range(1, 2f)][SerializeField] private bool canStandUp = true;

    [SerializeField] private bool WReleased;
    [SerializeField] private bool SReleased;
    [SerializeField] private UnityEvent flip;
    [SerializeField] private float deltaSpeed;
    [SerializeField] private float deltaSpeedMult;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float speedMod;
    [SerializeField] private float airSpeed;
    [SerializeField] private float intFlip = 1;
    [SerializeField] private bool left;

    [SerializeField] private Vector3 smallColl = new Vector3(0.6f, 0.6f, 0.6f);
    [SerializeField] private Vector3 normColl = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 deltCol = new Vector3(0f, 0.4f, 0f);
    [SerializeField] private Vector3 nullCol = new Vector3(0f, -0.949f, 0f);
    
    public bool Crouch
    {
        get => crouch;
        set => crouch = value;
    }

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    public float SpeedMod
    {
        get => speedMod;
        set => speedMod = value;
    }

    public bool AirControl
    {
        get => airControl;
        set => airControl = value;
    }
    
    public float IntFlip { get => intFlip; set => intFlip = value; }
    public Vector3 SmallColl { get => smallColl; set => smallColl = value; }
    public Vector3 NormColl { get => normColl; set => normColl = value; }
    public Vector3 DeltCol { get => deltCol; set => deltCol = value; }
    public Vector3 NullCol { get => nullCol; set => nullCol = value; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions["Attack"].IsPressed() && playerInput.actions["Move"].ReadValue<Vector2>().x == 0 && !animator.GetBool("isPunching"))
            if (!Crouch)
                removeFight.Strike();

        //horizontalMove = Input.GetAxisRaw("Horizontal") * SpeedMod;
        horizontalMove = playerInput.actions["Move"].ReadValue<Vector2>().x * SpeedMod;

        animator.SetFloat("hSpeed", playerInput.actions["Move"].ReadValue<Vector2>().x);
        animator.SetFloat("vSpeed", Math.Abs(GetComponent<Rigidbody>().velocity.y));

        if (Math.Abs(playerInput.actions["Move"].ReadValue<Vector2>().x) > 0)
        {
            if (deltaSpeed < currentSpeed * 10) deltaSpeed += Time.deltaTime * deltaSpeedMult;
        }
        else
        {
            if (deltaSpeed > Time.deltaTime * deltaSpeedMult)
                deltaSpeed -= Time.deltaTime * deltaSpeedMult;
            else
                deltaSpeed = 0;
        }

        animator.SetFloat("hSpeed", deltaSpeed);
        
        if (playerInput.actions["Move"].ReadValue<Vector2>().y > 0.5f)
        {
            jump = true;
            WReleased = false;
        }

        if (playerInput.actions["Move"].ReadValue<Vector2>().y is < 0.1f and > -0.1f) WReleased = true;

        if (playerInput.actions["Move"].ReadValue<Vector2>().y < -0.5f)
        {
            StartCrouch();
            SReleased = false;
        }

        if (playerInput.actions["Move"].ReadValue<Vector2>().y  is < 0.1f and > -0.1f) SReleased = true;

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


        if (Crouch && SReleased && canStandUp) EndCrouch();
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, Crouch, jump);
        jump = false;
    }

    public void AirGroundCollision(GameObject go)
    {
        left = true;
        if ((go.transform.position.x - transform.position.x) < 0)
        {
            left = false;
        }
        if (!Crouch && playerInput.actions["Move"].ReadValue<Vector2>().x  * (go.transform.position.x - transform.position.x) > 0)
        {
            if (go.TryGetComponent(out ClimbData climbData) && climb.IsCanClimb(climbData.FirstPoint(left)))
            {
                SpeedMod = 0;
                AirControl = false;
                climb.StartClimb(climbData.Points(left));
            }
        }
    }

    public void AirWallCollision(GameObject go)
    {
        if (playerInput.actions["Move"].ReadValue<Vector2>().x  * (go.transform.position.x - transform.position.x) > 0)
        {
            currentSpeed = 0;
            SpeedMod = 0;
        }
        else
        {
            if (IsGrounded)
                SpeedMod = 1;
            else
                SpeedMod = airSpeed;

            if (Crouch)
                currentSpeed = crouchSpeed;
            else
                currentSpeed = runSpeed;
        }
    }

    public void AirWallUnCollision()
    {
        if (Crouch)
        {
            currentSpeed = crouchSpeed;
            if (IsGrounded)
                SpeedMod = 1;
            else
                SpeedMod = airSpeed;
        }
        else
        {
            currentSpeed = runSpeed;
            if (IsGrounded)
                SpeedMod = 1;
            else
                SpeedMod = airSpeed;
        }
    }

    public void ToGround()
    {
        IsGrounded = true;
        WReleased = false;
        animator.SetBool("isJumping", false);
        SpeedMod = 1;
        bodyCollider.transform.localPosition = nullCol;
        if (!Crouch)
        {
            bodyCollider.transform.localScale = normColl;
        }
        else
            StartCrouch();
    }

    public void ToAir()
    {
        IsGrounded = false;
        animator.SetBool("isJumping", true);
        SpeedMod = airSpeed;
        bodyCollider.transform.localPosition += deltCol;
        bodyCollider.transform.localScale = smallColl;
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
        if ((IsGrounded || AirControl) && climb.StartedClimbing == false)
        {
            // Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            rb.velocity = new Vector2(move * animator.GetFloat("hSpeed"), rb.velocity.y);

            if (move > 0 && !facingRight)
                Flip();

            else if (move < 0 && facingRight) Flip();
            if (move > 0) animator.SetBool("isPunching", false);
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
        bodyCollider.transform.localScale = smallColl;
        visionCollider.transform.localScale = smallColl;
        //body.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public void EndCrouch()
    {
        Crouch = false;
        currentSpeed = runSpeed;
        animator.SetBool("isCrouch", false);
        bodyCollider.transform.localScale = normColl;
        visionCollider.transform.localScale = normColl;
        //body.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Flip()
    {
        flip?.Invoke();
        facingRight = !facingRight;
        IntFlip *= -1;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;

        body.transform.Rotate(0, 180, 0);
    }


    private IEnumerator JumpTimer()
    {
        currentJumpTimer = 0f;
        while (currentJumpTimer < maxJumpTimer)
        {
            if (WReleased) break;
            currentJumpTimer += 0.1f;
            rb.AddForce(new Vector2(0f, jumpForce * (maxJumpTimer - currentJumpTimer)), ForceMode.Force);
            yield return new WaitForSeconds(.1f);
        }

        yield return null;
    }
}