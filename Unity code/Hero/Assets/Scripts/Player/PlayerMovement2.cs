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

    public float fHorizontalDamping;
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
            //isJumping = false;
            GroundedRemember = GroundedRememberTime;
        }
        else
        {
            //isJumping = true;
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
             //OnLandig();
        }
        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            isJumping = true;
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = false;
            //OnLandig();
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

        
         Flip();
    }

    private void FixedUpdate()
    {
        //float fHorizontalVelocity = rb.velocity.x;
        //fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        //fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        //rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //public void OnLandig()
    //{
    //    animator.SetBool("IsLanding",true);
    //}

    private bool IsGrounded()
    {
        animator.SetBool("IsLanding", true);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void IsJumpingFalse()
    {
        isJumping = false;
    }
    

    

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f) 
        {
            isFacingRight = !isFacingRight;

            if (isJumping == true)
            {
                Quaternion theScale = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                transform.localRotation = theScale;
            }
            else
            {
                animator.SetTrigger("Turn");
            }



        }
        else if (!isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;

            if (isJumping == true)
            {
                Quaternion theScale = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                transform.localRotation = theScale;
            }
            else
            {
                animator.SetTrigger("Turn");
            }

        }
    }

    public void LookAtTargetTrigger()
    {
        //if (isFacingRight)
        //{
        //    transform.position += Vector3.right * Time.deltaTime * turnSpeed;
        //}
        //else
        //{
        //    transform.position += Vector3.left * Time.deltaTime * turnSpeed;
        //}
        
        transform.Rotate(0f, 180f, 0f);
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