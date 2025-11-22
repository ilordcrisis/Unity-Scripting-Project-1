using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    private int weaponType;
    public float shieldDuration = 5f; // NEW: How long the shield lasts
    

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;   // NEW: Assign in Inspector (your Shield object)
    private Coroutine shieldRoutine;  // NEW: Used to restart timer

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        weaponType = 1;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //Do I have a shield? If yes: do not lose a life, but instead deactivate the shield's visibility
        //If not: lose a life
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.PlaySound(7);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }
    private IEnumerator ShieldTimer() // NEW: Added the timed coroutine. Turn shield off after X seconds
    {
        yield return new WaitForSeconds(10f);
        gameManager.PlaySound(2);
        shieldPrefab.SetActive(false);
        shieldRoutine = null;  // reset so we can restart next time
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    //Picked up speed
                    speed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    weaponType = 2; //Picked up double weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3; //Picked up triple weapon
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break; 
                case 4: // NEW: Shield powerup
                    ActivateShield();
                    gameManager.ManagePowerupText(4);
                    break;
            }
        } 
        
        if(whatDidIHit.tag == "Coin")
        {
            gameManager.PlaySound(3);
            Destroy(whatDidIHit.gameObject);
            gameManager.AddScore(1);
        }

        if(whatDidIHit.tag == "Heal")
        {
            Debug.Log("I need healing");
            gameManager.PlaySound(4);
            Destroy(whatDidIHit.gameObject);
            lives = Mathf.Clamp(lives + 1, 0, 3);
            gameManager.ChangeLivesText(lives);
        }

    }

    public void ActivateShield() // NEW: Added the ActivateShield() function
    {
        // If shield already active, restart its timer
        if (shieldRoutine != null)
            StopCoroutine(shieldRoutine);

        shieldPrefab.SetActive(true);

        // Start shield timer
        shieldRoutine = StartCoroutine(ShieldTimer());
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(weaponType)
            {
                case 1:
                    gameManager.PlaySound(8);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    gameManager.PlaySound(8);
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    gameManager.PlaySound(8);
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

        //finds the halfway point of the screen
        float halfY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2f, Camera.main.transform.position.z * -1)).y;
        //finds the bottom point of the screen
        float bottomY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z * -1)).y;

        // clamp position between bottom and halfway point
        Vector3 pos = transform.position;

        if(pos.y > halfY)
        {
            pos.y = halfY;
        }
        
        if(pos.y < bottomY)
        {
            pos.y = bottomY;
        }
        
        transform.position = pos;
    }
}