using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
<<<<<<< Updated upstream
=======
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    public GameObject cloudPrefab;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject powerupPrefab;
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public GameObject audioPlayer;
>>>>>>> Stashed changes

    public GameObject enemyTwoPrefab;

    public GameObject enemyThreePrefab;

    public TextMeshProUGUI livesText;

    public int score;

    public float horizontalScreenSize = 6f;

    public float verticalScreenSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 3, 4);
        InvokeRepeating("CreateEnemyThree", 5, 6);
<<<<<<< Updated upstream
=======
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnCoin());
        StartCoroutine(SpawnHeart());
        //powerupText.text = "No powerups yet!";
>>>>>>> Stashed changes
    }

    void CreateEnemyOne()
    {
        Debug.Log("I am enemy one");
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Debug.Log("I am enemy two");
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-7f, 7f), 6.5f, 0), Quaternion.identity);
    }

    void CreateEnemyThree()
    {
<<<<<<< Updated upstream
        Debug.Log("I am enemy three");
        Instantiate(enemyThreePrefab, new Vector3(Random.Range(-3f, 3f), 6.5f, 0), Quaternion.identity);
=======
        Instantiate(enemyThreePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.8f, verticalScreenSize, 0), Quaternion.identity);
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, verticalScreenSize * 0.8f), 0), Quaternion.identity);
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, verticalScreenSize * 0.8f), 0), Quaternion.identity);
    }

    void CreateHeart()
    {
        Instantiate(heartPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-verticalScreenSize * 0.8f, verticalScreenSize * 0.8f), 0), Quaternion.identity);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
        
    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5); 
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnCoin()
    {
        Debug.Log("A coin spawned.");
        float spawnTime = Random.Range(3, 5); 
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnHeart()
    {
        Debug.Log("A heart spawned.");
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreateHeart();
        StartCoroutine(SpawnHeart());
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
        }
>>>>>>> Stashed changes
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}