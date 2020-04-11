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
            //remove some ppl
        }
    }
}
