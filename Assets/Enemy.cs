using UnityEngine;

public class Enemy : Entity 
{
    [SerializeField] private Transform player;
    private bool playerDetected;


    protected override void Update()
    {
        HandleMovement(facingDir);
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
        if (player.transform.position.x < transform.position.x && facingRight == true)
        {
            Flip();
        }
        else
        if (player.transform.position.x > transform.position.x && facingRight == false)
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
    //protected override void HandleMovement()
    //{
    //    if (canMove)
    //        rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
    //    else
    //        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    //}
}
