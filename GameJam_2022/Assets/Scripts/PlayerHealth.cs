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
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] float holdDur = 2f;

    float timer;
    LevelLoader loader;
    bool isDead = false;
    AudioSource audio;
    Rigidbody2D rigidbody;
    PlayerMovement playerMovement;
    public bool isOnSpawn = true;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        loader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        timer = Time.time;
    }

    private void Update()
    {
        HandleGameRestarting();

        if (Input.GetKeyDown(KeyCode.Tab) && !isLastPlayer && !isOnSpawn)
        {
            PlayerDeath();
        }
    }

    private void HandleGameRestarting()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = Time.time;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (Time.time - timer > holdDur)
            {
                timer = float.PositiveInfinity;
                loader.RestartLevel();
            }
        }
        else
        {
            timer = float.PositiveInfinity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap"))
        {
            PlayerDeath();
        }

        if (collision.collider.CompareTag("End"))
        {
            loader.LoadNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spawn"))
        {
            isOnSpawn = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spawn"))
        {
            isOnSpawn = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Spawn"))
        {
            isOnSpawn = false;
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
        deathParticle.Play();
        deadBody.SetActive(true);
        isDead = true;
    }
}
