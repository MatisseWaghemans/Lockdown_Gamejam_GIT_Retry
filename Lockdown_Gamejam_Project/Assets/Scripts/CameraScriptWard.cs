using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptWard : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float  LerpValue;


    private void FixedUpdate()
  {
<<<<<<< HEAD
<<<<<<< HEAD
=======
    Player = FindObjectOfType<PlayerMovement>().transform;
>>>>>>> parent of f6d7101... Sounds
=======
    Player = FindObjectOfType<PlayerMovement>().transform;
>>>>>>> parent of f6d7101... Sounds
      transform.position = Vector3.Lerp(transform.position,
          new Vector3(Player.position.x, Player.position.y, transform.position.z), LerpValue);
  }
}
