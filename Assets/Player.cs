using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] protected Animator healthAnim;
    private float xInput;
    [Header("Movement Details")]
    //Simple Movements
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump = true;

    //Dashing
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer tr;


    protected override void Awake()
    {
        tr.emitting = false;
        base.Awake();   
        healthAnim.SetFloat("health", currentHealth);
    }

    // Update is called once per frame
    protected override void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
        
    }
    protected override void TakeDamage()
    {
        base.TakeDamage();
        healthAnim.SetFloat("health", currentHealth);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.X))
            HandleDash();

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }

    private void HandleDash()
    {
        if (isDashing)
            return;

        if(canDash)
        {
            isDashing = true;
            canDash = false;

            tr.emitting = true;

            rb.linearVelocity = new Vector2(facingDir * dashSpeed, rb.linearVelocityY);

            anim.SetTrigger("dash");

            StartCoroutine(DashTime());

            

            StartCoroutine(DashCoolDown());
        }
    }

    private IEnumerator DashTime()
    {
        yield return new WaitForSeconds(dashTime);

        //Debug.Log("Dash Stoped");
        isDashing = false;
        tr.emitting = false;

    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        //Debug.Log("Cooldown ended");
    }


    public override void EnableMovements(bool enable)
    {
        base.EnableMovements(enable);
        canJump = enable;
    }
    protected override void HandleMovement()
    {
        if (isDashing)
            return;

        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
