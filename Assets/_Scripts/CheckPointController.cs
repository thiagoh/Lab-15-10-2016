using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {

    private Transform transform;

    public Transform spawnPoint;

    // Use this for initialization
    void Start() {
        transform = GetComponent<Transform>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {
            spawnPoint.position = transform.position;
        }
    }
}
