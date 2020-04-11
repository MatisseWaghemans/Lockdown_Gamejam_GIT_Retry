using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteBooth : MonoBehaviour
{
    //private SivilianController _sivilliancontroller;
    private void Start()
    {
        //_civilianController.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponentInChildren<GameObject>().CompareTag("Civillian"))
            {
                for(int i=0;i<10;i++)
                {
                Destroy(other.GetComponent<PlayerMovement>()._followers[i]);
                }
            }
        }
    }
}
