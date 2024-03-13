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
    public bool isFacingRight = true;

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

    public bool isWalking = false;

    public float acceleration;
    public float decceleration;
    public float velPower;

    public bool once = true;

    private void Start()
    {
        GameManager.instance.transition.Play("LevelLoaderEnd");

        joystick = GameManager.instance.joystick;
        jumpButton = GameManager.instance.jumpButton;
        pauseButton = GameManager.instance.pauseButton;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);
        }

        jumpButton.onClick.AddListener(jumpButtonTaskOnClick);
    }

    private void Update()
    {
        


        if (Input.GetKeyDown(KeyCode.C))
        {
            isWalking = !isWalking;
            animator.SetBool("IsWalking", isWalking);
            if (isWalking)
            {
                speed = 4f;
            }
            else
            {
                speed = 8f;
            }
        }

        
        

        GroundedRemember -= Time.deltaTime;
        if (IsGrounded())
        {
            GroundedRemember = GroundedRememberTime;
        }
        else
        {
            animator.SetBool("IsLanding", false);
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
            SaveData.instance.playerData.Jumps++;
        }

        jumpPressedRemember -= Time.deltaTime;    
        if ((jumpPressedRemember > 0) && (GroundedRemember > 0))
        {
            
            isJumping = true;
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
             jumpPressedRemember = 0f;
             rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
             doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            isJumping = true;
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = false;
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

        IsFacingRight();
        Jumpflip();
        Flip();

    }

    private void FixedUpdate()
    {

        float targetSpeed = horizontal * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);


        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }



    private bool IsGrounded()
    {
        animator.SetBool("IsLanding", true);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void IsJumpingFalse()
    {
        isJumping = false;
    }


    private void Jumpflip()
    {
        if (isJumping && isFacingRight && horizontal < 0f)
        {
            once = false;
            isFacingRight = false;
            Quaternion theScale = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            transform.localRotation = theScale;
        }
        else if (isJumping && !isFacingRight && horizontal > 0f)
        {
            once = true;
            isFacingRight = true;
            Quaternion theScale = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            transform.localRotation = theScale;
        }
    }

    private void Flip()
    {

        if (!isJumping && isFacingRight && horizontal < 0f && once == true) 
        {
            once = false;
            animator.SetTrigger("Turn");
        }
        else if (!isJumping && !isFacingRight && horizontal > 0f && once == false) 
        {
            once = true;
            animator.SetTrigger("Turn");
        }
    }

    public void LookAtTargetTrigger()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void IsFacingRight()
    {
        Quaternion playerRotation = transform.rotation;

        if (playerRotation == Quaternion.Euler(0, 0, 0)) 
        {
            isFacingRight = true;
        }
        if (playerRotation == Quaternion.Euler(0, -180, 0))
        {
            isFacingRight = false;
        }
    }

    public void MobileJump()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }

    void jumpButtonTaskOnClick()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }

}