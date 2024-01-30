using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    public Animator animator;

    private float horizontal;
    public float speed = 8f;
    public bool isJumping = false;
    public float jumpingPower = 16f;
    public float CrouchSpeed = 0.4f;
    public bool WasCrourch = false;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    public bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform CeilingCheck;
    const float CeilingRadius = .2f;
    [SerializeField] private Collider2D CrouchDisableCollider;
    public bool crouch = false;

    public float jumpPressedRemember = 0f;
    public float jumpPressedRememberTime = 0.2f;

    public float GroundedRemember = 0;
    public float GroundedRememberTime = 0.2f;

    private bool doubleJump;

    public Joystick joystick;
    public Button jumpButton;
    public Button pauseButton;

    private void Start()
    {
        joystick = GameManager.instance.joystick;
        jumpButton = GameManager.instance.jumpButton;
        pauseButton = GameManager.instance.pauseButton;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (IsWalled() || isWallJumping)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
        }
        

        GroundedRemember -= Time.deltaTime;
        if (IsGrounded())
        {
            GroundedRemember = GroundedRememberTime;
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            horizontal = joystick.Horizontal;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        

        

        animator.SetFloat("Speed", Mathf.Abs(horizontal));


        if (Input.GetButtonDown("Jump") && !WasCrourch) 
        {
            jumpPressedRemember = jumpPressedRememberTime;
            
        }

        jumpPressedRemember -= Time.deltaTime;    
        if ((jumpPressedRemember > 0) && (GroundedRemember > 0))
        {
             animator.SetBool("IsJumping", true);
             GroundedRemember = 0f;
             jumpPressedRemember = 0f;
             rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
             doubleJump = true;
             OnLandig();
        }
        else if (Input.GetButtonDown("Jump") && doubleJump && !IsWalled() && !isWallJumping)
        {
            animator.SetBool("IsJumping", true);
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = false;
            OnLandig();
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, groundLayer))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }



        if (Input.GetKeyDown(KeyCode.S) && WasCrourch == false)
        {
            animator.SetBool("IsCrouching", true);
            CrouchDisableCollider.enabled = false;
            speed *= CrouchSpeed;
            WasCrourch = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && crouch == false && WasCrourch == true)
        {
            animator.SetBool("IsCrouching", false);
            CrouchDisableCollider.enabled = true;
            speed /= CrouchSpeed;
            WasCrourch = false;
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    public void OnLandig()
    {
        animator.SetBool("IsJumping", isJumping);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f) //|| !isFacingRight && horizontal > 0f
        {
            isFacingRight = !isFacingRight;
            Quaternion theScale = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            transform.localRotation = theScale;

            
            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
        }
        else if (!isFacingRight && horizontal > 0f) //||! isFacingRight && horizontal < 0f
        {
            isFacingRight = !isFacingRight;
            Quaternion theScale = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            transform.localRotation = theScale;
        }
    }

    public void MobileJump()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }
}