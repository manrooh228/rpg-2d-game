using UnityEngine;

public class Player : Entity
{
    [SerializeField] protected Animator healthAnim;
    private float xInput;
    [Header("Movement Details")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump = true;

    protected override void Awake()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }
    public override void EnableMovements(bool enable)
    {
        base.EnableMovements(enable);
        canJump = enable;
    }
    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.y, jumpForce);
    }
}
