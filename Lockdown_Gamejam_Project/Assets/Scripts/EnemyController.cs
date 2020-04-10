using System.Collections;
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
        transform.position = Vector3.Lerp(transform.position, _randomPos, Time.deltaTime);
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
