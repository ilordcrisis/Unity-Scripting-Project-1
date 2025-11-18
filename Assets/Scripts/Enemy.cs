using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;

    public float speed = 8f;

    // Enum for readability
    private enum MovementType { StraightDown, Diagonal, ZigZag }
    private MovementType movementType;

    private Vector3 moveDirection;
    private float zigzagFrequency;
    private float zigzagAmplitude;
    private float startX;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       
        // Randomly choose one of the three movement patterns
        int randomPattern = Random.Range(0, 3);
        movementType = (MovementType)randomPattern;

        // Initialize movement parameters
        startX = transform.position.x;

        switch (movementType)
        {
            case MovementType.StraightDown:
                moveDirection = Vector3.down; // (0, -1, 0)
                break;

            case MovementType.Diagonal:
                float randomX = Random.Range(-0.7f, 0.7f);
                moveDirection = new Vector3(randomX, -1f, 0f).normalized;
                break;

            case MovementType.ZigZag:
                // Moves down while oscillating horizontally
                zigzagFrequency = Random.Range(2f, 4f);   // how fast it oscillates
                zigzagAmplitude = Random.Range(1f, 2f);   // how wide the zig-zag is
                break;
        }
    }

    void Update()
    {
        switch (movementType)
        {
            case MovementType.StraightDown:
                transform.Translate(moveDirection * speed * Time.deltaTime);
                break;

            case MovementType.Diagonal:
                transform.Translate(moveDirection * speed * Time.deltaTime);
                break;

            case MovementType.ZigZag:
                // Move down while oscillating on the X-axis
                float xOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
                transform.position += new Vector3(xOffset * Time.deltaTime, -speed * Time.deltaTime, 0f);
                break;
        }

        // Destroy if off-screen
        if (transform.position.y < -6.5f || Mathf.Abs(transform.position.x) > 12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        Debug.Log("Enemy collided with: " + whatDidIHit.name + " | tag: " + whatDidIHit.tag);

        if(whatDidIHit.gameObject.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            gameManager.PlaySound(5);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatDidIHit.gameObject.tag == "Weapons")
        {
            Debug.Log("Bullet hit enemy, adding score!");
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(5);
            gameManager.PlaySound(6);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);  
            Destroy(this.gameObject);
        }

    }
}
