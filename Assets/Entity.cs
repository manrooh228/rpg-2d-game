using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageFeedbackMaterial;
    [SerializeField] private float damageFeedbackDuration = 0.2f;
    private Coroutine damageFeedbackCoroutine;
    
    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;

    [Header("Movement Details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] protected int facingDir = 1;
    
    private float xInput;
    protected bool facingRight = true;
    [SerializeField] protected bool canMove = true;
    [SerializeField] private bool canJump;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement(xInput);
        HandleAnimations();
        HandleFlip();
    }
    
    public void DamageTargets()
    {
        Collider2D[] entityColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider2D entity in entityColliders)
        {
            Entity entityTarget = entity.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        PlayDamageFeedback();

        if (currentHealth < 0)
            Die();
    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
            StopCoroutine(damageFeedbackCoroutine);

        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMaterial = sr.material;

        sr.material = damageFeedbackMaterial;

        yield return new WaitForSeconds(damageFeedbackDuration);

        sr.material = originalMaterial;
    }

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        
        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 15);
    }

    public void EnableJumpAndMove(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    protected virtual void HandleFlip()
    {
        if (rb.linearVelocityX < 0 && facingRight == true)
        {
            Flip();
        }
        else
        if (rb.linearVelocityX > 0 && facingRight == false)
        {
            Flip();
        }
            
    }

    protected void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    protected virtual void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }


    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }

    private void TryToJump()
    {
        if(isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
    }

    protected virtual void HandleMovement(float xDirection)
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xDirection * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    protected virtual void HandleCollision ()
    {   
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));

        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
