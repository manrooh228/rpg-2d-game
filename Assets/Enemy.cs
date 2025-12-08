using UnityEngine;

public class Enemy : Entity 
{

    private bool playerDetected;
    protected override void Update()
    {
        HandleMovement(facingDir);
        HandleCollision();
        HandleAnimations();
        FlipAnimations();
        HandleAttack();
    }
    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("attack");
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
