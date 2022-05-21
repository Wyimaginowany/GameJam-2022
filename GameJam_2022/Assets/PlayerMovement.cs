using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;

    PlayerControls playerControls;
    Vector2 movementVector;
    BoxCollider2D collider;
    Rigidbody2D rigidBody;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Player.Jump.performed += Jump;
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementVector = playerControls.Player.Move.ReadValue<Vector2>();
        Debug.Log(playerControls.Player.Move.ReadValue<Vector2>());

        rigidBody.velocity = new Vector2(movementVector.x * movementSpeed, rigidBody.velocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded()) return;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    private bool isGrounded()
    {
        RaycastHit2D groundHit = Physics2D.BoxCast(
            collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return groundHit.collider != null;
    }

}
