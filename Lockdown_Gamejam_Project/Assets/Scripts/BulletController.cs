using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Civillian"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
        Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Hit();
        Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Civillian"))
        {
        Destroy(gameObject);
        }
    }
}
