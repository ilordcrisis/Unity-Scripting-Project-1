using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;

    private GameManager gameManager;

    // Speed and movement direction
    public float speed = 8f;
    private Vector3 moveDirection;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Assign a random diagonal direction (mostly downward)
        float randomX = Random.Range(-0.5f, 0.5f); // small horizontal variation
        moveDirection = new Vector3(randomX, -1f, 0f).normalized; // normalize for consistent speed
    }

    void Update()
    {
        // Move in the assigned direction
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Destroy if off-screen
        if (transform.position.y < -6.5f || Mathf.Abs(transform.position.x) > 10f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.gameObject.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatDidIHit.gameObject.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
