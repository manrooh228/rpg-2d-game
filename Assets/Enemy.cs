using UnityEngine;

public class Enemy : Entity 
{
    private bool playerDetected;

    [Header("Movement Details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private Transform whatWeFollow;
    //[SerializeField] private float jumpForce = 8;

    protected override void Update()
    {
        HandleMovement();
        HandleCollision();
        HandleAnimations();
        HandleFlip();
        HandleAttack();
    }
    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("attack");
        }
    }

    protected override void HandleFlip()
    {
        if (whatWeFollow.transform.position.x < transform.position.x && facingRight == true)
        {
            Flip();
        }
        else
        if (whatWeFollow.transform.position.x > transform.position.x && facingRight == false)
        {
            Flip();
        }
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
    }
    protected override void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
