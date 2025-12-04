using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField]private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        MoveHorizontal();
        HandleAnimations();
        FlipAnimations();
    }

    private void FlipAnimations()
    {
        if (rb.linearVelocityX < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        if (rb.linearVelocityX > 0)
            transform.rotation = new Quaternion(0, 0, 0, 0);


    }

    private void HandleAnimations()
    {
        bool isMoving = rb.linearVelocityX != 0;
        anim.SetBool("isMoving", isMoving);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void MoveHorizontal()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }
}
