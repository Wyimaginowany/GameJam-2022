using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerPool pool;
    [SerializeField] bool isLastPlayer = false;
    Rigidbody2D rigidbody;
    PlayerMovement playerMovement;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isLastPlayer)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            playerMovement.KillPlayerMovement();
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            playerMovement.KillPlayerMovement();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }
}
