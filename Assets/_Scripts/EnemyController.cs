using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

    //[SerializeField]
    private Transform transform;
    private Rigidbody2D rigidbody;
    private bool grounded;
    private bool groundAhead;
    private bool playerDetected;

    public float speed;
    public float INITIAL_SPEED = 2f;
    public float MAXIMUM_SPEED = 4f;

    public Transform sightStart;
    public Transform sightEnd;
    public Transform lineOfSight;

    // Use this for initialization
    void Start() {
        initialize();
    }

    private void initialize() {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        grounded = false;
        groundAhead = true;
        playerDetected = false;
        speed = INITIAL_SPEED;
        //move = 0f;
        //facingRight = true;
        //transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (grounded) {
            rigidbody.velocity = new Vector2(transform.localScale.x, 0f) * speed;

            groundAhead = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Solid"));
            playerDetected = Physics2D.Linecast(sightStart.position, lineOfSight.position, 1 << LayerMask.NameToLayer("Player"));

            Debug.DrawLine(sightStart.position, sightEnd.position, Color.yellow);
            Debug.DrawLine(sightStart.position, lineOfSight.position, Color.red);

            if (!groundAhead) {
                flip();
            }

            if (playerDetected) {
                speed = MAXIMUM_SPEED;
            } else {
                speed = INITIAL_SPEED;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.CompareTag("Platform")) {
            grounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
        }
        if (other.gameObject.CompareTag("Enemy")) {
            flip();
        }
    }

    public void OnCollisionExit2D(Collision2D other) {

        grounded = false;
    }

    private void flip() {
        if (transform.localScale.x == 1) {
            transform.localScale = new Vector2(-1f, 1f);
        } else {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
