using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    public Animator animator;

    public float horizontal;
    public float playerSpeed;
    public float walkingSpeed = 4f;
    public float runningSpeed = 4f;

    public bool isJumping = false;
    public float jumpingPower = 16f;
    public float cutJumpHeight;
    public bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform landCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize;


    public float jumpPressedRemember = 0f;
    public float jumpPressedRememberTime = 0.2f;

    public float GroundedRemember = 0;
    public float GroundedRememberTime = 0.2f;

    private bool doubleJump;

    public Joystick joystick;

    public bool isWalking = false;

    public float acceleration;
    public float decceleration;
    public float velPower;

    public bool once = true;
    public float timer;
    public bool isRunning;
    public bool isRunningOnce;

    public float sighTime;
    public float runtime;

    public ParticleSystem dust;
    public float frictionAmount;

    public float idleTimer;

    private void Start()
    {

        if (isWalking == true)
        {
            playerSpeed = walkingSpeed;
            animator.SetBool("IsWalking", true);
        }

    }

    private void Update()
    {


            if (isWalking)
            {
                playerSpeed = walkingSpeed;
            }
            else
            {
                playerSpeed = runningSpeed;
            }

        


        runtime -= Time.deltaTime;
        if (runtime < 0)
        {
            runtime = 1;
        }

        sighTime -= Time.deltaTime;
        idleTimer -= Time.deltaTime;

        if (isRunning == false && idleTimer > 100f)
        {
            idleTimer = UnityEngine.Random.Range(8, 12);
        }
        else if (isRunning == true || isJumping == true)
        {
            idleTimer = 200f;
        }

        if (idleTimer <= 0)
        {
            animator.SetTrigger("Idle" + UnityEngine.Random.Range(1, 3));
            idleTimer = UnityEngine.Random.Range(8, 12);
        }

        if (isRunning == false && sighTime > 100f)
        {
            sighTime = UnityEngine.Random.Range(15, 25);
        }
        else if (isRunning == true || isJumping == true)
        {
            sighTime = 200f;
        }  

        if (sighTime <= 0)
        {
            AudioManager.instance.PlayAudio("Player", "PlayerSigh");
            sighTime = UnityEngine.Random.Range(20, 25);
        }

        if (isRunning == true && isRunningOnce == false && isWalking == false)
        {
            AudioManager.instance.PlayAudio("PlayerRun", "PlayerRun");
            isRunningOnce = true;
        }
        else if (isRunning == false && isRunningOnce == true && isWalking == false)
        {
            AudioManager.instance.StopAudio("PlayerRun");//"PlayerRun"
            isRunningOnce = false;
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


        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;
        
        
        Jump();

    }


    public void JumpTest()
    {
        jumpPressedRemember = jumpPressedRememberTime;
    }

    private void FixedUpdate()
    {
        Run();
        IsRunning();
        IsFacingRight();
        Jumpflip();
        Flip();       
        timer += Time.deltaTime;

        if (IsLanding() == true)
        {
               
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
            }
        }

        if (GroundedRemember > 0 && Mathf.Abs(horizontal) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }


    }

    public void Jump()
    {

        if ((jumpPressedRemember > 0) && (GroundedRemember > 0))
        {
            int random = UnityEngine.Random.Range(0, 4);
            if (random == 0 && PauseMenu.instance.GameIsPaused == false)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerJump");
            }
            isJumping = true;
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && doubleJump)
        {
            int random = UnityEngine.Random.Range(0, 4);
            if (random == 0 && PauseMenu.instance.GameIsPaused == false)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerJump");
            }
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = false;
        }
    }

    public void Run()
    {
        float targetSpeed = horizontal * playerSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private bool IsGrounded()
    {
        
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsLanding()
    {
        animator.SetBool("IsLanding", true);
        
        return Physics2D.OverlapCircle(landCheck.position, 0.2f, groundLayer);
    }

    public void IsJumpingFalse()
    {
        AudioManager.instance.PlayAudio("Player", "PlayerLand");
        isJumping = false;
    }

    public void LandSound()
    {
        AudioManager.instance.PlayAudio("Player", "PlayerLand");
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
        if (doubleJump)
        {
            int random = UnityEngine.Random.Range(0, 4);
            if (random == 0)
            {
                AudioManager.instance.PlayAudio("Player", "PlayerJump");
            }
            animator.SetTrigger("IsJumping");
            GroundedRemember = 0f;
            jumpPressedRemember = 0f;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = false;
        }
        else
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }
        
    }


    public void IsRunning()
    {

        if (horizontal == 0 && isRunning == true || isJumping == true)
        {
            timer = 0f;
            isRunning = false;
        }
        if (horizontal != 0 && isRunning == false && isJumping == false)
        {
            timer = 0f;
            isRunning = true;
        }
        if (isRunning == true && timer >= 0.05f)
        {
            animator.SetBool("IsRunning", true);
        }
        if (isRunning == false && timer >= 0.05f)
        {
            animator.SetBool("IsRunning", false);
        }

    }

    public void PlayDust()
    {
        dust.Play();
    }

}