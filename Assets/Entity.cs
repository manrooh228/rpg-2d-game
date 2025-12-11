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
    [SerializeField] protected int maxHealth = 1;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected Material damageFeedbackMaterial;
    [SerializeField] protected float damageFeedbackDuration = 0.2f;
    protected Coroutine damageFeedbackCoroutine;
    
    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;

    protected int facingDir = 1;
    protected bool facingRight = true;
    [SerializeField] protected bool canMove = true;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] protected bool isGrounded;

    protected void Awake()
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
        //HandleInput();
        //HandleMovement(xInput);
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
    public virtual void EnableMovements(bool enable)
    {
        canMove = enable;
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

    //private void HandleInput()
    //{
    //    xInput = Input.GetAxisRaw("Horizontal");

    //    if (Input.GetKeyDown(KeyCode.Space))
    //        TryToJump();

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //        HandleAttack();
    //}


    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }

    protected virtual void HandleMovement()
    {   
        
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
