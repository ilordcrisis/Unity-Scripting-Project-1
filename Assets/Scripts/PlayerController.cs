using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public int lives;

    private float playerSpeed;

    private float horizontalInput;

    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;

    private float verticalScreenLimit = 6.5f;

    public GameObject explosionPrefab;

    public GameObject bulletPrefab;

    private GameManager gameManager;


    void Start()
    {
        playerSpeed = 6f;
        lives = 3;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
    }

    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();

    }

    void Movement()
    {
        //get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //player can leave the screen horizontally
   
        if(transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //player can leave the screen vertically
        if(transform.position.y > verticalScreenLimit || transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }
    
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pew Pew" + verticalScreenLimit);
            //spawn bullet at player position
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
