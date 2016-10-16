using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    private Transform transform;
    private Rigidbody2D rigidbody;
    private float move;
    private float jump;
    private bool facingRight;
    private bool grounded;
    private Animator animator;
    private Camera camera;
    private Transform spawnPoint;
    private GameController gameController;

    public float velocity = 10f;
    public float jumpForce = 100f;


    [Header("Sound Clips")]
    public AudioSource jumpSound;
    public AudioSource deathSound;
    public AudioSource coinSound;
    public AudioSource hurtSound;

    // Use this for initialization
    void Start() {
        initialize();
    }

    // Update is called once per frame (Physics)
    void FixedUpdate() {

        if (grounded) {

            // check if input is present for movement
            move = Input.GetAxis("Horizontal");

            if (move > 0f) {
                // set the animation to walk
                animator.SetInteger("HeroState", 1);
                move = 1f;
                facingRight = true;
                flip();
            } else if (move < 0f) {
                // set the animation to walk
                animator.SetInteger("HeroState", 1);
                move = -1f;
                facingRight = false;
                flip();
            } else {
                // set the animation to idle
                animator.SetInteger("HeroState", 0);
                move = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                jump = 1f;
                jumpSound.Play();
            }

            rigidbody.AddForce(new Vector2(
                   move * velocity,
                   jump * jumpForce),
                   ForceMode2D.Force);
        } else {
            move = 0f;
            jump = 0f;
        }

        moveCamera();

        //Debug.Log(move);
        Debug.Log("Grounded: " + grounded);
    }

    private void moveCamera() {

        camera.transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    -10f);
    }

    private void flip() {
        if (facingRight) {
            transform.localScale = new Vector2(1f, 1f);
        } else {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.CompareTag("Platform")) {
            grounded = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin")) {
            DestroyObject(other.gameObject);
            coinSound.Play();
            gameController.ScoreValue += 10;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            deathSound.Play();
            // lose life
            transform.position = spawnPoint.position;
            gameController.LivesValue -= 1;
        }

        if (other.gameObject.CompareTag("Enemy")) {
            hurtSound.Play();
            // lose life
            transform.position = spawnPoint.position;
            gameController.LivesValue -= 1;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        // set the animation to jump
        animator.SetInteger("HeroState", 2);
        grounded = false;
    }

    private void initialize() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;

        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        move = 0f;
        facingRight = true;
        grounded = false;
        transform.position = spawnPoint.position;
    }
}
