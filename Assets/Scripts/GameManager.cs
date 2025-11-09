using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;

    public TextMeshProUGUI livesText;

    public int score;

    public float horizontalScreenSize = 6f;

    public float verticalScreenSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        InvokeRepeating("CreateEnemyOne", 1, 2);
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
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