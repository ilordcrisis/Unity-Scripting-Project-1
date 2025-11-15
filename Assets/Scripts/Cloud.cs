
using UnityEngine;

public class Cloud : MonoBehaviour
{

    private float speed;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.localScale = transform.localScale * Random.Range(0.5f, 1.5f);
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Random.Range(0.1f, 0.7f));
        speed = Random.Range(1f, 3f) * gameManager.cloudMove;
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed * gameManager.cloudMove;

        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);

        if (transform.position.y < -gameManager.verticalScreenSize)
        {
            transform.position = new Vector3(Random.Range(-gameManager.horizontalScreenSize, gameManager.horizontalScreenSize), gameManager.verticalScreenSize, 0);
        }
        
    }
}
