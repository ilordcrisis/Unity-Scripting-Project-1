using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 8f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
