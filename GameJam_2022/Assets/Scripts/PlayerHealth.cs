using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerPool pool;
    [SerializeField] bool isLastPlayer = false;
    [SerializeField] GameObject[] bodyParts;
    [SerializeField] GameObject deadBody;
    [SerializeField] AudioClip deathSound;

    bool isDead = false;
    AudioSource audio;
    Rigidbody2D rigidbody;
    PlayerMovement playerMovement;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isLastPlayer)
        {
            PlayerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        if (isDead) return;

        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.KillPlayerMovement();
        gameObject.layer = LayerMask.NameToLayer("Dead");
        audio.PlayOneShot(deathSound);

        foreach (GameObject part in bodyParts)
        {
            part.SetActive(false);
        }
        deadBody.SetActive(true);
        isDead = true;
    }
}
