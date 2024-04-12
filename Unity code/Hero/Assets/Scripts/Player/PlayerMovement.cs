using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    public Animator animator;

    private float horizontal;
    public float speed = 8f;

    public bool isJumping = false;
    public float jumpingPower = 16f;
    public bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize;


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
    public float timer;
    public bool isRunning;
    public bool isRunningOnce;

    public float sighTime;
    public float runtime;

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
        if (Input.GetKey(KeyCode.V))
        {
            AudioManager.instance.PlaySFX("PlayerRun");
        }

        runtime -= Time.deltaTime;
        if (runtime < 0)
        {
            //AudioManager.instance.musicSounds[0].source.pitch = Random.Range(0.8f, 1.1f);
            //AudioManager.instance.musicSounds[0].source.volume = Random.Range(0.3f, 0.5f);
            runtime = 1;
        }

        sighTime -= Time.deltaTime;

        if (isRunning == false && sighTime > 100f)
        {
            sighTime = Random.Range(15, 25);
        }
        else if (isRunning == true || isJumping == true)
        {
            sighTime = 200f;
        }  

        if (sighTime <= 0)
        {
            AudioManager.instance.PlaySFX("PlayerSigh");
            sighTime = Random.Range(15, 25);
        }

        if (isRunning == true && isRunningOnce == false)
        {
            AudioManager.instance.PlaySFX("PlayerRun");
            isRunningOnce = true;
        }
        else if (isRunning == false && isRunningOnce == true)
        {
            AudioManager.instance.StopSFX("PlayerRun");
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
            //SaveData.instance.playerData.Jumps++;
        }

        jumpPressedRemember -= Time.deltaTime;
        Jump();




    }

    private void FixedUpdate()
    {
        Run();
        IsRunning();
        IsFacingRight();
        Jumpflip();
        Flip();       
        timer += Time.deltaTime;
    }

    public void Jump()
    {
        if ((jumpPressedRemember > 0) && (GroundedRemember > 0))
        {
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                AudioManager.instance.PlaySFX("PlayerJump");
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
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                AudioManager.instance.PlaySFX("PlayerJump");
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
        float targetSpeed = horizontal * speed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private bool IsGrounded()
    {
        animator.SetBool("IsLanding", true);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void IsJumpingFalse()
    {
        AudioManager.instance.PlaySFX("PlayerLand");
        isJumping = false;
    }

    public void LandSound()
    {
        AudioManager.instance.PlaySFX("PlayerLand");
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

}