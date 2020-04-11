using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float  LerpValue;
    Generator _rooms;
    
    int _room =0;

 void Start()
 {
   _rooms = GetComponent<Generator>();
 }
    private void FixedUpdate()
  {
    transform.position = Vector3.Lerp(transform.position,_rooms._roomPositionList[_room],Time.deltaTime);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Player"))
    {
      _room++;
    }

  }
}
