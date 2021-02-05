using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    //public float maxAirTime = 1.1f;
    //public float timeInAir = 0f;
    private float groundHeightCoordinates;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip dieSound;

    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        groundHeightCoordinates = transform.position.y;
        animator = GetComponent<Animator>();
        // The default sound
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = jumpSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetComponent<GameController>().gameState == GameState.playing)
        {
            jumpBehaviour();
        }
       
    }

    private void jumpBehaviour()
    {
        if (transform.position.y <= groundHeightCoordinates && isPressingJump())
        {
            animator.Play("PlayerJumping");
            audioSource.Play();
        }

        // DEPRECATED
        //bool isInAir = animator.GetBool("isInAir");

        //if (!isInAir && isPressingJump())
        //{
        //    animator.SetBool("isInAir", true);
        //    timeInAir = maxAirTime;
        //    audioSource.Play();
        //}
        //else if (isInAir)
        //{
        //    if (timeInAir <= 0)
        //    {
        //        animator.SetBool("isInAir", false);
        //    }
        //    else
        //    {
        //        timeInAir -= Time.deltaTime;
        //    }
        //}
    }

    public void isPlaying(bool isPlaying)
    {
        animator.SetBool("isPlaying", isPlaying);
    }

    public void killPlayer()
    {
        animator.Play("PlayerDying");
        audioSource.PlayOneShot(dieSound);
    }

    public void allowResetGame()
    {
        game.GetComponent<GameController>().gameState = GameState.readyToReset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            killPlayer();
            game.GetComponent<GameController>().gameState = GameState.ended;
        }
    }

    public bool isPressingJump()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0);
    }
}
