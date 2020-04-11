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
        if(Camera.main.WorldToViewportPoint(transform.position).x<0||Camera.main.WorldToViewportPoint(transform.position).x>1)
        {
            Destroy(gameObject);
        }
        if(Camera.main.WorldToViewportPoint(transform.position).y<0||Camera.main.WorldToViewportPoint(transform.position).y>1)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Civillian"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

    }
    void OnColisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
