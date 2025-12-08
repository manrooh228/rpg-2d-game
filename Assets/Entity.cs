using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;

    [Header("Movement Details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    protected int facingDir = -1;
    private float xInput;
    private bool canMove;
    private bool canJump;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        FlipAnimations();
    }
    
    public void DamageTargets()
    {
        Collider2D[] entityColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (Collider2D entity in entityColliders)
        {
            Entity entityTarget = entity.GetComponent<Entity>();
            //entity.TakeDamage();
        }
    }

    private void TakeDamage()
    {

    }

    public void EnableJumpAndMove(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    private void FlipAnimations()
    {
        if (rb.linearVelocityX < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        if (rb.linearVelocityX > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);
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
            TryToAttack();
    }


    protected virtual void TryToAttack()
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

    protected virtual void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
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

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
