﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 _randomPos;
    private bool _moving = true;
    // Start is called before the first frame update
    void Start()
    {
        _randomPos = Random.insideUnitCircle*10;
        Debug.Log(_randomPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(_moving)
        {
        float distance = Vector3.Distance(transform.position,_player.transform.position);
        if(distance<10)
        {
            _timer += Time.deltaTime;
            FollowPlayer();
            MoveGun();
            if(_timer>3)
            {
                ShootPlayer();
                _timer=0;
            }
        }
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = true;
            Destroy(_gun);
        }
    }
    void ShootPlayer()
    {
        if (_hasShot)
        {
            _hasShot = false;
            StartCoroutine(ShootCouroutine(0.4f));
        }
    }
    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position,_player.transform.position,Time.deltaTime*0.3f);
    }

    private void MoveGun()
    {
        Vector3 player = Camera.main.WorldToViewportPoint(_player.transform.position);
        Vector3 enemyPos = Camera.main.WorldToViewportPoint(transform.position);
        if(enemyPos.x>player.x)
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = true;
            _gun.transform.localPosition = new Vector3(-0.4f,0.15f,-0.01f);
        }
        else
        {
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = false;
            _gun.transform.localPosition = new Vector3(0.38f,0.07f,-0.01f);
        }

        float distance = Vector3.Distance(transform.position,_randomPos);
        if(distance<1)
        {
            _moving = false;
            _randomPos =Random.insideUnitCircle*10;
            Debug.Log(_randomPos);
            _moving = true;
        }
    }
}
