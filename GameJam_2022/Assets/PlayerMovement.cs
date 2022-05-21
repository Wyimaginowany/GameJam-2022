using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;

    BoxCollider2D collider;
    Rigidbody2D rigidBody;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rigidBody.velocity.y);
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D groundHit = Physics2D.BoxCast(
            collider.bounds.center,
            collider.bounds.size,
            0, Vector2.down,
            0.01f,
            groundLayer);
        return groundHit.collider != null;
    }

}
