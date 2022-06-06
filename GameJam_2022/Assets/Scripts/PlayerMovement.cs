using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float wallSlidingSpeed = 1f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask deadPlayerLayer;
    [SerializeField] PlayerPool pool;

    float wasOnGround;
    float jumpWasPressed;
    bool isDead = false;
    PlayerControls playerControls;
    BoxCollider2D collider;
    Rigidbody2D rigidBody;
    Vector2 movementVector;
    Vector2 startingPosition;
    Vector2 parentVelocity;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        //playerControls.Player.Jump.performed += Jump;
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        movementVector = playerControls.Player.Move.ReadValue<Vector2>();
        if (transform.parent.GetComponent<Rigidbody2D>() != null)
        {
            parentVelocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            parentVelocity = new Vector2(0, 0);
        }

        if (onWall() && movementVector.x !=0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        wasOnGround -= Time.deltaTime;
        jumpWasPressed -= Time.deltaTime;

        if (isGrounded() || isOnPlayer())
        {
            wasOnGround = 0.07f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpWasPressed = 0.1f;
        }

        if (jumpWasPressed > 0 && wasOnGround > 0)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(parentVelocity.x + movementVector.x * movementSpeed, rigidBody.velocity.y);
    }

    private void Jump()
    {
        jumpWasPressed = 0;
        wasOnGround = 0;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    private bool isGrounded()
    {
        RaycastHit2D groundHit = Physics2D.BoxCast(
            collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return groundHit.collider != null;
    }

    private bool isOnPlayer()
    {
        RaycastHit2D playerHit = Physics2D.BoxCast(
            collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, deadPlayerLayer);
        return playerHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D wallHitRight = Physics2D.BoxCast(
        collider.bounds.center, collider.bounds.size, 0, new Vector2(1, 0), 0.01f, groundLayer);
        RaycastHit2D wallHitLeft = Physics2D.BoxCast(
        collider.bounds.center, collider.bounds.size, 0, new Vector2(-1, 0), 0.01f, groundLayer);

        return (wallHitRight.collider != null || wallHitLeft.collider != null);
    }

    public void KillPlayerMovement()
    {
        if (isDead) return;
        playerControls.Disable();
        pool.SpawnNextPlayer();
        isDead = true;
    }
}
