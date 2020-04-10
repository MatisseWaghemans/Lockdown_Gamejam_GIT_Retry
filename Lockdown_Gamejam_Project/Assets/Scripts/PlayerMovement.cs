using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rigidbody;
    private bool _hasSquashed = true;
    private bool _hasTurned = false;
    [SerializeField] private float _Squash = 0.2f;
    [SerializeField] private float Frequency = 2f;


    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _force;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private SpriteRenderer character;
    private Transform _spawnTransform;

    private bool _hasShot = true;

    public float Reloadtime = 1f;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
            ShootGun();
        if(_hasShot)
        MoveGun();
    }

    private void Move()
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>mouse.x)
        {
            character.flipX = true;
        }
        else
        {
            character.flipX =false;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetAxisRaw("Horizontal") < 0 && !_hasTurned)
        {
            _hasTurned = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && _hasTurned)
        {
            _hasTurned = false;
        }

        movement = movement.normalized;

        if (movement.magnitude > 0.1f & _hasSquashed)
        {
            StartCoroutine(Squash());
        }
    }

    private void ShootGun()
    {
        if (_hasShot)
        {
            _spawnTransform = _bulletSpawn;
            _spawnTransform.rotation = _gun.transform.rotation;
            _hasShot = false;
            StartCoroutine(ShootCouroutine(Reloadtime));
        }
    }
    private void MoveGun()
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        if(playerPos.x>mouse.x)
        {
            _gun.transform.localPosition = new Vector3( 0.84f,-0.7f, _gun.transform.localPosition.z);
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = true;
        }
        else
        {
            _gun.transform.localPosition = new Vector3( -0.6f,-1.5f, _gun.transform.localPosition.z);
            _gun.GetComponentInChildren<SpriteRenderer>().flipY = false;
        }
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - _gun.transform.position.y, lookAt.x - _gun.transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        _gun.transform.rotation = Quaternion.Euler(-AngleDeg, 90, 0);
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.deltaTime);
    }


    IEnumerator ShootCouroutine(float Seconds)
    {

        Shoot();
        yield return new WaitForSeconds(0.07f);

        Shoot();
        yield return new WaitForSeconds(0.07f);

        Shoot();

        yield return new WaitForSeconds(Seconds);
        _hasShot = true;
        yield return null;
    }

    private void Shoot()
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if(mouse.x<0.5f)
        {
            GameObject _bullet =Instantiate(_bulletPrefab,_spawnTransform.position,Quaternion.Euler(0,0,_spawnTransform.eulerAngles.x));
                    _bullet.GetComponent<Rigidbody2D>().AddForce(_spawnTransform.forward*_force,ForceMode2D.Impulse);
        }
        else
        {
            GameObject _bullet =Instantiate(_bulletPrefab,_spawnTransform.position,Quaternion.Euler(0,0,-_spawnTransform.eulerAngles.x));
                    _bullet.GetComponent<Rigidbody2D>().AddForce(_spawnTransform.forward*_force,ForceMode2D.Impulse);
        }
    }

    IEnumerator Squash()
    {
        float time = 0;
        _hasSquashed = false;


        float sin = 0;

        while (sin >= 0)
        {
             sin = _Squash * Mathf.Sin(time * Frequency);
            transform.localScale = Vector3.one + new Vector3(-sin, sin, -sin);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _hasSquashed = true;
        yield return null;
    }
}
